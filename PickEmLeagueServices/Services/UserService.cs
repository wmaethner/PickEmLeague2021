using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickEmLeagueDatabase;
using PickEmLeagueDatabase.Models;


namespace PickEmLeagueServices.Services
{
    public class UserService
    {
        DatabaseContext _dbContext;

        public UserService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> AddUser(User user)
        {
            User newUser = _dbContext.Users.Add(user).Entity;

            await _dbContext.SaveChangesAsync();

            return newUser;
        }

        public IEnumerable<User> GetUsers()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUser(Guid guid)
        {
            return _dbContext.Users.SingleOrDefault(u => u.Guid == guid);
        }
    }
}
