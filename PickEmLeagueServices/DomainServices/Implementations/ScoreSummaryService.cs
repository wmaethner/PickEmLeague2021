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

        public IEnumerable<UserSummary> GetSummaries(int week)
        {
            var summaries = new List<UserSummary>();
            var users = _userRepository.GetAll().ToList();

            foreach (var user in _mapper.Map<IEnumerable<User>>(users))
            {
                summaries.Add(GetUserSummary(user, week));
            }

            return SortSummaries(summaries);
        }

        //TODO: Handle ties
        public User GetWeekWinner(int week)
        {
            if (week < 1 || !_gameService.IsWeekDone(week))
            {
                return null;
            }

            var summaries = GetSummaries(week);
            return summaries.ToList().FirstOrDefault(s => s.WeekSummary.Place == 1).User;
        }

        private UserSummary GetUserSummary(User user, int week)
        {
            var summary = new UserSummary() { User = user };
            var picks = _gamePickRepository.GetByUser(user.Id);

            bool madePick = false;
            bool missedPick = false;

            foreach (var pick in picks)
            {
                if (CorrectPick(pick.Pick, pick.Game))
                {
                    summary.SeasonSummary.SeasonScore += pick.Wager;
                    summary.SeasonSummary.CorrectPicks++;
                    if (pick.Game.Week == week)
                    {
                        summary.WeekSummary.WeekScore += pick.Wager;
                        summary.WeekSummary.CorrectPicks++;
                    }
                }

                if (pick.Game.Week == week)
                {
                    if (pick.Pick == GameResult.NotPlayed)
                    {
                        missedPick = true;
                    }
                    else
                    {
                        madePick = true;
                    }
                }
            }

            summary.WeekSummary.WeekPickStatus =
                (madePick) ? (missedPick ? WeekPickStatus.PartiallyPicked :
                                           WeekPickStatus.FullyPicked)
                           : WeekPickStatus.NotPicked;

            return summary;
        }


        private bool CorrectPick(GameResult usersPick, PickEmLeagueDatabase.Entities.Game game)
        {
            if (usersPick != GameResult.NotPlayed)
            {
                return game.GameResult == usersPick;
            }
            return false;
        }

        private List<UserSummary> SortSummaries(List<UserSummary> summaries)
        {
            //TODO: Genericize this, maybe week and season summary derive from a base class that both
            //      contain a score and place?

            // Set week places
            var sorted = summaries.OrderByDescending(x => x.WeekSummary.WeekScore).ThenBy(y => y.User.Name).ToList();

            sorted[0].WeekSummary.Place = 1;

            for (int i = 1; i < sorted.Count(); i++)
            {
                // If same score as previous user then set to the same place
                // else set to i + 1 (0th index = 1st place, 1st index = 2nd place ...)
                sorted[i].WeekSummary.Place =
                    sorted[i - 1].WeekSummary.WeekScore == sorted[i].WeekSummary.WeekScore ?
                    sorted[i - 1].WeekSummary.Place : i + 1;
            }

            // Set season places
            sorted = sorted.OrderByDescending(x => x.SeasonSummary.SeasonScore).ThenBy(y => y.User.Name).ToList();

            sorted[0].SeasonSummary.Place = 1;

            for (int i = 1; i < sorted.Count(); i++)
            {
                // If same score as previous user then set to the same place
                // else set to i + 1 (0th index = 1st place, 1st index = 2nd place ...)
                sorted[i].SeasonSummary.Place =
                    sorted[i - 1].SeasonSummary.SeasonScore == sorted[i].SeasonSummary.SeasonScore ?
                    sorted[i - 1].SeasonSummary.Place : i + 1;
            }

            return sorted;
        }
    }
}
