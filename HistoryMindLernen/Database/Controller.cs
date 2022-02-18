using System.Text;
using System.Data.SQLite;
using Dapper;

namespace HistoryMindLernen.Database {
    public class Controller {
        private static Controller? Instance { get; set; }
        private SQLiteConnection Connection { get; set; }

        private Controller() {
            this.Connection = new SQLiteConnection(@"Data Source=./Database/HistoryMind.db;Version=3;Read Only=True;");
        }

        public struct HistoryMindResult {
            public string Begriff { get; set; }
            public string Erklärung { get; set; }
            public long Confidence { get; set; }
        }

        public HistoryMindResult RandomHistoryMind(bool historyMind1 = true, bool historyMind2 = true, bool historyMind3 = true) {
            StringBuilder stringBuilder = new();

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

            IEnumerable<HistoryMindResult> query = this.Connection.Query<HistoryMindResult>(stringBuilder.ToString());

            return query.ElementAt(new Random().Next(query.Count()));
        }

        public static Controller GetInstance() {
            if (Instance == null)
                Instance = new Controller();

            return Instance;
        }
    }
}
