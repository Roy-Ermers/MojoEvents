﻿using System;
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
        public int Clicks { get; private set; }
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
            else
            {
                Sql.Query($"UPDATE Festival SET FestivalName = '{EventName ?? " "}', StartDate = '{StartDate.ToShortDateString()}',EndDate = '{EndDate.ToShortDateString()}',Location = '{Location ?? ""}',EntryPrice = {EntryPrice},YoutubeVideo = '{YoutubeVideo ?? " "}',Image = '{Image ?? " "}',Info = '{Info ?? " "}',Draft = {Draft},OwnerID = {OwnerID} WHERE FestivalID = {EventID};");

            }
        }
        public static Festival Read(int ID)
        {
            var query = Sql.Query($"SELECT * FROM Festival WHERE FestivalID = {ID};");
            query.Read();
            if (query.HasRows)
            { 
                Festival result = new Festival();
                result.EventID = query.GetInt32(0);
                result.EventName = query.GetString(1);
                result.StartDate = query.GetDateTime(2);
                result.EndDate = query.GetDateTime(3);
                result.EntryPrice = query.GetDecimal(4);
                result.YoutubeVideo = query.GetValue(5).ToString();
                result.Image = query.GetValue(6).ToString();
                result.Info = query.GetValue(7)?.ToString();
                result.Location = query.GetString(8);
                result.Clicks = query.GetInt32(9);
                result.Draft = query.GetBoolean(10);
                result.OwnerID = query.GetInt32(11);

                return result;
            }
            else throw new NullReferenceException("Could not found " + ID);
        }
    
        public static List<Festival> FromUsers(int UserID)
        {
            List<Festival> festivals = new List<Festival>();
            var query = Sql.Query($"SELECT * FROM Festival WHERE OwnerID = {UserID};");
            while(query.Read())
            {
                Festival result = new Festival
                {
                    EventID = query.GetInt32(0),
                    EventName = query.GetString(1),
                    StartDate = query.GetDateTime(2),
                    EndDate = query.GetDateTime(3),
                    EntryPrice = query.GetDecimal(4),
                    YoutubeVideo = query.GetValue(5).ToString(),
                    Image = query.GetValue(6).ToString(),
                    Info = query.GetValue(7)?.ToString(),
                    Location = query.GetString(8),
                    Clicks = query.GetInt32(9),
                    Draft = query.GetBoolean(10),
                    OwnerID = query.GetInt32(11)
                };
                festivals.Add(result);
            }
            return festivals;
        }
    }
}
