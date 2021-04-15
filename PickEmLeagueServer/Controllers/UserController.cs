using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PickEmLeagueServer.Database;
using PickEmLeagueServer.Models;

namespace PickEmLeagueServer.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {       
        private readonly ILogger<UserController> _logger;
        //private readonly UserDatabase _database;
        private DBContext _dbContext;

        public UserController(ILogger<UserController> logger, DBContext dbContext)
        {
            _logger = logger;
            //_database = userDatabase;
            _dbContext = dbContext;
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
             _dbContext.Users.Add(user);

            await _dbContext.SaveChangesAsync();
            return user;
            //return true;

            
            //return _database.Create(user);


        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            Console.WriteLine("user get request");
            _logger.LogInformation("Get user list");
            //return _database.Read();
            return _dbContext.Users.ToList();
        }

        [HttpGet("{id}")]
        public User Get([FromRoute] Guid id)
        {
            Console.WriteLine($"user get request with id {id}");
            //return _database.Read(id);
            return _dbContext.Users.SingleOrDefault(x => x.Guid == id);
        }
    }
}
