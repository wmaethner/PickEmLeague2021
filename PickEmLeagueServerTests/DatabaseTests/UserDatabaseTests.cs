//using System;
//using System.Collections.Generic;
//using PickEmLeagueServer.Database;
//using PickEmLeagueServer.Models;
//using Xunit;

//namespace PickEmLeagueServerTests
//{
//    public class UserDatabastTests
//    {
//        #region Helper Functions
//        private static readonly string[] FirstNames = new[]
//            {
//            "Alice", "Bob", "Charlie", "Danielle", "Ethan", "Fred", "Ginny", "Heather", "Isaac", "John"
//        };
//        private static readonly string[] LastNames = new[]
//        {
//            "Anderson", "Baker", "Chan", "Davidson", "Ebert", "Franklin", "Grigson", "Hunkler", "Immelman", "Johnson"
//        };
//        private static readonly string[] Emails = new[]
//        {
//            "AEmail", "BEmail", "CEmail", "DEmail", "EEmail", "FEmail", "GEmail", "HEmail", "IEmail", "JEmail"
//        };

//        private User CreateUser()
//        {
//            var rng = new Random();
//            return new User()
//            {
//                FirstName = FirstNames[rng.Next(FirstNames.Length)],
//                LastName = LastNames[rng.Next(LastNames.Length)],
//                Email = Emails[rng.Next(Emails.Length)]
//            };
//        }
//        #endregion

//        [Fact]
//        public void Create_ValidUser_ReturnsUser()
//        {
//            UserDatabase database = new UserDatabase();
//            User user = CreateUser();

//            Assert.True(database.Create(user));
//        }

//        [Fact]
//        public void Read_NoId_ReturnsListOfUsers()
//        {
//            UserDatabase database = new UserDatabase();
//            User user1 = CreateUser();
//            User user2 = CreateUser();
//            List<User> users;

//            database.Create(user1);
//            users = new List<User>(database.Read());
//            Assert.Equal(users[0], user1);

//            database.Create(user2);
//            users = new List<User>(database.Read());
//            Assert.Equal(2, users.Count);
//            Assert.Contains(user1, users);
//            Assert.Contains(user2, users);        
//        }

//        [Fact]
//        public void Read_ValidId_ReturnsUser()
//        {
//            UserDatabase database = new UserDatabase();
//            User user1 = CreateUser();
//            User user2 = CreateUser();            

//            database.Create(user1);
//            Assert.Equal(user1, database.Read(user1.Id));
            
//            database.Create(user2);
//            Assert.Equal(user1, database.Read(user1.Id));
//        }

//        [Fact]
//        public void Read_InvalidId_ThrowsException()
//        {
//            UserDatabase database = new UserDatabase();
//            User user1 = CreateUser();
//            User user2 = CreateUser();

//            database.Create(user1);
//            Assert.Throws<Exception>(() => { database.Read(Guid.NewGuid().ToString()); });
//        }

//        [Fact]
//        public void Update_ValidUser_ReturnsUser()
//        {

//        }

//        [Fact]
//        public void Delete_ValidId_ReturnsTrue()
//        {

//        }

//        [Fact]
//        public void Delete_InvalidId_ThrowsException()
//        {

//        }
//    }
//}
