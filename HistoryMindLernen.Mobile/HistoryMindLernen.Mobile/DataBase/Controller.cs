using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using Dapper;
using System.Linq;

namespace HistoryMindLernen.Mobile.Database {
    public interface ISQLite {
        SqliteConnection GetConnection();
    }

    public class Controller {
        public static Controller Instance { get; set; }
        private SqliteConnection SQLiteConnection { get; set; }

        public struct HistoryMindResult {
            public string Begriff { get; set; }
            public string Erklärung { get; set; }
            public long Confidence { get; set; }
        }

        private Controller() {
            this.SQLiteConnection = DependencyService.Get<ISQLite>().GetConnection();
        }

        public static Controller GetInstance() {
            if (Instance == null)
                Instance = new Controller();

            return Instance;
        }

        public HistoryMindResult RandomHistoryMind(bool historyMind1 = true, bool historyMind2 = true, bool historyMind3 = true) {
            StringBuilder stringBuilder = new StringBuilder();

            if (historyMind1) {
                stringBuilder.AppendLine("SELECT * FROM HistoryMind");

                if (historyMind2 || historyMind3)
                    stringBuilder.AppendLine("UNION");
            }
            if (historyMind2) {
                stringBuilder.AppendLine("SELECT * FROM HistoryMind2");

                if (historyMind3)
                    stringBuilder.AppendLine("UNION");
            }
            if (historyMind3) {
                stringBuilder.AppendLine("SELECT * FROM HistoryMind3");
            }

            IEnumerable<HistoryMindResult> query = this.SQLiteConnection.Query<HistoryMindResult>(stringBuilder.ToString());

            return query.ElementAt(new Random().Next(query.Count()));
        }
    }
}
