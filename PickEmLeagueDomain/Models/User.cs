namespace PickEmLeagueDomain.Models
{
    public class User : GuidBasedModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
    }
}
