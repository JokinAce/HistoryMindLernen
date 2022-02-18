using HistoryMindLernen.Database;
using System.Speech.Recognition;

namespace HistoryMindLernen
{
    public partial class Form : System.Windows.Forms.Form
    {
        private List<Controller.HistoryMindResult> Begriffe { get; set; } = new();
        private Controller.HistoryMindResult Begriff { get; set; }
        private int Punkte { get; set; } = 0;
        private string? PrevText { get; set; }

        public Form()
        {
            this.InitializeComponent();
        }

        private void AddPunkt() {
            this.Punkte += 1;
            this.PunkteLabel.Text = $"Punkte: {Punkte}/6";
        }

        private void ClearPunkt() {
            this.PunkteLabel.Text = $"Punkte: 0/6";
            Punkte = 0;
        }

        private void SchiebBegriffe() {
            this.Begriff = this.Begriffe.ElementAt(Begriffe.Count - 1);
            this.Begriffe.Remove(this.Begriff);
        }

        private bool CheckBegriffe() {
            if (this.Begriffe.Count == 0) {
                this.AuflösungKnopf.Visible = false;
                this.KorregierenKnopf.Visible = false;
                this.PunkteLabel.Visible = false;

                this.ErklärungTextBox.Text = $"Note: {6 - Punkte}";
                ClearPunkt();
                this.HistoryMindGroupBox.Visible = true;

                return false;
            }
            return true;
        }

        private void LoadBegriffe() {
            Controller controller = Controller.GetInstance();
            (bool, bool, bool) HistoryMinds = (this.HistoryMindCheckBox.Checked, this.HistoryMind2CheckBox.Checked, this.HistoryMind3CheckBox.Checked);

            for (int i = 0; i != 6; i++) {
                Controller.HistoryMindResult tempResult = controller.RandomHistoryMind(HistoryMinds.Item1, HistoryMinds.Item2, HistoryMinds.Item3);

                while (this.Begriffe.Contains(tempResult)) {
                    tempResult = controller.RandomHistoryMind(HistoryMinds.Item1, HistoryMinds.Item2, HistoryMinds.Item3);
                }

                this.Begriffe.Add(tempResult);
            }
        }

        private void HistoryMindCheckBox_CheckedChanged(object sender, EventArgs e) {
            if (!this.HistoryMindCheckBox.Checked && !this.HistoryMind2CheckBox.Checked && !this.HistoryMind3CheckBox.Checked)
                this.HistoryMindCheckBox.Checked = true;
        }

        private void StartRounde() {
            if (!this.KorregierenKnopf.Visible)
                this.KorregierenKnopf.Visible = true;
            if (!this.AuflösungKnopf.Visible)
                this.AuflösungKnopf.Visible = true;
            if (!this.PunkteLabel.Visible)
                this.PunkteLabel.Visible = true;
            if (this.Begriffe.Count == 0)
                this.LoadBegriffe();
            if (this.HistoryMindGroupBox.Visible)
                this.HistoryMindGroupBox.Visible = false; 
        }

        private void NeuerBegriffKnopf_Click(object sender, EventArgs e) {
            StartRounde();

            this.ErklärungTextBox.Text = null;
            this.SchiebBegriffe();

            if (this.CheckBegriffe()) {
                this.BegriffLabel.Text = this.Begriff!.Begriff;
                this.NeuerBegriffKnopf.Text = "Skip Begriff";
            } else {
                this.BegriffLabel.Text = "Drücke \"Neuer Begriff\" um zu starten";
                this.NeuerBegriffKnopf.Text = "Neuer Begriff";
            }
        }

        private void AuflösungKnopf_Click(object sender, EventArgs e) {
            this.KorregierenKnopf.Visible = false;
            this.AuflösungKnopf.Visible = false;
            this.NeuerBegriffKnopf.Text = "Neuer Begriff";

            int confident = DamerauLevenshtein(this.Begriff!.Erklärung, this.ErklärungTextBox.Text);

            if (confident >= 50) {
                this.ErklärungTextBox.Text = @$"War nicht so korrekt, Confidence: {confident}%
{this.ErklärungTextBox.Text}

Erwartete Erklärung
{this.Begriff.Erklärung}";
            } else {
                this.ErklärungTextBox.Text = @$"Joa passt, Confidence: {confident}%
{this.ErklärungTextBox.Text}

Erwartete Erklärung
{this.Begriff!.Erklärung}";
                this.AddPunkt();
            }

            this.ErklärungTextBox.Find("Erwartete Erklärung");
            this.ErklärungTextBox.SelectionColor = Color.Yellow;

            this.CheckBegriffe();
        }

