using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojoEvents
{
    public class Festival
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public float EntryPrice { get; set; }
        public string YoutubeVideo { get; set; }
        public string Image { get; set; }
        public string Info { get; set; }
        public bool Draft { get; set; }
        public int OwnerID { get; set; }

        public static Festival Read(int ID)
        {
            var query = Sql.Query($"SELECT * FROM Festival WHERE FestivalID = {ID};");
            Festival result = new Festival
            {
                EventID = ID,
                EventName = query.GetString(1),
                StartDate = query.GetDateTime(2),
                EndDate = query.GetDateTime(3),
                Location = query.GetString(4),
                EntryPrice = query.GetFloat(5),
                YoutubeVideo = query.GetString(6),
                Image = query.GetString(7),
                Info = query.GetString(8),
                Draft = query.GetBoolean(9),
                OwnerID = query.GetInt32(10)
            };
            return result;
        }
    }
}
