using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PickEmLeagueWebUI.Models;
using PickEmLeagueWebUI.Services;

namespace PickEmLeagueWebUI.Pages
{
    public class testModel : PageModel
    {
        public IEnumerable<User> Users { get; set; }

        public async Task OnGetAsync()
        {
            Users = await new UserService().GetUsersAsync();
        }
    }
}