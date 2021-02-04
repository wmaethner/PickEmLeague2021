using System;
using System.Text.Json.Serialization;

namespace PickEmLeagueServer.Models
{
    public class User : DatabaseObject
    {                
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public User()
        {
        }

        public override object Clone()
        {
            return new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email
            };
        }
    }
}
