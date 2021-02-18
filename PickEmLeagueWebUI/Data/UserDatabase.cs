using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PickEmLeagueWebUI.Models;

namespace PickEmLeagueWebUI.Data
{
    public class UserDatabase
    {
        private List<User> _users;
        private IConfiguration Configuration;
        private HttpClient httpClient;

        public UserDatabase(IConfiguration configuration)
        {
            Configuration = configuration;
            httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(Configuration["ServerEndpoint"]);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> AddAsync(User user)
        {
            HttpResponseMessage response = await httpClient.PostAsync("user/",
                new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
            //return JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync());
        }

        public async Task<IEnumerable<User>> GetAsync()
        {

            Task<Stream> streamTask = httpClient.GetStreamAsync("/user");
            List<User> users = await JsonSerializer.DeserializeAsync<List<User>>(await streamTask);
            return users;

            //IEnumerable<User> users = null;
            //HttpResponseMessage response = await httpClient.GetAsync("/user");
            //if (response.IsSuccessStatusCode)
            //{
            //    users = await response.Content.
            //}

            //return _users.ToArray();
        }

        public User Get(Guid id)
        {
            return _users.Select(x => x).Where(x => x.Id == id).First();
        }

        public void Edit(User user)
        {
            int index = _users.FindIndex(x => x.Id == user.Id);
            _users[index] = user;
        }
    }
}
