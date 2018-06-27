using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojoEvents.Models
{
    public class Scope
    {
        public static UserScopes? Read(int UserID)
        {
            var query = Sql.Query($"SELECT ScopeIndex FROM Scope WHERE UserID = {UserID} AND FestivalID IS NULL;");
            UserScopes? scopes = UserScopes.None;
            while (query.Read())
            {
                scopes |= (UserScopes)query.GetValue(0);
            }
            return query.HasRows ? scopes : null;
        }

        public static UserScopes Read(int UserID, int EventID)
        {
            var query = Sql.Query($"SELECT ScopeIndex FROM Scope WHERE UserID = {UserID} AND FestivalID = {EventID};");
            UserScopes scopes = UserScopes.None;
            while(query.Read())
            {
                scopes |= (UserScopes)(sbyte)query.GetValue(0);
            }
            return scopes;
        }

        public static void Write(UserScopes scopes, int UserID, int EventID = -1)
        {
            foreach (Enum value in Enum.GetValues(scopes.GetType()))
                if (scopes.HasFlag(value))
                    Sql.ScalarQuery($@"INSERT INTO Scope (UserID,FestivalID,ScopeIndex) VALUES ({UserID}, {EventID}, {(int)(UserScopes)value});");
        }
    }
}
