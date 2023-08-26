using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackgroundServiceSample.Model
{
    public class BG_model
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public string datee { get; set; }


    }
}
