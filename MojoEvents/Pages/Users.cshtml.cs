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
        public DataTableReader Users;
        public void OnGet()
        {
            var query = Sql.Query("SELECT * FROM BackUser;");
            if (query != null && query.FieldCount > 0)
            {
                Users = query;
            }
        }
    }
}