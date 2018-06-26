using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MojoEvents.Pages
{
    public class InlogUsersModel : PageModel
    {
        public void OnPost()
        {
            string name = Request.Form["UserName"];
            string Password = Request.Form["Password"];

            var query = Sql.Query($"SELECT UserID FROM BackUser WHERE UserName = '{name}' AND PasswordHash = PASSWORD('{Password}');");

            if (query.HasRows)
            {
                query.Read();
                var userID = query.GetInt32(0);
                HttpContext.Session.SetInt32("UserID", userID);
                Response.Redirect("./editor",true);
            }
            else
            {
                ViewData["Message"] = "Gebruikersnaam of wachtwoord klopt niet.";
            }
        }
    }
}