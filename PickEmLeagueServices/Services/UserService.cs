using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PickEmLeagueDatabase.Interfaces;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueServices.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IMapper _mapper;

        public UserService(IUserRepository dbContext, IMapper mapper)
        {
            _userRepository = dbContext;
            _mapper = mapper;
        }

        public async Task<User> AddUser(string email, string firstName, string lastName)
        {
            PickEmLeagueDatabase.Entities.User user = new PickEmLeagueDatabase.Entities.User()
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                Guid = Guid.NewGuid(),
            };

            await _userRepository.AddUser(user);

            return _mapper.Map<User>(user);
        }

        public async Task<List<User>> GetUsers()
        {
            return _mapper.Map<List<User>>((await _userRepository.GetUsersAsync()).ToList());
        }

        public async Task<User> GetUser(Guid guid)
        {        
            return _mapper.Map<User>(await _userRepository.GetUserAsync(guid));
        }

        public async Task<User> UpdateUser(User user)
        {
            // RDS Implementation
            //var entity = _userRepository.GetUserAsync(user.Guid);
            //_mapper.Map(user, entity);
            //await _userRepository.SaveChangesAsync();

            // DynamoDB Implementation
            var entity = await _userRepository.GetUserAsync(user.Guid);
            await _userRepository.DeleteUser(entity);
            _mapper.Map(user, entity);
            await _userRepository.AddUser(entity);

            return user;
        }
    }
}
