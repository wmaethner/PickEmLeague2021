using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;
using PickEmLeagueShared.Enums;

namespace PickEmLeagueServices.DomainServices.Implementations
{
    public class ScoreSummaryService : IScoreSummaryService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGamePickRepository _gamePickRepository;
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public ScoreSummaryService(IUserRepository userRepository, IGamePickRepository gamePickRepository,
            IGameService gameService, IMapper mapper)
        {
            _userRepository = userRepository;
            _gamePickRepository = gamePickRepository;
            _gameService = gameService;
            _mapper = mapper;
        }

        //TODO: Handle ties
        public User GetWeekWinner(int week)
        {
            if (week < 1 || !_gameService.IsWeekDone(week))
            {
                return null;
            }

            var summaries = GetWeekSummaries(week);
            return summaries.ToList().FirstOrDefault(s => s.Place == 1).User;
        }

        public List<WeekSummary> GetWeekSummaries(int week)
        {
            var summaries = new List<WeekSummary>();
            var users = _mapper.Map<IEnumerable<User>>(_userRepository.GetAll().ToList());

            foreach (var user in users)
            {
                var summary = new WeekSummary(user, week);
                var picks = _gamePickRepository.GetByUserAndWeek(user.Id, week);
                bool madePick = false;
                bool missedPick = false;

                foreach (var pick in picks)
                {
                    if (CorrectPick(pick.Pick, pick.Game))
                    {
                        summary.Score += pick.Wager;
                        summary.CorrectPicks++;
                    }

                    missedPick = pick.Pick == GameResult.NotPlayed ? true : missedPick;
                    madePick = pick.Pick == GameResult.NotPlayed ? madePick : true;
                }

                summary.PickStatus = (madePick) ? (missedPick ? WeekPickStatus.PartiallyPicked :
                                                WeekPickStatus.FullyPicked)
                                : WeekPickStatus.NotPicked;

                summaries.Add(summary);
            }

            HandleMissedWeek(summaries, week);

            SetBaseSummaryPlaces(summaries);

            return summaries;
        }

        public List<SeasonSummary> GetSeasonSummaries()
        {
            List<SeasonSummary> summaries = new List<SeasonSummary>();
            var users = _mapper.Map<IEnumerable<User>>(_userRepository.GetAll().ToList());

            foreach (var user in users)
            {
                var summary = new SeasonSummary(user);
                summaries.Add(summary);
            }

            for (int week = 1; week <= 18; week++)
            {
                var weekSummaries = GetWeekSummaries(week);

                foreach (var weekSummary in weekSummaries)
                {
                    var seasonSummary = summaries.First(s => s.User.Id == weekSummary.User.Id);

                    seasonSummary.Score += weekSummary.Score;
                    seasonSummary.CorrectPicks += weekSummary.CorrectPicks;
                }
            }

            SetBaseSummaryPlaces(summaries);

            return summaries;
        }

        private void HandleMissedWeek(List<WeekSummary> weekSummaries, int week)
        {
            bool userHasMissedWeek = false;

            foreach (var user in weekSummaries.Select(x => x.User))
            {
                userHasMissedWeek = user.MissedWeeks.Contains(week);

                if (userHasMissedWeek)
                {
                    break;
                }
            }

            if (userHasMissedWeek)
            {
                var min = weekSummaries.Where(s => !s.User.MissedWeeks.Contains(week))
                                       .Select(s => s.Score)
                                       .Min();

                foreach (var summary in weekSummaries.Where(s => s.User.MissedWeeks.Contains(week)))
                {
                    summary.Score = min - 10;
                }
            }
        }

        private void SetBaseSummaryPlaces(IEnumerable<IBaseSummary> summaries)
        {
            var sortedList = summaries.OrderByDescending(x => x.Score).ThenBy(y => y.User.Name).ToList();

            sortedList[0].Place = 1;

            for (int i = 1; i < summaries.Count(); i++)
            {
                // If same score as previous user then set to the same place
                // else set to i + 1 (0th index = 1st place, 1st index = 2nd place ...)
                sortedList[i].Place =
                    sortedList[i - 1].Score == sortedList[i].Score ?
                    sortedList[i - 1].Place : i + 1;
            }
        }

        private bool CorrectPick(GameResult usersPick, PickEmLeagueDatabase.Entities.Game game)
        {
            if (usersPick != GameResult.NotPlayed)
            {
                return game.GameResult == usersPick;
            }
            return false;
        }
    }
}
