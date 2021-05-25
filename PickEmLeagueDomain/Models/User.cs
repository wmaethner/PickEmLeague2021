using System;

namespace PickEmLeagueDomain.Models
{
    public class User : GuidBasedModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User other)
        {
            return other != null &&
                   FirstName == other.FirstName &&
                   LastName == other.LastName &&
                   Email == other.Email;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, FirstName, LastName, Email);
        }
    }
}
