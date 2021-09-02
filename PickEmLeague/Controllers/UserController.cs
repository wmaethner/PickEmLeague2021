using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost("create-user")]
        public User AddUser()
        {
            User user = new User();

            var entity = _dbContext.Users.Add(_mapper.Map<PickEmLeagueDatabase.Entities.User>(user));
            _dbContext.SaveChanges();
            return _mapper.Map<User>(entity.Entity);
        }

        [HttpGet("get-all-users")]
        public IEnumerable<User> GetUsers()
        {
            var list = _dbContext.Users.ToList();
            return _mapper.Map<List<User>>(list);
        }

        [HttpGet("get-user")]
        public User GetUser(long id)
        {
            return _mapper.Map<User>(_dbContext.Find<PickEmLeagueDatabase.Entities.User>(id));
        }

        [HttpPut("update-user")]
        public void EditUserAsync(User user)
        {
            var entity = _dbContext.Find<PickEmLeagueDatabase.Entities.User>(user.Id);
            _mapper.Map(user, entity);
            _dbContext.SaveChanges();
        }

        [HttpDelete]
        public void DeleteUser(long id)
        {
            var entity = _dbContext.Find<PickEmLeagueDatabase.Entities.User>(id);
            _dbContext.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
