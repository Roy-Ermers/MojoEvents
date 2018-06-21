using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace MojoEvents.Pages
{
    public class UsersModel : PageModel
    {
        public MySqlDataReader Users;
        public void OnGet()
        {
           Users = Sql.Query("SELECT * FROM BackUser;");
        }
    }
}