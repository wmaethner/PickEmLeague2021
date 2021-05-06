using System;
using System.ComponentModel.DataAnnotations.Schema;
using Amazon.DynamoDBv2.Model;

namespace PickEmLeagueDatabase.Entities
{
    [Amazon.DynamoDBv2.DataModel.DynamoDBTable("Users")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid Guid { get; set; }
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
