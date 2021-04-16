using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PickEmLeagueDatabase.Models;
using PickEmLeagueServer.Database;

namespace PickEmLeagueServices.Services
{
    public class UserService
    {
        DBContext _dbContext;

        public UserService(DBContext dbContext)
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
