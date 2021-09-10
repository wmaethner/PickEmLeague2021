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
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGamePickService _gamePickService;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gamerepository, IUserRepository userRepository,
            IGamePickService gamePickService, IMapper mapper)
        {
            _gameRepository = gamerepository;
            _userRepository = userRepository;
            _gamePickService = gamePickService;
            _mapper = mapper;
        }

        public async Task<Game> CreateForWeek(int week)
        {
            return _mapper.Map<Game>(
                await _gameRepository.CreateAsync(
                    new PickEmLeagueDatabase.Entities.Game() { Week = week }));
        }

        public IEnumerable<Game> GetForWeek(int week)
        {
            return _mapper.Map<IEnumerable<Game>>(_gameRepository.GetByWeek(week));
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
