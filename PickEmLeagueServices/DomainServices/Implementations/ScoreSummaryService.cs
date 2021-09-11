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
        private readonly IMapper _mapper;

        public ScoreSummaryService(IUserRepository userRepository, IGamePickRepository gamePickRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _gamePickRepository = gamePickRepository;
            _mapper = mapper;
        }

        public IEnumerable<UserSummary> GetSummaries(int week)
        {
            var summaries = new List<UserSummary>();
            var users = _userRepository.GetAll().ToList();

            foreach (var user in _mapper.Map<List<User>>(users))
            {
                summaries.Add(GetUserSummary(user, week));
            }

            return summaries.OrderByDescending(x => x.WeekSummary.WeekScore).ThenBy(y => y.User.Name);
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


    }
}
