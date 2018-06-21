using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojoEvents.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public int OwnerID { get; set; }
        public static User Read(int ID)
        {
            var query = Sql.Query($"SELECT * FROM BacKUser WHERE UserID = {ID};");
            query.Read();
            if (query.HasRows)
            {
                User user = new User();
                user.UserID = query.GetInt32(0);
                user.UserName = query.GetString(1);
                user.Password = (byte[])query.GetValue(2);
                user.OwnerID = query.GetInt32(4);
                return user;
            }
            else
                throw new Exception("Could not find user " + ID);
        }
        static List<User> Cache;
        static DateTime LastUpdated = DateTime.MinValue;
        public static List<User> Read()
        {
            if (Cache != null && Cache.Count > 0 && LastUpdated > DateTime.Now.AddMinutes(5)) 
                return Cache;
            var query = Sql.Query($"SELECT * FROM BacKUser;");
            query.Read();
            if (query.HasRows)
            {
                List<User> users = new List<User>();
                while (query.Read())
                {
                    User user = new User
                    {
                        UserID = query.GetInt32(0),
                        UserName = query.GetString(1),
                        Password = (byte[])query.GetValue(2),
                        OwnerID = query.GetInt32(4)
                    };
                    users.Add(user);
                }
                LastUpdated = DateTime.Now;
                Cache = users;
                return users;
            }
            else
                throw new Exception("User Table is empty.");

        }
    }
}
