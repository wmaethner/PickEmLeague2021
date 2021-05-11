using Microsoft.AspNetCore.Mvc;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : CoreController<User, PickEmLeagueDatabase.Entities.User, IUserService>
    {       
        public UserController(IUserService userService) : base(userService)
        {
        }     
    }
}
