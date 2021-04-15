using System;
using System.Text.Json.Serialization;

namespace PickEmLeagueServer.Models
{
    public class User : DatabaseObject
    {        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }        

        public override string ToString()
        {
            return $"{FirstName}" +
                $"{LastName}" +
                $"{Email}" +
                base.ToString();
        }
    }
}
