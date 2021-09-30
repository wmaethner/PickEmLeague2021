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
    public class StatsServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IGamePickService _gamePickService;
        private readonly IMapper _mapper;
        private readonly IScoreSummaryService _scoreSummaryService;
        private readonly IStatsService _statsService;
        private readonly PickEmLeagueDbContext _dbContext;

        public StatsServiceTests()
        {
            var serviceProvider = ServiceHelper.BuildUnitTestServices("statsServiceTests");

            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _gameRepository = serviceProvider.GetRequiredService<IGameRepository>();
            _gamePickService = serviceProvider.GetRequiredService<IGamePickService>();
            _scoreSummaryService = serviceProvider.GetRequiredService<IScoreSummaryService>();
            _statsService = serviceProvider.GetRequiredService<IStatsService>();
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _dbContext = serviceProvider.GetRequiredService<PickEmLeagueDbContext>();
        }


        [Fact]
        public async Task GetAverageScores_CorrectPicks_ReturnsCorrectScores()
        {
            InitializeDb();
            var user = await CreateUserAsync();
            CreateGames(1, 5, GameResult.HomeWin);
            CreateGames(2, 5, GameResult.HomeWin);
            await MakeUserPicksAsync(user.Id, 1, GameResult.HomeWin);
            await MakeUserPicksAsync(user.Id, 2, GameResult.HomeWin);

            var averages = (await _statsService.GetAverageScoresAsync()).ToList();

            Assert.Single(averages);
            Assert.Equal(user.Id, averages[0].User.Id);
            Assert.Equal(15, averages[0].Score);
        }

        [Fact]
        public async Task GetAverageScores_HalfCorrectPicks_ReturnsCorrectScores()
        {
            InitializeDb();
            var user = await CreateUserAsync();
            CreateGames(1, 5, GameResult.HomeWin);
            CreateGames(2, 5, GameResult.HomeWin);
            await MakeUserPicksAsync(user.Id, 1, GameResult.HomeWin);
            await MakeUserPicksAsync(user.Id, 2, GameResult.AwayWin);

            var averages = (await _statsService.GetAverageScoresAsync()).ToList();

            Assert.Single(averages);
            Assert.Equal(user.Id, averages[0].User.Id);
            Assert.Equal(7.5, averages[0].Score);
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
