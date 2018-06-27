using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MojoEvents;
using MojoEvents.Models;
namespace MojoEvents.Pages
{

    public class festivalsModel : PageModel
    {
        public string Message { get; private set; }
        public List<Festival> GetFestivals()
        {
            List<Festival> result = new List<Festival>();
            System.Data.DataTableReader query;
            if (Scope.Read(HttpContext.Session.GetInt32("UserID").Value)?.HasFlag(UserScopes.SeeAllData) ?? false)
                query = Sql.Query($"SELECT * FROM Festival;");
            else query = Sql.Query($"SELECT * FROM Festival WHERE OwnerID = {HttpContext.Session.GetInt32("UserID").Value};");
            if (query.HasRows)
            {
                while (query.Read())
                {
                    result.Add(Festival.Read(query.GetInt32(0)));
                }
                return result;
            }
            Message = "Geen Festivals gevonden.";
            return result;
        }

        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("UserID").HasValue)
                return Redirect("./login?referer=festivals");

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