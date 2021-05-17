using AutoMapper;
using PickEmLeague.Global.Shared;
using PickEmLeagueDatabase.Interfaces;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueServices.Services
{
    [DIServiceScope(typeof(IUserService), typeof(UserService))]
    public class UserService : BaseService<User, PickEmLeagueDatabase.Entities.User>, IUserService
    {
        public UserService(IUserRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }    
    }
}
