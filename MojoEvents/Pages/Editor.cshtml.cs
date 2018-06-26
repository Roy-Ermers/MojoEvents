using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MojoEvents.Pages
{
    public class EditorModel : PageModel
    {
        public Festival Festival { get; set; }
        public string FestivalName
        {
            get
            {
                return Festival?.EventName ?? "New Festival";
            }
        }

        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("UserID").HasValue)
                return Redirect("./login?referer=analytics");
            if (!string.IsNullOrEmpty(Request.Query["ID"]))
            {
                Festival = Festival.Read(int.Parse(Request.Query["ID"]));
            }
            else Festival = new Festival();
            return Page();
        }
        public void OnPost()
        {
            if (!Request.HasFormContentType) return;
            Festival = Festival ?? new Festival();
            Festival.EventName = Request.Form["EventName"];
            Festival.StartDate = DateTime.Parse(Request.Form["StartDate"]);
            Festival.EndDate = DateTime.Parse(Request.Form["EndDate"]);
            Festival.Location = Request.Form["Location"];
            Festival.EntryPrice = decimal.Parse(Request.Form["EntryPrice"]);
            Festival.YoutubeVideo = Request.Form["YoutubeVideo"];
            Festival.Image = Request.Form["Image"];
            Festival.Draft = string.IsNullOrEmpty(Request.Form["Draft"]);
            Festival.OwnerID = int.Parse(Request.Form["OwnerID"]);
            Festival.Write();
            Response.Redirect("./festivals", true);
        }
    }
}