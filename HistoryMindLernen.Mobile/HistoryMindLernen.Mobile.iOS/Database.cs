using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Xamarin.Forms;
using Dapper;
using HistoryMindLernen.Mobile.Database;
using HistoryMindLernen.Mobile.Droid;
using Foundation;

[assembly: Dependency(typeof(Database))]
namespace HistoryMindLernen.Mobile.Droid {

    public class Database : ISQLite {

        SqliteConnection ISQLite.GetConnection() {

            const string databaseName = "HistoryMind.db";
            string dbFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string dbFile = Path.Combine(dbFolder, databaseName);
            if (!File.Exists(dbFile)) {
                string existingDb = NSBundle.MainBundle.PathForResource("MyLite", "db");
                File.Copy(existingDb, dbFile);
            }

            return new SqliteConnection($@"Data Source={dbFile};Mode=ReadOnly;");
        }
    }
}