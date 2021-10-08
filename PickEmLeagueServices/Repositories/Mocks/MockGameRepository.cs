using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.Repositories.Mocks
{
    public class MockGameRepository : MockCrudRepository<Game, PickEmLeagueModels.Models.Game>, IGameRepository
    {
        public MockGameRepository(IMapper mapper) : base(mapper)
        {
        }

        public IEnumerable<Game> GetByWeek(int week)
        {
            return GetQueryable().Where(g => g.Week == week).ToList();
        }
    }
}
