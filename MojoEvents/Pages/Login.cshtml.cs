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
            string name         = Request.Form["UserName"];
            string Password     = Request.Form["Password"];

            var query = Sql.ScalarQuery($"SELECT UserID FROM BackUser WHERE UserName = '{name}' AND Password = PASSWORD('{Password}');");
            if(query!=null)
            {
                Request.HttpContext.Session.SetInt32("UserID",(int)query);
            }
            else
            {
                ViewData["Message"] = "Gebruikersnaam of wachtwoord klopt niet.";
            }
        }
    }
}