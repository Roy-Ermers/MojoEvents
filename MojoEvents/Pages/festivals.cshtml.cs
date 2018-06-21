using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MojoEvents;
namespace MojoEvents.Pages
{

    public class festivalsModel : PageModel
    {
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
        public void OnGet()
        {
        }
    }
}