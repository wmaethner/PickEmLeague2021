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
            if ((await GetUserAsync(user.Id)) != null)
            {
                throw new Exception("User already exists");
            }

            await Context.Create(user);

            return user;
        }

        public async Task DeleteUser(User user)
        {
            await Context.Delete<User>(user.Id);
        }

        public async Task<User> GetUserAsync(Guid guid)
        {
            return (await Context.GetQueryableAsync<User>()).SingleOrDefault(u => u.Id == guid);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return (await Context.GetQueryableAsync<User>()).ToList();
        }

        async Task IUserRepository.SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
