using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PickEmLeagueDatabase.Entities
{
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
