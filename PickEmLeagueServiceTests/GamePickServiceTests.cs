using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeague.Registrations;
using PickEmLeagueDatabase;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueModels.Profiles;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;
using Xunit;

namespace PickEmLeagueServiceTests
{
    public class GamePickServiceTests
    {
        private List<Game> _games = new List<Game>();

        private readonly PickEmLeagueDbContext _dbContext;

        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IGamePickRepository _gamePickRepository;
        private readonly IGamePickService _gamePickService;

        public GamePickServiceTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.test.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            var servicesCollection = new ServiceCollection();
            servicesCollection.RegisterRepositories();
            servicesCollection.RegisterServices();
            servicesCollection.RegisterDatabase(configuration, true);
            servicesCollection.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));

            var serviceProvider = servicesCollection.BuildServiceProvider();


            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _gameRepository = serviceProvider.GetRequiredService<IGameRepository>();
            _gamePickRepository = serviceProvider.GetRequiredService<IGamePickRepository>();
            _gamePickService = serviceProvider.GetRequiredService<IGamePickService>();

            _dbContext = serviceProvider.GetRequiredService<PickEmLeagueDbContext>();
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

        private void InitializeDb()
        {
            _dbContext.Database.EnsureDeleted();
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
