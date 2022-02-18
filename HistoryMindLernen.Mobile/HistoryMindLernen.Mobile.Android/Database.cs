using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Data.Sqlite;
using Xamarin.Forms;
using Dapper;
using HistoryMindLernen.Mobile.Database;
using HistoryMindLernen.Mobile.Droid;

[assembly: Dependency(typeof(Database))]
namespace HistoryMindLernen.Mobile.Droid {

    public class Database : ISQLite {

        SqliteConnection ISQLite.GetConnection() {

            const string databaseName = "HistoryMind.db";
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string dbFile = Path.Combine(docFolder, databaseName); // FILE NAME TO USE WHEN COPIED

            if (!File.Exists(dbFile)) {
                FileStream writeStream = new FileStream(dbFile, FileMode.OpenOrCreate, FileAccess.Write);
                Android.App.Application.Context.Assets.Open(databaseName).CopyTo(writeStream);
            }

            return new SqliteConnection($@"Data Source={dbFile};Mode=ReadOnly;");
        }
    }
}