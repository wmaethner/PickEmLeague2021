using System;
using System.Threading.Tasks;
using PickEmLeagueDatabase.Entities;

namespace PickEmLeagueServices.Repositories.Interfaces
{
    public interface IUserRepository : ICrudRepository<User, PickEmLeagueModels.Models.User>
    {
        User GetByEmail(string email);
    }
}
