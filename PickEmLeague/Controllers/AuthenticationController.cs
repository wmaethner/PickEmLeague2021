using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PickEmLeagueModels.Models;
using PickEmLeagueModels.Models.Responses;
using PickEmLeagueServices.DomainServices.Interfaces;

namespace PickEmLeague.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("attemptLogin")]
        public LoginResponse Login(string email, string passwordHash)
        {
            var user = _authenticationService.AttempLogin(email, passwordHash);
            return new LoginResponse()
            {
                User = user,
                LoggedIn = (user != null)
            };
        }
    }
}
