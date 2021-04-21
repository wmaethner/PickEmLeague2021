using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeagueDatabase;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;
using PickEmLeagueUtils;
using Xunit;

namespace PickEmLeagueServerTests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly DatabaseContext _db;

        public UserServiceTests()
        {
            var services = ServiceUtils.BuildTestServiceProvider();
            _userService = services.GetRequiredService<IUserService>();
            _db = services.GetService<DatabaseContext>()!;
        }

        [Fact]
        public async Task GetUserWithGuidSucceeds()
        {
            User user = await _userService.AddUser("email1@gmail.com", "first1", "last1");

            User retrievedUser = await _userService.GetUser(user.Guid);
            Assert.Equal(user.Guid, retrievedUser.Guid);
            Assert.Equal(user.FirstName, retrievedUser.FirstName);
        }

        [Fact]
        public async Task GetUsersSucceeds()
        {
            Assert.Empty(await _userService.GetUsers());

            await _userService.AddUser("email1@gmail.com", "first1", "last1");
            Assert.Single(await _userService.GetUsers());

            await _userService.AddUser("email1@gmail.com", "first1", "last1");
            Assert.Equal(2, (await _userService.GetUsers()).Count);
        }
    }
}