        private void KorregierenKnopf_MouseDown(object sender, MouseEventArgs e) {
            PrevText = this.ErklärungTextBox.Text;

            string differenz = Difference(this.Begriff!.Erklärung, this.ErklärungTextBox.Text)[..(this.ErklärungTextBox.Text.Length > this.Begriff.Erklärung.Length ? this.Begriff.Erklärung.Length : this.ErklärungTextBox.Text.Length)];

            this.ErklärungTextBox.Text = @$"{this.ErklärungTextBox.Text}
{differenz}";

            this.ErklärungTextBox.Find(differenz, RichTextBoxFinds.MatchCase);
            this.ErklärungTextBox.SelectionColor = Color.Yellow;
        }

        private void KorregierenKnopf_MouseUp(object sender, MouseEventArgs e) {
            this.ErklärungTextBox.Text = PrevText;
        }

        public static string Difference(string str1, string str2) {
            if (str1 == null) {
                return str2;
            }
            else if (str2 == null) {
                return str1;
            }

            List<string> set1 = str1.Split(' ').Distinct().ToList();
            List<string> set2 = str2.Split(' ').Distinct().ToList();

            var diff = set2.Count > set1.Count ? set2.Except(set1).ToList() : set1.Except(set2).ToList();

            return string.Join(" ", diff);
        }

        public static int DamerauLevenshtein(string original, string modified) {
            int len_orig = original.Length;
            int len_diff = modified.Length;

            var matrix = new int[len_orig + 1, len_diff + 1];
            for (int i = 0; i <= len_orig; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= len_diff; j++)
                matrix[0, j] = j;

            for (int i = 1; i <= len_orig; i++) {
                for (int j = 1; j <= len_diff; j++) {
                    int cost = modified[j - 1] == original[i - 1] ? 0 : 1;
                    var vals = new int[] {
                    matrix[i - 1, j] + 1,
                    matrix[i, j - 1] + 1,
                    matrix[i - 1, j - 1] + cost
                };
                    matrix[i, j] = vals.Min();
                    if (i > 1 && j > 1 && original[i - 1] == modified[j - 2] && original[i - 2] == modified[j - 1])
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                }
            }
            return matrix[len_orig, len_diff];
        }

        //public static int GetDamerauLevenshteinDistance(string s, string t) {
        //    if (string.IsNullOrEmpty(s)) {
        //        throw new ArgumentNullException(s, "String Cannot Be Null Or Empty");
        //    }

        //    if (string.IsNullOrEmpty(t)) {
        //        throw new ArgumentNullException(t, "String Cannot Be Null Or Empty");
        //    }

        //    int n = s.Length; // length of s
        //    int m = t.Length; // length of t

        //    if (n == 0) {
        //        return m;
        //    }

        //    if (m == 0) {
        //        return n;
        //    }

        //    int[] p = new int[n + 1]; //'previous' cost array, horizontally
        //    int[] d = new int[n + 1]; // cost array, horizontally

        //    // indexes into strings s and t
        //    int i; // iterates through s
        //    int j; // iterates through t

        //    for (i = 0; i <= n; i++) {
        //        p[i] = i;
        //    }

        //    for (j = 1; j <= m; j++) {
        //        char tJ = t[j - 1]; // jth character of t
        //        d[0] = j;

        //        for (i = 1; i <= n; i++) {
        //            int cost = s[i - 1] == tJ ? 0 : 1; // cost
        //                                               // minimum of cell to the left+1, to the top+1, diagonally left and up +cost                
        //            d[i] = Math.Min(Math.Min(d[i - 1] + 1, p[i] + 1), p[i - 1] + cost);
        //        }

        //        // copy current distance counts to 'previous row' distance counts
        //        int[] dPlaceholder = p; //placeholder to assist in swapping p and d
        //        p = d;
        //        d = dPlaceholder;
        //    }

        //    // our last action in the above loop was to switch d and p, so p now 
        //    // actually has the most recent cost counts
        //    return p[n];
        //}
    }
}