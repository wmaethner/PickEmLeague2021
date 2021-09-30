using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PickEmLeagueModels.Models;
using PickEmLeagueModels.Models.Stats;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.DomainServices.Implementations
{
    public class StatsService : IStatsService
    {
        private readonly IScoreSummaryService _scoreSummaryService;
        private readonly IUserRepository _userRepository;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public StatsService(IScoreSummaryService scoreSummaryService,
            IGameService gameService, IUserRepository userRepository,
            IMapper mapper)
        {
            _scoreSummaryService = scoreSummaryService;
            _userRepository = userRepository;
            _gameService = gameService;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AverageScores>> GetAverageScoresAsync()
        {
            List<AverageScores> averages = new List<AverageScores>();
            Dictionary<long, double> scores = new Dictionary<long, double>();
            int weekCount = 0;

            for (int week = 1; week <= 18; week++)
            {
                if (!_gameService.IsWeekDone(week))
                {
                    break;
                }

                var summaries = _scoreSummaryService.GetSummaries(week);
                foreach (var item in summaries)
                {
                    if (!scores.ContainsKey(item.User.Id))
                    {
                        scores[item.User.Id] = 0;
                    }
                    scores[item.User.Id] += item.WeekSummary.WeekScore;
                }
                weekCount++;
            }

            foreach (var item in scores)
            {
                averages.Add(new AverageScores()
                {
                    User = _mapper.Map<User>(await _userRepository.GetById(item.Key)),
                    Score = item.Value / weekCount
                });
            }

            return averages;

            //return await scores.Select(async (KeyValuePair<long, double> arg) =>
            //    new AverageScores() {
            //        User = _mapper.Map<User>(await _userRepository.GetById(arg.Key)),
            //        Score = arg.Value / weekCount })
            //    .ToList();
        }
    }
}
