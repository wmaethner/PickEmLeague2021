using System;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeagueServices.DomainServices.Interfaces;
using PickEmLeagueServices.Repositories.Interfaces;
using Xunit;

namespace PickEmLeagueServiceTests
{
    public class AuthenticationServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticationService _authenticationService;

        private string ValidEmail = "valid@valid.com";
        private string InvalidEmail = "invalid@valid.com";
        private string ValidPassword = "valid";
        private string InvalidPassword = "invalid";

        public AuthenticationServiceTests()
        {
            var serviceProvider = ServiceHelper.BuildUnitTestServices("authTests");

            _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
            _authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
            
            _userRepository.CreateAsync(new PickEmLeagueDatabase.Entities.User()
                { Email = ValidEmail, PasswordHash = ValidPassword });
        }

        [Fact]
        public void AttemptLogin_ValidCredentials_ReturnsSuccess()
        {
            var response = _authenticationService.AttempLogin(ValidEmail, ValidPassword);

            Assert.True(response.LoggedIn);
            Assert.NotNull(response.User);
        }

        [Fact]
        public void AttemptLogin_InvalidEmail_Fails()
        {
            var response = _authenticationService.AttempLogin(InvalidEmail, ValidPassword);

            Assert.False(response.LoggedIn);
            Assert.Null(response.User);
        }

        [Fact]
        public void AttemptLogin_InvalidPassword_Fails()
        {
            var response = _authenticationService.AttempLogin(ValidEmail, InvalidPassword);

            Assert.False(response.LoggedIn);
            Assert.Null(response.User);
        }

        [Fact]
        public void AttemptLogin_InvalidBoth_Fails()
        {
            var response = _authenticationService.AttempLogin(InvalidEmail, InvalidPassword);

            Assert.False(response.LoggedIn);
            Assert.Null(response.User);
        }
    }
}
