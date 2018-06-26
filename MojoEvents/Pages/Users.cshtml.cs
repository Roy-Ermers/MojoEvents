using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MojoEvents.Models;
using MySql.Data.MySqlClient;
namespace MojoEvents.Pages
{
    public class UsersModel : PageModel
    {
        public List<User> Users { get; private set; }
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("UserID").HasValue)
                return Redirect("./login?referer=analytics");
            else
            {
                if (Request.HttpContext.Session.GetInt32("UserID") == 0)
                    Users = Models.User.Read();
                else
                    Users = Models.User.Read().FindAll(x => x.OwnerID == Request.HttpContext.Session.GetInt32("UserID"));
            }
            return Page();
        }
    }
}