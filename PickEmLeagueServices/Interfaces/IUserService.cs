using PickEmLeagueDomain.Models;

namespace PickEmLeagueServices.Interfaces
{
    public interface IUserService : IBaseService<User, PickEmLeagueDatabase.Entities.User>
    {
    }
}
