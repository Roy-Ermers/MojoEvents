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
        public string FestivalClickCount;
        public string FestivalNames;
        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("UserID").HasValue)
                return Redirect("./login?referer=analytics");

            var query = Sql.Query($"SELECT FestivalName, Clicks FROM Festival WHERE OwnerID = {HttpContext.Session.GetInt32("UserID").Value}");
            while(query.Read())
            {
                FestivalNames += $"\"{query.GetValue(0)}\",";
                FestivalClickCount += query.GetValue(1).ToString() + ",";
            }
            FestivalClickCount = FestivalClickCount.TrimEnd(',');
            FestivalNames = FestivalNames.TrimEnd(',');
            return Page();
        }
    }
}