using Microsoft.Extensions.DependencyInjection;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;
using PickEmLeagueUtils;

namespace PickEmLeagueServerTests.ServiceTests
{
    public class UserServiceTests : BaseServiceTests<User, PickEmLeagueDatabase.Entities.User>
    {
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            var services = ServiceUtils.BuildTestServiceProvider();
            _userService = services.GetRequiredService<IUserService>();
        }

        public override User NewModel()
        {
            return new User()
            {
                Email = "email1@gmail.com",
                FirstName = "first",
                LastName = "last"
            };
        }

        public override User UpdateModel(User model)
        {
            model.FirstName += "more";
            return model;
        }

        protected override IBaseService<User, PickEmLeagueDatabase.Entities.User> BaseService()
        {
            return _userService;
        }
    }
}
