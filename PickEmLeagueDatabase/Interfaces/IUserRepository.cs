using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueDatabase.Entities;

namespace PickEmLeagueDatabase.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);

        IEnumerable<User> GetUsers();
        User GetUser(Guid guid);

        Task SaveChangesAsync();
    }
}
