using System;
using Amazon.DynamoDBv2.DataModel;

namespace PickEmLeagueDatabase.Entities
{
    [DynamoDBTable("Users")]
    public class User : GuidBasedEntity
    {
        //public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }        

        public override string ToString()
        {
            return $"{FirstName}" +
                $"{LastName}" +
                $"{Email}";
        }
    }
}
