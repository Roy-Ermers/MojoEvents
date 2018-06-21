using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MojoEvents
{
    public class Festival
    {
        public int EventID { get; set; } = -1;
        public string EventName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public decimal EntryPrice { get; set; }
        public string YoutubeVideo { get; set; }
        public string Image { get; set; }
        public string Info { get; set; }
        public bool Draft { get; set; }
        public int OwnerID { get; set; }

        public void Write()
        {
            if(EventID==-1)
            {
                Sql.Query($"INSERT INTO Festival (FestivalName, StartDate, EndDate, Location, EntryPrice, YoutubeVideo, Image, Info, Draft, OwnerID) " +
                    $"VALUES ('{EventName ?? " "}','{StartDate.ToShortDateString()}','{EndDate.ToShortDateString()}','{Location ?? ""}',{EntryPrice},'{YoutubeVideo ?? " "}','{Image ?? " "}','{Info ?? " "}',{Draft},{OwnerID});");
            }
        }
        public static Festival Read(int ID)
        {
            var query = Sql.Query($"SELECT * FROM Festival WHERE FestivalID = {ID};");
            query.Read();
            if (query.HasRows)
            { 
                Festival result = new Festival();
                result.EventID = ID;
                result.EventName = query.GetString(1);
                result.StartDate = query.GetDateTime(2);
                result.EndDate = query.GetDateTime(3);
                result.EntryPrice = query.GetDecimal(4);
                result.YoutubeVideo = query.GetValue(5).ToString();
                result.Image = query.GetValue(6).ToString();
                result.Info = query.GetString(7);
                result.Location = query.GetString(8);
                result.Draft = query.GetBoolean(9);
                result.OwnerID = query.GetInt32(10);

                return result;
            }
            else throw new NullReferenceException("Could not found " + ID);
        }
    }
}
