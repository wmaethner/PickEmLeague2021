using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using PickEmLeagueDatabase;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.Repositories.Implementations
{
    public class GameRepository : CrudRepository<Game, PickEmLeagueModels.Models.Game>, IGameRepository
    {
        public GameRepository(PickEmLeagueDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public void ClearDb()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Game> GetByWeek(int week)
        {
            return GetQueryable().Where(g => g.Week == week).ToList();
        }
    }
}
