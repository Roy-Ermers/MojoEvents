using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MojoEvents.Models;
using MySql.Data.MySqlClient;
namespace MojoEvents.Pages
{
    public class UsersModel : PageModel
    {
        public string Message { get; private set; }
        public string FestivalOptions { get; set; }
        public List<User> GetUsers()
        {
            List<User> Users = new List<User>();

            if (Scope.Read(HttpContext.Session.GetInt32("UserID").Value)?.HasFlag(UserScopes.SeeAllData) ?? false)
                return Models.User.Read();
            else
                return Models.User.Read().FindAll(x => x.OwnerID == Request.HttpContext.Session.GetInt32("UserID"));
        }

        public IActionResult OnGet()
        {
            if (!HttpContext.Session.GetInt32("UserID").HasValue)
                return Redirect("./login?referer=users");

            return Page();
        }
        public void OnPostAdd()
        {
            if (Request.Form.ContainsKey("NewUser"))
            {
                new User
                {
                    UserName = Request.Form["NewUser"],
                    OwnerID = Request.HttpContext.Session.GetInt32("UserID").Value
                }.Write();
            }

        }
        public void OnPost()
        {
            if (!ModelState.IsValid) return;
            if (Request.Form.ContainsKey("UserID"))
            {
                if (Request.Form.ContainsKey("DeleteUser"))
                {
                    Sql.Query($"DELETE FROM BackUser WHERE UserID = {Request.Form["UserID"]};");
                    return;
                }
                List<int> Festivals = new List<int>();
                UserScopes Scopes = UserScopes.None;
                foreach (string query in Request.Form.Keys)
                {
                    if (query == "editFestival") Scopes |= UserScopes.EditFestival;
                    if (query == "DeleteFestival") Scopes |= UserScopes.DeleteFestival;
                    if (query == "CreateFestival") Scopes |= UserScopes.CreateFestival;
                }

                foreach (string festivalID in Request.Form["Festivals"])
                {
                    Scope.Write(Scopes, int.Parse(Request.Form["UserID"]), int.Parse(festivalID));
                }
            }
        }
    }
}