using System;
using AutoMapper;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.DomainServices.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AuthenticationService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public User AttempLogin(string email, string passwordHash)
        {
            var user = _userRepository.GetByEmail(email);

            if (user?.PasswordHash == passwordHash)
            {
                return _mapper.Map<User>(user);
            }

            return null;
        }
    }
}
