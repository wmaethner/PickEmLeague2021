using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;
using Xunit;

namespace PickEmLeagueServiceTests
{
    public class GamePickServiceTests
    {
        private List<Game> _games = new List<Game>();

        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IGamePickRepository _gamePickRepository;
        private readonly IGamePickService _gamePickService;
        private readonly IGameService _gameService;

        public GamePickServiceTests()
        {
            var serviceProvider = ServiceHelper.BuildUnitTestServices("gamePickTests");

            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _gameRepository = serviceProvider.GetRequiredService<IGameRepository>();
            _gamePickRepository = serviceProvider.GetRequiredService<IGamePickRepository>();
            _gameService = serviceProvider.GetRequiredService<IGameService>();
            _gamePickService = serviceProvider.GetRequiredService<IGamePickService>();
        }

        [Fact]
        public async Task GetByUser_ReturnsPicks()
        {
            InitializeDb();

            var user = await _userRepository.CreateAsync();

            await CreateGamesAsync(2, 5);

            foreach (var game in _games)
            {
                await CreateGamePick(game, user);
            }

            Assert.Equal(10, _gamePickService.GetByUser(user.Id).Count());
            Assert.Empty(await _gamePickService.GetByUserAndWeekAsync(user.Id + 1, 3));
        }

        [Fact]
        public async Task GetByUserAndWeek_WeekDefined_ReturnsPicks()
        {
            InitializeDb();

            var user = await _userRepository.CreateAsync();

            await CreateGamesAsync(2, 5);

            foreach (var game in _games)
            {
                await CreateGamePick(game, user);
            }

            Assert.Equal(5, (await _gamePickService.GetByUserAndWeekAsync(user.Id, 1)).Count());
            Assert.Equal(5, (await _gamePickService.GetByUserAndWeekAsync(user.Id, 2)).Count());
            Assert.Empty(await _gamePickService.GetByUserAndWeekAsync(user.Id, 3));
        }

        [Fact]
        public async Task GetByUserAndWeek_PicksNotCreated_CreatesAndReturnsPicks()
        {
            InitializeDb();

            var user = await _userRepository.CreateAsync();

            await CreateGamesAsync(2, 5);

            Assert.Equal(5, (await _gamePickService.GetByUserAndWeekAsync(user.Id, 1)).Count());
            Assert.Equal(5, (await _gamePickService.GetByUserAndWeekAsync(user.Id, 2)).Count());
            Assert.Empty(await _gamePickService.GetByUserAndWeekAsync(user.Id, 3));
        }

        [Fact]
        public async Task GetByUserAndWeek_PicksCreated_GameNoLongerExists_CreatesAndRemovesPicks()
        {
            InitializeDb();

            var user = await _userRepository.CreateAsync();

            await CreateGamesAsync(2, 5);

            foreach (var game in _gameRepository.GetAll())
            {
                await CreateGamePick(game, user);
            }

            var games = _gameRepository.GetAll();
            var game1 = games.First(g => g.Week == 1);
            var game2 = games.Where(g => g.Week == 2).ToList();
            Assert.True(await _gameRepository.DeleteAsync(game1.Id));
            Assert.True(await _gameRepository.DeleteAsync(game2[0].Id));
            Assert.True(await _gameRepository.DeleteAsync(game2[1].Id));

            Assert.Equal(4, (await _gamePickService.GetByUserAndWeekAsync(user.Id, 1)).Count());
            Assert.Equal(3, (await _gamePickService.GetByUserAndWeekAsync(user.Id, 2)).Count());
            Assert.Empty(await _gamePickService.GetByUserAndWeekAsync(user.Id, 3));
        }

        [Fact]
        public async Task GetByUserAndWeek_PicksAndWagersCreated_RemoveGame_AdjustsWagersCorrectly()
        {
            InitializeDb();

            var user = await _userRepository.CreateAsync();

            await CreateGamesAsync(1, 5);

            foreach (var game in _games)
            {
                await CreateGamePick(game, user);
            }
            // Sets wagers
            var picks = await _gamePickService.GetByUserAndWeekAsync(user.Id, 1);
            await _gameService.DeleteGame(picks.Where(p => p.Wager == 4).First().GameId);
            
            Assert.Equal(4, (await _gamePickService.GetByUserAndWeekAsync(user.Id, 1)).Count());
            var wagers = (await _gamePickService.GetByUserAndWeekAsync(user.Id, 1)).Select(p => p.Wager).ToList();

            for (int i = 1; i <= 4; i++)
            {
                Assert.Contains(i, wagers);
            }

            await _gameService.DeleteGame(picks.Where(p => p.Wager == 2).First().GameId);

            Assert.Equal(3, (await _gamePickService.GetByUserAndWeekAsync(user.Id, 1)).Count());
            wagers = (await _gamePickService.GetByUserAndWeekAsync(user.Id, 1)).Select(p => p.Wager).ToList();

            for (int i = 1; i <= 3; i++)
            {
                Assert.Contains(i, wagers);
            }
        }

        [Fact]
        public async Task GetByUserAndWeek_PicksNotCreated_AllWagersSet()
        {
            InitializeDb();

            var user = await _userRepository.CreateAsync();

            await CreateGamesAsync(1, 5);

            var picks = (await _gamePickService.GetByUserAndWeekAsync(user.Id, 1)).ToList();
            Assert.Equal(5, picks.Count());
            for (int i = 1; i <= picks.Count(); i++)
            {
                Assert.NotNull(picks.Find(p => p.Wager == i));
            }
        }

        [Fact]
        public async Task GetByUserAndWeek_BadWeek_NoErrorAsync()
        {
            InitializeDb();

            var user = await _userRepository.CreateAsync();

            await CreateGamesAsync(1, 5);
            var picks = (await _gamePickService.GetByUserAndWeekAsync(user.Id, 2)).ToList();
        }

        private void InitializeDb()
        {
            _gamePickRepository.ClearDb();
            _gameRepository.ClearDb();
        }

        private async Task CreateGamesAsync(int weeks, int gamesPerWeeks)
        {
            for (int week = 1; week <= weeks; week++)
            {
                for (int i = 0; i < gamesPerWeeks; i++)
                {
                    await CreateGameAsync(1, 2, week);
                }
            }
        }

        private async Task CreateGameAsync(long homeTeam, long awayTeam, int week)
        {
            Game game = new Game()
            {
                HomeTeamId = homeTeam,
                AwayTeamId = awayTeam,
                Week = week
            };
            game = await _gameRepository.CreateAsync(game);
            _games.Add(game);
        }

        private async Task CreateGamePick(Game game, User user)
        {
            await _gamePickRepository.CreateAsync(game, user);
        }
    }
}
