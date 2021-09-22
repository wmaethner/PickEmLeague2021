using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.DomainServices.Implementations
{
    public class GamePickService : IGamePickService
    {
        private readonly IGamePickRepository _gamePickRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GamePickService(IGamePickRepository gamePickRepository,
                               IGameRepository gameRepository,
                               IUserRepository userRepository,
                               IMapper mapper)
        {
            _gamePickRepository = gamePickRepository;
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IEnumerable<GamePick> GetByUser(long userId)
        {
            return _mapper.Map<IEnumerable<GamePick>>(_gamePickRepository.GetByUser(userId));
        }

        public IEnumerable<GamePick> GetByWeek(int week)
        {
            return _mapper.Map<IEnumerable<GamePick>>(_gamePickRepository.GetByWeek(week));
        }

        public async Task<IEnumerable<GamePick>> GetByUserAndWeekAsync(long userId, int week)
        {
            var user = await _userRepository.GetById(userId);
            if (user == null)
            {
                return new List<GamePick>();
            }

            var games = _gameRepository.GetByWeek(week).ToList();
            var currentPicks = _mapper.Map<IEnumerable<GamePick>>(
                _gamePickRepository.GetByUserAndWeek(userId, week)).ToList();

            // Ensure picks are created first
            await EnsurePicksExistAsync(games, user, week);
            await SortAndFillPickWagersAsync(games, userId, week);

            return _mapper.Map<IEnumerable<GamePick>>(
                _gamePickRepository.GetByUserAndWeek(userId, week));
        }

        public async Task<bool> UpdateByUserAndWeekAsync(IEnumerable<GamePick> gamePicks)
        {
            //TODO: Validate picks??

            foreach (var pick in gamePicks)
            {
                await _gamePickRepository.UpdateAsync(pick);
            }
            return true;
        }

        private async Task EnsurePicksExistAsync(List<PickEmLeagueDatabase.Entities.Game> games,
            PickEmLeagueDatabase.Entities.User user, int week)
        {
            var currentPicks = _mapper.Map<IEnumerable<GamePick>>(
                _gamePickRepository.GetByUserAndWeek(user.Id, week)).ToList();

            foreach (var game in games)
            {
                if (currentPicks.Find(p => p.Game.Id == game.Id) == null)
                {
                    await _gamePickRepository.CreateAsync(game, user);
                }
            }
        }

        private async Task SortAndFillPickWagersAsync(List<PickEmLeagueDatabase.Entities.Game> games,
            long userId, int week)
        {
            GamePick[] finalPicks = new GamePick[games.Count];
            List<GamePick> picksToFinalize = new List<GamePick>();

            var currentPicks = _mapper.Map<IEnumerable<GamePick>>(
                _gamePickRepository.GetByUserAndWeek(userId, week)).ToList();

            // remove zero wagers
            picksToFinalize = currentPicks.Where(gp => gp.Wager == 0).ToList();
            picksToFinalize.ForEach(pick => currentPicks.Remove(pick));

            // fill in wagers from remaining current picks
            foreach (var pick in currentPicks)
            {
                if (finalPicks[pick.Wager - 1] != null)
                {
                    picksToFinalize.Add(pick);
                }
                else
                {
                    finalPicks[pick.Wager - 1] = pick;
                }
            }

            // Fill in from picks to finalize and set their wager value
            foreach (var pick in picksToFinalize)
            {
                for (int i = 0; i < finalPicks.Length; i++)
                {
                    if (finalPicks[i] == null)
                    {
                        pick.Wager = i + 1;
                        finalPicks[i] = pick;
                        break;
                    }
                }
            }

            foreach (var pick in finalPicks)
            {
                await _gamePickRepository.UpdateAsync(pick);
            }
        }
    }
}
