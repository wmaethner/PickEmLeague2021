using System;
namespace PickEmLeagueModels.Models
{
    public class User : DbModel
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string PasswordHash { get; set; }
    }
}
