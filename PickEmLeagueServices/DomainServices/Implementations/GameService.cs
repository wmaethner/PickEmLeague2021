using System;
using System.Linq;
using System.Threading.Tasks;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.DomainServices.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGamePickService _gamePickService;

        public GameService(IGameRepository gamerepository, IUserRepository userRepository, IGamePickService gamePickService)
        {
            _gameRepository = gamerepository;
            _userRepository = userRepository;
            _gamePickService = gamePickService;
        }

        public async Task<bool> DeleteGame(long id)
        {
            //Need to adjust user pick wagers for the week of this game
            var users = _userRepository.GetAll();
            var game = await _gameRepository.GetById(id); 

            // Ensure picks are created then update the wagers above the game deleted (shift down)
            foreach (var user in users)
            {
                var picks = (await _gamePickService.GetByUserAndWeekAsync(user.Id, game.Week)).ToList();
                var wagerToDelete = picks.Find(p => p.GameId == id).Wager;
                var ordered = picks.OrderBy(p => p.Wager);

                for (int index = wagerToDelete; index < ordered.Count(); index++)
                {
                    picks[index].Wager = index;
                }

                await _gamePickService.UpdateByUserAndWeekAsync(picks);
            }

            await _gameRepository.DeleteAsync(id);

            return true;
        }
    }
}
