using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickEmLeagueDomain.Models;
using PickEmLeagueDomain.Models.Requests;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueServer.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {       
        private readonly ILogger<UserController> _logger;
        private IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<User> CreateAsync([FromBody] CreateUserRequest request)
        {
            Console.WriteLine($"create new user {request.FirstName}");
            User user = await _userService.AddUser(request.Email, request.FirstName, request.LastName);
            return user;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync()
        {
            Console.WriteLine("user get request");
            _logger.LogInformation("Get user list");

            return await _userService.GetUsers();
        }

        [HttpGet("{id}")]
        public async Task<User> GetAsync([FromRoute] Guid id)
        {
            Console.WriteLine($"user get request with id {id}");
            User user = await _userService.GetUser(id);
            if (user == null)
            {
                throw new Exception($"Null user for id {id}");
            }
            return user;
        }

        [HttpPut]
        public async Task<User> Update([FromBody] User user)
        {
            return await _userService.UpdateUser(user);
        }
    }
}
