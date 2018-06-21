using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MojoEvents.Pages
{
    public class EditorModel : PageModel
    {
        public string FestivalName { get; } = "New Festival";
        public void OnPost()
        {
            if (!Request.HasFormContentType) return;
            Festival result = new Festival
            {
                EventName = Request.Form["EventName"],
                StartDate = DateTime.Parse(Request.Form["StartDate"]),
                EndDate = DateTime.Parse(Request.Form["EndDate"]),
                Location = Request.Form["Location"],
                EntryPrice = decimal.Parse(Request.Form["EntryPrice"]),
                YoutubeVideo = Request.Form["YoutubeVideo"],
                Image = Request.Form["Image"],
                Draft = string.IsNullOrEmpty(Request.Form["Draft"]),
                OwnerID = int.Parse(Request.Form["OwnerID"])
            };
            result.Write();
        }
    }
}