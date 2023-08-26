using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BackgroundServiceSample.Droid;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Droid))]
namespace BackgroundServiceSample.Droid
{
    
    public class SQLite_Droid : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "fakestoredb.sqlite";
            var dbPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(dbPath, dbName);
            var connection = new SQLiteConnection(path);
            return connection;
        }
    }
}