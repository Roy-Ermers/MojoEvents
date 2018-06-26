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
        public void OnGet()
        {
            if (Request.HttpContext.Session.GetInt32("UserID") == null) 
                Response.Redirect("./login");
            else if (HttpContext.Session.GetInt32("UserID") == 0)
                Users = Models.User.Read();
            else
                Users = Models.User.Read().FindAll(x => x.OwnerID == HttpContext.Session.GetInt32("UserID"));
        }
    }
}