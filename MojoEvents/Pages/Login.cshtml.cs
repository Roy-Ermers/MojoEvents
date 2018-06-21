using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MojoEvents.Pages
{
    public class InlogUsersModel : PageModel
    {
        public void OnPost()
        {
            string name         = Request.Form["TXTnaam"];
            string Password     = Request.Form["TXTww"];

           
        }
    }
}