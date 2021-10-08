using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
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

        public ScoreSummaryServiceTests()
        {
            var serviceProvider = ServiceHelper.BuildUnitTestServices("scoreSummaryTests");

            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _gameRepository = serviceProvider.GetRequiredService<IGameRepository>();
            _gamePickService = serviceProvider.GetRequiredService<IGamePickService>();
            _scoreSummaryService = serviceProvider.GetRequiredService<IScoreSummaryService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
        }

        [Fact]
        public async Task GetWeekSummaries_UserHasntMadePicks_ZeroPointsAndCorrectStatusAsync()
        {
            InitializeDb();
            await CreateUserAsync();
            CreateGames(1, 5, GameResult.HomeWin);
            CreateGames(2, 5, GameResult.HomeWin);

            var summary = _scoreSummaryService.GetWeekSummaries(1);
            var userSummary = summary.ToList()[0];

            Assert.Equal(1, userSummary.User.Id);
            Assert.Equal(0, userSummary.Score);
            Assert.Equal(WeekPickStatus.NotPicked, userSummary.PickStatus);
        }

        [Fact]
        public async Task GetScoreSummary_UserMadeCorrectPicks_CorrectScoreAsync()
        {
            InitializeDb();
            await CreateUserAsync();
            CreateGames(1, 5, GameResult.HomeWin);
            CreateGames(2, 5, GameResult.HomeWin);
            await MakeUserPicksAsync(1, 2, GameResult.HomeWin);
            
            var weekSummary = _scoreSummaryService.GetWeekSummaries(2);
            var seasonSummary = _scoreSummaryService.GetSeasonSummaries();
            var userWeekSummary = weekSummary.ToList()[0];
            var userSeasonSummary = seasonSummary.ToList()[0];

            Assert.Equal(1, userWeekSummary.User.Id);
            Assert.Equal(15, userWeekSummary.Score);
            Assert.Equal(WeekPickStatus.FullyPicked, userWeekSummary.PickStatus);

            Assert.Equal(1, userSeasonSummary.User.Id);
            Assert.Equal(15, userSeasonSummary.Score);          
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
            
            var weekSummary = _scoreSummaryService.GetWeekSummaries(1);
            var seasonSummary = _scoreSummaryService.GetSeasonSummaries();

            Assert.Equal(2, weekSummary.FirstOrDefault(s => s.User.Id == user1.Id).Place);
            Assert.Equal(2, seasonSummary.FirstOrDefault(s => s.User.Id == user1.Id).Place);

            Assert.Equal(1, weekSummary.FirstOrDefault(s => s.User.Id == user2.Id).Place);
            Assert.Equal(1, seasonSummary.FirstOrDefault(s => s.User.Id == user2.Id).Place);
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
            
            var weekSummary = _scoreSummaryService.GetWeekSummaries(1);
            var seasonSummary = _scoreSummaryService.GetSeasonSummaries();

            Assert.Equal(1, weekSummary.FirstOrDefault(s => s.User.Id == user1.Id).Place);
            Assert.Equal(1, seasonSummary.FirstOrDefault(s => s.User.Id == user1.Id).Place);

            Assert.Equal(3, weekSummary.FirstOrDefault(s => s.User.Id == user2.Id).Place);
            Assert.Equal(3, seasonSummary.FirstOrDefault(s => s.User.Id == user2.Id).Place);

            Assert.Equal(1, weekSummary.FirstOrDefault(s => s.User.Id == user3.Id).Place);
            Assert.Equal(1, seasonSummary.FirstOrDefault(s => s.User.Id == user3.Id).Place);
        }

        [Fact]
        public async Task GetScoreSummary_MultipleUsersWithMissedWeek_Scores10LessThanMinimum()
        {
            InitializeDb();
            var user1 = await CreateUserAsync();
            var user2 = await CreateUserAsync();
            var user3 = await CreateUserAsync();
            CreateGames(1, 5, GameResult.HomeWin);

            await MakeUserPicksAsync(user1.Id, 1, GameResult.HomeWin);
            // Results in 11 points
            await MakeUserPicksAsync(user2.Id, 1, new GameResult[] { GameResult.HomeWin,
                GameResult.HomeWin,
                GameResult.HomeWin,
                GameResult.AwayWin,
                GameResult.HomeWin });

            user3.MissedWeeks.Add(1);
            // Assume they made some picks
            await MakeUserPicksAsync(user3.Id, 1, new GameResult[] { GameResult.HomeWin,
                GameResult.HomeWin,
                GameResult.AwayWin,
                GameResult.AwayWin,
                GameResult.AwayWin });
            await _userRepository.SaveAsync();

            var weekSummary = _scoreSummaryService.GetWeekSummaries(1);
            var seasonSummary = _scoreSummaryService.GetSeasonSummaries();

            Assert.Equal(15, weekSummary.FirstOrDefault(s => s.User.Id == user1.Id).Score);
            Assert.Equal(15, seasonSummary.FirstOrDefault(s => s.User.Id == user1.Id).Score);

            Assert.Equal(11, weekSummary.FirstOrDefault(s => s.User.Id == user2.Id).Score);
            Assert.Equal(11, seasonSummary.FirstOrDefault(s => s.User.Id == user2.Id).Score);

            Assert.Equal(1, weekSummary.FirstOrDefault(s => s.User.Id == user3.Id).Score);
            Assert.Equal(1, seasonSummary.FirstOrDefault(s => s.User.Id == user3.Id).Score);
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
            //_dbContext.Database.EnsureDeleted();
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

        private async Task MakeUserPicksAsync(long user, int week, int gamePickIndex, GameResult result)
        {
            var gamePicks = _mapper.Map<List<GamePick>>((await _gamePickService.GetByUserAndWeekAsync(user, week)).ToList());

            gamePicks[gamePickIndex].Pick = result;

            await _gamePickService.UpdateByUserAndWeekAsync(gamePicks);
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

        private async Task MakeUserPicksAsync(long user, int week, GameResult[] result)
        {
            var gamePicks = _mapper.Map<List<GamePick>>((await _gamePickService.GetByUserAndWeekAsync(user, week)).ToList());

            if (gamePicks.Count != result.Length)
            {
                throw new Exception("Different amounts for picks and results");
            }

            for (int i = 0; i < gamePicks.Count; i++)
            {
                gamePicks[i].Pick = result[i];
                gamePicks[i].Wager = i + 1;
            }

            await _gamePickService.UpdateByUserAndWeekAsync(gamePicks);
        }


    }
}
