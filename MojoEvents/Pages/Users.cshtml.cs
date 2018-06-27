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
        public string Message { get; private set; }
        public List<User> GetUsers()
        {
            List<User> Users = new List<User>();
            
            if (Request.HttpContext.Session.GetInt32("UserID") == 0)
                Users = Models.User.Read();
            else
                Users = Models.User.Read().FindAll(x => x.OwnerID == Request.HttpContext.Session.GetInt32("UserID"));
            
            return Users;
        }

        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("UserID").HasValue)
                return Redirect("./login?referer=users");
            return Page();
        }
    }
}