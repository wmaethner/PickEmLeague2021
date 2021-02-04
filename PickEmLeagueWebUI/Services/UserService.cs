using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PickEmLeagueWebUI.Models;

namespace PickEmLeagueWebUI.Services
{
    public class UserService
    {
        public UserService()
        {
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            User[] users = null;
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://localhost:5001/user");
                if (response.IsSuccessStatusCode)
                {
                    users = JsonConvert.DeserializeObject<User[]>(await response.Content.ReadAsStringAsync());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return users;
        }
    }
}
