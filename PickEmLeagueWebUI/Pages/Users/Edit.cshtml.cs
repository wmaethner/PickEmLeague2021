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
    public class EditModel : PageModel
    {
        [BindProperty]
        public User UserModel { get; set; }

        private UserDatabase _database;

        public EditModel(UserDatabase userDatabase)
        {
            _database = userDatabase;
        }

        public void OnGet(string id)
        {
            UserModel = _database.Get(new Guid(id));
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _database.Edit(UserModel);

            return RedirectToPage("./Index");
        }
    }
}