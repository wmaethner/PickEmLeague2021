using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickEmLeagueDatabase.Models;
using PickEmLeagueServices.Services;

namespace PickEmLeagueServer.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {       
        private readonly ILogger<UserController> _logger;
        private UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<User> CreateAsync(string email, string firstName, string lastName)
        {
            Console.WriteLine($"create new user {firstName}");
            User user = new User()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };
            Console.WriteLine("Adding  user " + user);
            await _userService.AddUser(user);
            return user;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            Console.WriteLine("user get request");
            _logger.LogInformation("Get user list");

            return _userService.GetUsers();
        }

        [HttpGet("{id}")]
        public User Get([FromRoute] Guid id)
        {
            Console.WriteLine($"user get request with id {id}");
            User user = _userService.GetUser(id);
            if (user == null)
            {
                throw new Exception($"Null user for id {id}");
            }
            return user;
        }
    }
}
