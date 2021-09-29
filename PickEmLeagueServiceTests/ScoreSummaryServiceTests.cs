using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeagueDatabase;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;
using PickEmLeagueShared.Enums;
using Xunit;

namespace PickEmLeagueServiceTests
{
    public class ScoreSummaryServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IGamePickService _gamePickService;
        private readonly IMapper _mapper;
        private readonly IScoreSummaryService _scoreSummaryService;
        private readonly PickEmLeagueDbContext _dbContext;

        public ScoreSummaryServiceTests()
        {
            var serviceProvider = ServiceHelper.BuildUnitTestServices("scoreSummaryTests");

            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _gameRepository = serviceProvider.GetRequiredService<IGameRepository>();
            _gamePickService = serviceProvider.GetRequiredService<IGamePickService>();
            _scoreSummaryService = serviceProvider.GetRequiredService<IScoreSummaryService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _dbContext = serviceProvider.GetRequiredService<PickEmLeagueDbContext>();
        }

        [Fact]
        public async Task GetSummaries_UserHasntMadePicks_ZeroPointsAndCorrectStatusAsync()
        {
            InitializeDb();
            await CreateUserAsync();
            CreateGames(1, 5, GameResult.HomeWin);
            CreateGames(2, 5, GameResult.HomeWin);

            var summary = _scoreSummaryService.GetSummaries(1);
            var userSummary = summary.ToList()[0];

            Assert.Equal(1, userSummary.User.Id);
            Assert.Equal(0, userSummary.WeekSummary.WeekScore);
            Assert.Equal(WeekPickStatus.NotPicked, userSummary.WeekSummary.WeekPickStatus);
        }

        [Fact]
        public async Task GetScoreSummary_UserMadeCorrectPicks_CorrectScoreAsync()
        {
            InitializeDb();
            await CreateUserAsync();
            CreateGames(1, 5, GameResult.HomeWin);
            CreateGames(2, 5, GameResult.HomeWin);
            await MakeUserPicksAsync(1, 2, GameResult.HomeWin);
            
            var summary = _scoreSummaryService.GetSummaries(2);
            var userSummary = summary.ToList()[0];

            Assert.Equal(1, userSummary.User.Id);
            Assert.Equal(15, userSummary.WeekSummary.WeekScore);
            Assert.Equal(15, userSummary.SeasonSummary.SeasonScore);
            Assert.Equal(WeekPickStatus.FullyPicked, userSummary.WeekSummary.WeekPickStatus);
        }

        [Fact]
        public async Task GetScoreSummary_MultipleUsers_Place1Then2()
        {
            InitializeDb();
            var user1 = await CreateUserAsync();
            var user2 = await CreateUserAsync();
            CreateGames(1, 5, GameResult.HomeWin);
            await MakeUserPicksAsync(user1.Id, 1, GameResult.AwayWin);
            await MakeUserPicksAsync(user2.Id, 1, GameResult.HomeWin);
            
            var summary = _scoreSummaryService.GetSummaries(1);

            Assert.Equal(2, summary.FirstOrDefault(s => s.User.Id == user1.Id).WeekSummary.Place);
            Assert.Equal(2, summary.FirstOrDefault(s => s.User.Id == user1.Id).SeasonSummary.Place);
            Assert.Equal(1, summary.FirstOrDefault(s => s.User.Id == user2.Id).WeekSummary.Place);
            Assert.Equal(1, summary.FirstOrDefault(s => s.User.Id == user2.Id).SeasonSummary.Place);
        }

        [Fact]
        public async Task GetScoreSummary_MultipleUsersWithTies_Place1Then1Then3()
        {
            InitializeDb();
            var user1 = await CreateUserAsync();
            var user2 = await CreateUserAsync();
            var user3 = await CreateUserAsync();
            CreateGames(1, 5, GameResult.HomeWin);
            await MakeUserPicksAsync(user1.Id, 1, GameResult.HomeWin);
            await MakeUserPicksAsync(user2.Id, 1, GameResult.AwayWin);
            await MakeUserPicksAsync(user3.Id, 1, GameResult.HomeWin);
            
            var summary = _scoreSummaryService.GetSummaries(1);

            Assert.Equal(1, summary.FirstOrDefault(s => s.User.Id == user1.Id).WeekSummary.Place);
            Assert.Equal(1, summary.FirstOrDefault(s => s.User.Id == user1.Id).SeasonSummary.Place);
            Assert.Equal(3, summary.FirstOrDefault(s => s.User.Id == user2.Id).WeekSummary.Place);
            Assert.Equal(3, summary.FirstOrDefault(s => s.User.Id == user2.Id).SeasonSummary.Place);
            Assert.Equal(1, summary.FirstOrDefault(s => s.User.Id == user3.Id).WeekSummary.Place);
            Assert.Equal(1, summary.FirstOrDefault(s => s.User.Id == user3.Id).SeasonSummary.Place);
        }

