using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueDatabase.Entities;

namespace PickEmLeagueDatabase.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task DeleteUser(User user);

        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(Guid guid);

        Task SaveChangesAsync();
    }
}
