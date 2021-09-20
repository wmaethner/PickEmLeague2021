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
            CreateUser();
            CreateGames(1, 5, GameResult.HomeWin);
            CreateGames(2, 5, GameResult.HomeWin);

            var summary = await _scoreSummaryService.GetSummariesAsync(1);
            var userSummary = summary.ToList()[0];

            Assert.Equal(1, userSummary.User.Id);
            Assert.Equal(0, userSummary.WeekSummary.WeekScore);
            Assert.Equal(WeekPickStatus.NotPicked, userSummary.WeekSummary.WeekPickStatus);
        }

        [Fact]
        public async Task GetScoreSummary_UserMadeCorrectPicks_CorrectScoreAsync()
        {
            InitializeDb();
            CreateUser();
            CreateGames(1, 5, GameResult.HomeWin);
            CreateGames(2, 5, GameResult.HomeWin);
            await MakeUserPicksAsync(1, 2, GameResult.HomeWin);
            
            var summary = await _scoreSummaryService.GetSummariesAsync(2);
            var userSummary = summary.ToList()[0];

            Assert.Equal(1, userSummary.User.Id);
            Assert.Equal(15, userSummary.WeekSummary.WeekScore);
            Assert.Equal(15, userSummary.SeasonSummary.SeasonScore);
            Assert.Equal(WeekPickStatus.FullyPicked, userSummary.WeekSummary.WeekPickStatus);
        }

        [Fact]
        public async Task GetWeekWinner_WeekIsFinished_ReturnsCorrectWinner()
        {
            InitializeDb();
            CreateUser();
            CreateUser();
            CreateGames(1, 5, GameResult.HomeWin);
            await MakeUserPicksAsync(1, 1, GameResult.HomeWin);
            await MakeUserPicksAsync(2, 1, GameResult.AwayWin);

            var winner = await _scoreSummaryService.GetWeekWinner(1);
            Assert.NotNull(winner);
            Assert.Equal(1, winner.Id);
        }

        [Fact]
        public async Task GetWeekWinner_WeekNotFinished_ReturnsNull()
        {
            InitializeDb();
            CreateUser();
            CreateUser();
            CreateGames(1, 5, GameResult.NotPlayed);
            await MakeUserPicksAsync(1, 1, GameResult.HomeWin);
            await MakeUserPicksAsync(2, 1, GameResult.AwayWin);

            var winner = await _scoreSummaryService.GetWeekWinner(1);
            Assert.Null(winner);
        }

        private void InitializeDb()
        {
            _dbContext.Database.EnsureDeleted();
        }

        private void CreateUser()
        {
            _userRepository.CreateAsync();
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

        private async Task MakeUserPicksAsync(int user, int week, GameResult result)
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
