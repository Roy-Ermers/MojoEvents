using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MojoEvents.Pages
{
    public class ChartpageModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (Request.HttpContext.Session.GetInt32("UserID") == null)
                return RedirectPermanent("./login");
            else return Page();
        }
    }
}