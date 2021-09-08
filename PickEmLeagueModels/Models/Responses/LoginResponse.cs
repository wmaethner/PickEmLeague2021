using System;
namespace PickEmLeagueModels.Models.Responses
{
    public class LoginResponse
    {
        public bool LoggedIn { get; set; }
        public User User { get; set; }
    }
}
