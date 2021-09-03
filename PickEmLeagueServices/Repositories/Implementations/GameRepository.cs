using System;
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
    }
}
