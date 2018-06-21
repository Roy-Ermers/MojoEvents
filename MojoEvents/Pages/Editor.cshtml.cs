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
        public void OnGet()
        {

        }
    }
}