using System;

namespace PickEmLeagueDatabase.Entities
{
    [Amazon.DynamoDBv2.DataModel.DynamoDBTable("Users")]
    public class User
    {
        public Guid Id { get; set; }
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
