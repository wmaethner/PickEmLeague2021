using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PickEmLeagueDatabase.Interfaces;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueServices.Services
{
    public class GameService : BaseService<Game, PickEmLeagueDatabase.Entities.Game>, IGameService
    {
        IGameRepository _repository;
        IMapper _mapper;

        public GameService(IGameRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Game>> GetWeeksGames(int week)
        {
            return _mapper.Map<List<Game>>(await _repository.GetWeeksGames(week));
        }
    }
}
