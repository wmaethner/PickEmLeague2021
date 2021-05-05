using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueDatabase.Interfaces;

namespace PickEmLeagueDatabase.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IDatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<User> AddUser(User user)
        {
            if (GetUser(user.Guid) != null)
            {
                throw new Exception("User already exists");
            }

            await Context.Create(user);

            return user;
        }

        public User GetUser(Guid guid)
        {
            return Context.GetQueryable<User>().SingleOrDefault(u => u.Guid == guid);
        }

        public IEnumerable<User> GetUsers()
        {
            return Context.GetQueryable<User>().ToList();
        }

        async Task IUserRepository.SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
