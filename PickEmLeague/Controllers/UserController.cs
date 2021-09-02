using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeague.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpPost("create-user")]
        public async Task<User> AddUserAsync()
        {
            return MapUser(await _userRepository.CreateAsync());
        }

        [HttpGet("get-all-users")]
        public IEnumerable<User> GetUsers()
        {
            return _mapper.Map<IEnumerable<User>>(_userRepository.GetAll());
        }

        [HttpGet("get-user")]
        public async Task<User> GetUserAsync(long id)
        {
            return MapUser(await _userRepository.GetAsync(id));
        }

        [HttpPut("update-user")]
        public async Task EditUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        [HttpDelete]
        public async Task DeleteUserAsync(long id)
        {
            await _userRepository.DeleteAsync(id);
        }

        private User MapUser(PickEmLeagueDatabase.Entities.User user)
        {
            return _mapper.Map<User>(user); 
        }
    }
}
