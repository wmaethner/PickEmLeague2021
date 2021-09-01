using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PickEmLeagueDatabase;
using PickEmLeagueModels.Models;

namespace PickEmLeague.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private PickEmLeagueDbContext _dbContext;
        private IMapper _mapper;

        public UserController(PickEmLeagueDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public void AddUser(User user)
        {
            _dbContext.Users.Add(_mapper.Map<PickEmLeagueDatabase.Entities.User>(user));
            _dbContext.SaveChanges();
        }

        [HttpGet]
        public IEnumerable<User> GetUsers()
        {
            //return new List<User>()
            //{
            //    new User(){ Id = 1, Name = "One Name", Email = "one@test.com"},
            //    new User(){ Id = 2, Name = "Two Name", Email = "two@test.com"}
            //};

            _dbContext.Users.Add(new PickEmLeagueDatabase.Entities.User() { Name = "test" });
            _dbContext.SaveChanges();

            var list = _dbContext.Users.ToList();

            return _mapper.Map<List<User>>(list);
        }
    }
}
