using System;
using System.Text.Json.Serialization;

namespace PickEmLeagueWebUI.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }

        public User()
        {
        }
    }
}
