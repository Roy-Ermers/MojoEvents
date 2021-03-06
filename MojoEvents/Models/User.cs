﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojoEvents.Models
{
    public class User
    {
        public int UserID { get; private set; } = -1;
        public string UserName { get; set; }
        public string Password { get; set; }
        public int OwnerID { get; set; }
        
        public UserScopes? Scopes
        {
            get
            {
                if (scopes == UserScopes.None) 
                    scopes = Scope.Read(UserID);
                return scopes;
            }
        }

        private UserScopes? scopes = UserScopes.None;
        public static User Read(int ID)
        {
            var query = Sql.Query($"SELECT * FROM BackUser WHERE UserID = {ID};");
            query.Read();
            if (query.HasRows)
            {
                User user = new User();
                user.UserID = query.GetInt32(0);
                user.UserName = query.GetString(1);
                user.Password = query.GetString(2);
                user.OwnerID = query.GetInt32(3);
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
            var query = Sql.Query($"SELECT * FROM BackUser;");
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
                        Password = query.GetString(2),
                        OwnerID = query.GetInt32(3)
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

        public void Write()
        {
            if (UserID == -1)
            {
                Sql.ScalarQuery($"INSERT INTO BackUser (UserName, PasswordHash, OwnerID) VALUES ('{UserName}','{Password}', {OwnerID});");
            }
            else
            {
                Sql.ScalarQuery($"UPDATE BackUser SET UserName = '{UserName}', PasswordHash = '{Password}', OwnerID = {OwnerID} WHERE UserID = {UserID};");
            }
        }
        public void ChangePassword(string newPassword)
        {
            Password = Sql.ScalarQuery($"SELECT PASSWORD({newPassword});").ToString();
        }
        public UserScopes GetScopes(int EventID)
        {
            return Scope.Read(UserID, EventID);
        }
    }
}
