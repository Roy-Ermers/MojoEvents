using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MojoEvents;
namespace MojoEvents.Pages
{

    public class festivalsModel : PageModel
    {
        public string Message { get; private set; }
        public List<Festival> GetFestivals()
        {
            List<Festival> result = new List<Festival>();
            var query = Sql.Query($"SELECT * FROM Festival;");
            if (query.HasRows)
            {
                while (query.Read())
                {
                    result.Add(Festival.Read(query.GetInt32(0)));
                }
                return result;
            }
            result.Add(new Festival() { EventName = "(geen Festival gevonden)" });
            return result;
        }
        public IActionResult OnGet()
        {
            if (Request.HttpContext.Session.GetInt32("UserID") == null)
                return RedirectPermanent("./login");
            return Page();
        }
        public void OnPost()
        {
            if (!ModelState.IsValid)
                return;
            if (!string.IsNullOrEmpty(Request.Form["delete"]))
            {
                Sql.Query("DELETE FROM Festival WHERE FestivalID = " + Request.Form["delete"] + ";");
                Message = "Festival verwijderd!";
            }
        }
    }
}