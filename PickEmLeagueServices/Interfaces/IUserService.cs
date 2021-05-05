using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PickEmLeagueDomain.Models;

namespace PickEmLeagueServices.Interfaces
{
    public interface IUserService
    {
        Task<User> AddUser(string email, string firstName, string lastName);

        Task<List<User>> GetUsers();
        Task<User> GetUser(Guid guid);

        Task<User> UpdateUser(User user);
    }
}
