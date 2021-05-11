using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PickEmLeagueDatabase;
using PickEmLeagueDatabase.Interfaces;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;
using PickEmLeagueUtils;
using Xunit;

namespace PickEmLeagueServerTests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly IDatabaseContext _db;

        public UserServiceTests()
        {
            var services = ServiceUtils.BuildTestServiceProvider();
            _userService = services.GetRequiredService<IUserService>();
            _db = services.GetService<IDatabaseContext>()!;
        }

        [Fact]
        public async Task GetUserWithGuidSucceeds()
        {
            User user = await _userService.Add(new User()
            {
                Email = "email1@gmail.com",
                FirstName = "first1",
                LastName = "last1"
            });

            User retrievedUser = await _userService.Get(user.Id);
            Assert.Equal(user.Id, retrievedUser.Id);
            Assert.Equal(user.FirstName, retrievedUser.FirstName);
        }

        [Fact]
        public async Task GetUsersSucceeds()
        {
            Assert.Empty(await _userService.GetAll());

            await _userService.Add(new User()
            {
                Email = "email1@gmail.com",
                FirstName = "first1",
                LastName = "last1"
            });
            Assert.Single(await _userService.GetAll());

            await _userService.Add(new User()
            {
                Email = "email1@gmail.com",
                FirstName = "first1",
                LastName = "last1"
            });
            Assert.Equal(2, (await _userService.GetAll()).Count);
        }

        [Fact]
        public async Task UpdateUserSucceeds()
        {
            User user = await _userService.Add(new User()
            {
                Email = "email1@gmail.com",
                FirstName = "first1",
                LastName = "last1"
            });
            Assert.Equal(user.FirstName, (await _userService.Get(user.Id)).FirstName);
            user.FirstName = "newfirst";
            await _userService.Update(user);

            Assert.Single(await _userService.GetAll());
            Assert.Equal(user.FirstName, (await _userService.Get(user.Id)).FirstName);
        }
    }
}
