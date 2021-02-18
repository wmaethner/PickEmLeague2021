using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PickEmLeagueWebUI.Data;
using PickEmLeagueWebUI.Models;

namespace PickEmLeagueWebUI.Pages.Users
{
    public class IndexModel : PageModel
    {
        private UserDatabase _database;

        public List<User> Users { get; set; }

        public IndexModel(UserDatabase userDatabase)
        {
            _database = userDatabase;
        }

        public async Task OnGetAsync()
        {
            Users = (await _database.GetAsync()).ToList();
        }
    }
}