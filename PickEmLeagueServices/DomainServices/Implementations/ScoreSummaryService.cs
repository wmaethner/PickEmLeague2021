using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PickEmLeagueModels.Models;
using PickEmLeagueModels.Models.Responses;
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

        public IEnumerable<UsersWeeklyScoreSummary> GetScoreSummary(int week)
        {          
            var usersWeeklyScoreSummaries = new List<UsersWeeklyScoreSummary>();
            var users = _userRepository.GetAll().ToList();

            foreach (var user in _mapper.Map<List<User>>(users))
            {
                usersWeeklyScoreSummaries.Add(GetUsersWeeklyScoreSummary(user, week));
            }

            return usersWeeklyScoreSummaries;
        }

        private UsersWeeklyScoreSummary GetUsersWeeklyScoreSummary(User user, int week)
        {
            UsersWeeklyScoreSummary summary = new UsersWeeklyScoreSummary() { User = user };
            var picks = _gamePickRepository.GetByUser(user.Id);

            foreach (var pick in picks)
            {
                if (CorrectPick(pick.Pick, pick.Game))
                {
                    summary.SeasonScore += pick.Wager;
                    summary.WeekScore += (pick.Game.Week == week) ? pick.Wager : 0;
                }
            }

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