        [Fact]
        public async Task GetWeekWinner_WeekIsFinished_ReturnsCorrectWinner()
        {
            InitializeDb();
            await CreateUserAsync();
            await CreateUserAsync();
            CreateGames(1, 5, GameResult.HomeWin);
            await MakeUserPicksAsync(1, 1, GameResult.HomeWin);
            await MakeUserPicksAsync(2, 1, GameResult.AwayWin);

            var winner = _scoreSummaryService.GetWeekWinner(1);
            Assert.NotNull(winner);
            Assert.Equal(1, winner.Id);
        }

        [Fact]
        public async Task GetWeekWinner_WeekNotFinished_ReturnsNull()
        {
            InitializeDb();
            await CreateUserAsync();
            await CreateUserAsync();
            CreateGames(1, 5, GameResult.NotPlayed);
            await MakeUserPicksAsync(1, 1, GameResult.HomeWin);
            await MakeUserPicksAsync(2, 1, GameResult.AwayWin);

            var winner = _scoreSummaryService.GetWeekWinner(1);
            Assert.Null(winner);
        }

        [Fact]
        public async Task GetWeekWinner_WeekWinnerNotSameAsSeasonWinner_ReturnsCorrectWinner()
        {
            InitializeDb();
            var user1 = await CreateUserAsync();
            var user2 = await CreateUserAsync();
            CreateGames(1, 5, GameResult.HomeWin);
            CreateGames(2, 5, GameResult.HomeWin);
            CreateGames(3, 5, GameResult.HomeWin);

            // user 1 wins weeks 1 and 2 and will be season leader
            await MakeUserPicksAsync(user1.Id, 1, GameResult.HomeWin);
            await MakeUserPicksAsync(user1.Id, 2, GameResult.HomeWin);
            await MakeUserPicksAsync(user1.Id, 3, GameResult.AwayWin);

            // user 2 wins week 3
            await MakeUserPicksAsync(user2.Id, 1, GameResult.AwayWin);
            await MakeUserPicksAsync(user2.Id, 2, GameResult.AwayWin);
            await MakeUserPicksAsync(user2.Id, 3, GameResult.HomeWin);

            Assert.Equal(user1.Id, _scoreSummaryService.GetWeekWinner(1).Id);
            Assert.Equal(user1.Id, _scoreSummaryService.GetWeekWinner(2).Id);
            Assert.Equal(user2.Id, _scoreSummaryService.GetWeekWinner(3).Id);
        }

        private void InitializeDb()
        {
            _dbContext.Database.EnsureDeleted();
        }

        private async Task<PickEmLeagueDatabase.Entities.User> CreateUserAsync()
        {
            return await _userRepository.CreateAsync();
        }

        private void CreateGames(int week, int gameAmount, GameResult gameResult = GameResult.NotPlayed)
        {
            for (int i = 0; i < gameAmount; i++)
            {
                _gameRepository.CreateAsync(new PickEmLeagueDatabase.Entities.Game()
                {
                    Week = week,
                    AwayTeamId = (i * 2),
                    HomeTeamId = (i * 2) + 1,
                    GameResult = gameResult
                });
            }
        }

        private async Task MakeUserPicksAsync(long user, int week, GameResult result)
        {
            var gamePicks = _mapper.Map<List<GamePick>>((await _gamePickService.GetByUserAndWeekAsync(user, week)).ToList());

            foreach (var pick in gamePicks)
            {
                pick.Pick = result;
            }

            await _gamePickService.UpdateByUserAndWeekAsync(gamePicks);
        }
    }
}
