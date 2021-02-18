using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using PickEmLeagueWebUI.Data;
using PickEmLeagueWebUI.Models;

namespace PickEmLeagueWebUI.Pages.Users
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public User UserModel { get; set; }

        private UserDatabase _database;

        public CreateModel(IConfiguration configuration, UserDatabase userDatabase)
        {
            _database = userDatabase;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _database.AddAsync(UserModel);      

            return RedirectToPage("./Index");
        }
    }
}