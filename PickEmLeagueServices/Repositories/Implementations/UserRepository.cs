using System;
using AutoMapper;
using PickEmLeagueDatabase;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.Repositories.Implementations
{
    public class UserRepository : CrudRepository<User, PickEmLeagueModels.Models.User>, IUserRepository
    {
        public UserRepository(PickEmLeagueDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}
