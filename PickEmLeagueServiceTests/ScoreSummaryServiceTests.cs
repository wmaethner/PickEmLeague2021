﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;
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

            _userRepository.CreateAsync();

            for (int week = 1; week <= 2; week++)
            {
                for (int i = 0; i < 5; i++)
                {
                    _gameRepository.CreateAsync(new PickEmLeagueDatabase.Entities.Game()
                    {
                        Week = week,
                        AwayTeamId = (i * 2),
                        HomeTeamId = (i * 2) + 1,
                        GameResult = PickEmLeagueShared.Enums.GameResult.HomeWin
                    });
                }
            }
        }

        [Fact]
        public void GetScoreSummary_UserHasntMadePicks_ZeroPoints()
        {
            var summary = _scoreSummaryService.GetScoreSummary(1);
            var userSummary = summary.ToList()[0];

            Assert.Equal(1, userSummary.User.Id);
            Assert.Equal(0, userSummary.WeekScore);
        }

        [Fact]
        public async Task GetScoreSummary_UserMadeCorrectPicks_CorrectScoreAsync()
        {
            var gamePicks = _mapper.Map<List<GamePick>>((await _gamePickService.GetByUserAndWeekAsync(1, 2)).ToList());

            foreach (var pick in gamePicks)
            {
                pick.Pick = PickEmLeagueShared.Enums.GameResult.HomeWin;
            }

            await _gamePickService.UpdateByUserAndWeekAsync(gamePicks);

            var summary = _scoreSummaryService.GetScoreSummary(2);
            var userSummary = summary.ToList()[0];

            Assert.Equal(1, userSummary.User.Id);
            Assert.Equal(15, userSummary.WeekScore);
            Assert.Equal(15, userSummary.SeasonScore);
        }
    }
}