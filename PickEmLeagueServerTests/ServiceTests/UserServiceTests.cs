using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PickEmLeagueDatabase;
using PickEmLeagueDatabase.Models;
using PickEmLeagueServices.Services;
using Xunit;

namespace PickEmLeagueServerTests.ServiceTests
{
    public class UserServiceTests
    {
        public UserServiceTests()
        {
            ContextOptions = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "user service db")
                .Options;

            Seed();
        }

        protected DbContextOptions<DatabaseContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new DatabaseContext(ContextOptions))
            { 
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                User user = new User()
                {
                    Email = "user1@email.com",
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Id = 1
                };

                context.Add(CreateUser("email1@gmail.com", "user1", "last1", 1));
                context.Add(CreateUser("email2@gmail.com", "user2", "last2", 2));
                context.Add(CreateUser("email3@gmail.com", "user3", "last3", 3));

                context.SaveChanges();
            }
        }

        private User CreateUser(string email, string first, string last, int id)
        {
            return new User()
            {
                Email = email,
                FirstName = first,
                LastName = last,
                Id = id
            };
        }

        [Fact]
        public void Can_get_items()
        {
            using (var context = new DatabaseContext(ContextOptions))
            {
                UserService userService = new UserService(context);

                var users = (List<User>) userService.GetUsers();

                Assert.Equal(3, users.Count);
                Assert.Equal("email1@gmail.com", users[0].Email);
                Assert.Equal("email2@gmail.com", users[1].Email);
                Assert.Equal("email3@gmail.com", users[2].Email);
            }
        }
    }
}
