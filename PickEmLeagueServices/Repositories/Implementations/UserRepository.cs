using System;
using System.Linq;
using System.Threading.Tasks;
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

        public User GetByEmail(string email)
        {
            return GetQueryable().FirstOrDefault(u => u.Email == email);
        }
    }
}
