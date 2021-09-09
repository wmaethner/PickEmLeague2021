using System;
using AutoMapper;
using PickEmLeagueModels.Models;
using PickEmLeagueModels.Models.Responses;
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

        public LoginResponse AttempLogin(string email, string passwordHash)
        {
            User model = null;
            var user = _userRepository.GetByEmail(email);

            if (user?.PasswordHash == passwordHash)
            {
                model = _mapper.Map<User>(user);
            }

            return new LoginResponse()
            {
                User = model,
                LoggedIn = (model != null)
            };

        }
    }
}
