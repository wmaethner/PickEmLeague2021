using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PickEmLeague.Global.Shared;
using PickEmLeagueDatabase.Interfaces;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueServices.Services
{
    [DIServiceScope(typeof(IGamePickService), typeof(GamePickService))]
    public class GamePickService : BaseService<GamePick, PickEmLeagueDatabase.Entities.GamePick>, IGamePickService
    {
        IMapper _mapper;
        IGamePickRepository _repository;
        IGameRepository _gameRepository;

        public GamePickService(IGamePickRepository repository, IGameRepository gameRepository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public Task<IEnumerable<GamePick>> GetUsersGamePicks(User user)
        {
            return GetUsersGamePicks(user.Id);
        }

        public async Task<IEnumerable<GamePick>> GetUsersGamePicks(Guid userId)
        {
            return _mapper.Map<List<GamePick>>(await _repository.GetUsersGamePicks(userId));
        }

        public Task<IEnumerable<GamePick>> GetUsersWeekGamePicks(User user, int week)
        {
            return GetUsersWeekGamePicks(user.Id, week);
        }

        public async Task<IEnumerable<GamePick>> GetUsersWeekGamePicks(Guid userId, int week)
        {
            List<GamePick> usersPicks = (await GetUsersGamePicks(userId)).ToList();
            List<GamePick> usersWeekPicks = new List<GamePick>();

            foreach (var pick in usersPicks)
            {
                if ((await _gameRepository.Get(pick.Game)).Week == week)
                {
                    usersWeekPicks.Add(pick);
                } 
            }

            return usersWeekPicks;
        }
    }
}
