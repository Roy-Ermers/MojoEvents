using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace MojoEvents.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            Sql.Query("INSERT INTO BackUser (UserName, PasswordHash, OwnerID) VALUES ('TestUser', PASSWORD('Wachtwoord'),1)");
        }
    }
}
