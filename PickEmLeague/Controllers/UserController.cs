using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeague.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : CrudController<PickEmLeagueDatabase.Entities.User, User>
    {
        public UserController(IUserRepository repository, IMapper mapper, ILogger<UserController> logger) :
            base(repository, mapper, logger)
        {
        }
    }
}
