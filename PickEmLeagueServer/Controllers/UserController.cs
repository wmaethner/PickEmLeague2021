using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickEmLeagueServer.Database;
using PickEmLeagueServer.Models;

namespace PickEmLeagueServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {       
        private readonly ILogger<UserController> _logger;
        private readonly UserDatabase _database;

        public UserController(ILogger<UserController> logger, UserDatabase userDatabase)
        {
            _logger = logger;
            _database = userDatabase;
        }

        [HttpPost]
        public ActionResult<bool> Create(User user)
        {
            Console.WriteLine($"create new user {user.FirstName}");
            return _database.Create(user);           
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            Console.WriteLine("user get request");
            _logger.LogInformation("Get user list");
            return _database.Read();
        }

        [HttpGet("{id}")]
        public User Get(string id)
        {
            Console.WriteLine($"user get request with id {id}");
            return _database.Read(id);
        }
    }
}
