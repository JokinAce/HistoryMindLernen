using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using HistoryMindLernen.Mobile.Database;

namespace HistoryMindLernen.Mobile
{
    public partial class MainPage : ContentPage
    {
        private List<Controller.HistoryMindResult> Begriffe { get; set; } = new List<Controller.HistoryMindResult>();
        private Controller.HistoryMindResult Begriff { get; set; }
        private int Punkte { get; set; } = 0;
        private string PrevText { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private void NeuerBegriff_Clicked(object sender, EventArgs e) {
            StartRounde();

            this.ErklärungTextBox.Text = null;
            this.SchiebBegriffe();

            if (this.CheckBegriffe()) {
                this.BegriffLabel.Text = this.Begriff.Begriff;
                this.NeuerBegriffKnopf.Text = "Skip Begriff";
            } else {
                this.BegriffLabel.Text = "Drücke \"Neuer Begriff\" um zu starten";
                this.NeuerBegriffKnopf.Text = "Neuer Begriff";
            }
        }

        private void Korrektur_Pressed(object sender, EventArgs e) {
            PrevText = this.ErklärungTextBox.Text;

            if (this.ErklärungTextBox.Text.Length > this.Begriff.Erklärung.Length) {

            }

            string differenz = Difference(this.Begriff.Erklärung, this.ErklärungTextBox.Text).Substring(0,
                (this.ErklärungTextBox.Text?.Length ?? 0) > this.Begriff.Erklärung.Length ? this.Begriff.Erklärung.Length : this.ErklärungTextBox.Text.Length);
            // What the fuck is this code? (If user types longer string then the original, substring fails fix)

            this.ErklärungTextBox.Text = $@"{this.ErklärungTextBox.Text}
{differenz}";
        }

        private void Korrektur_Released(object sender, EventArgs e) {
            this.ErklärungTextBox.Text = PrevText;
        }

        private void Auflösung_Clicked(object sender, EventArgs e) {
            this.KorregierenKnopf.IsVisible = false;
            this.AuflösungKnopf.IsVisible = false;
            this.NeuerBegriffKnopf.Text = "Neuer Begriff";

            int confident = DamerauLevenshtein(this.Begriff.Erklärung, this.ErklärungTextBox.Text ?? string.Empty);

            if (confident >= 50) {
                this.ErklärungTextBox.Text = $@"War nicht so korrekt, Confidence: {confident}%
{this.ErklärungTextBox.Text}

Erwartete Erklärung
{this.Begriff.Erklärung}";
            } else {
                this.ErklärungTextBox.Text = $@"Joa passt, Confidence: {confident}%
{this.ErklärungTextBox.Text}

Erwartete Erklärung
{this.Begriff.Erklärung}";
                this.AddPunkt();
            }

            //this.ErklärungTextBox.("Erwartete Erklärung");
            //this.ErklärungTextBox.SelectionColor = Color.Yellow;

            this.CheckBegriffe();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e) {
            if (!this.HistoryMindCheckBox.IsChecked && !this.HistoryMind2CheckBox.IsChecked && !this.HistoryMind3CheckBox.IsChecked) {
                this.HistoryMindCheckBox.IsChecked = true;
            }
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
                this.AuflösungKnopf.IsVisible = false;
                this.KorregierenKnopf.IsVisible = false;
                this.PunkteLabel.IsVisible = false;

                this.ErklärungTextBox.Text = $"Note: {6 - Punkte}";
                ClearPunkt();
                this.HistoryMindGroupBox.IsVisible = true;

                return false;
            }
            return true;
        }

        private void LoadBegriffe() {
            Controller controller = Controller.GetInstance();
            (bool, bool, bool) HistoryMinds = (this.HistoryMindCheckBox.IsChecked, this.HistoryMind2CheckBox.IsChecked, this.HistoryMind3CheckBox.IsChecked);

            for (int i = 0; i != 6; i++) {
                Controller.HistoryMindResult tempResult = controller.RandomHistoryMind(HistoryMinds.Item1, HistoryMinds.Item2, HistoryMinds.Item3);

                while (this.Begriffe.Contains(tempResult)) {
                    tempResult = controller.RandomHistoryMind(HistoryMinds.Item1, HistoryMinds.Item2, HistoryMinds.Item3);
                }

                this.Begriffe.Add(tempResult);
            }
        }

        private void StartRounde() {
            if (!this.KorregierenKnopf.IsVisible)
                this.KorregierenKnopf.IsVisible = true;
            if (!this.AuflösungKnopf.IsVisible)
                this.AuflösungKnopf.IsVisible = true;
            if (!this.PunkteLabel.IsVisible)
                this.PunkteLabel.IsVisible = true;
            if (this.Begriffe.Count == 0)
                this.LoadBegriffe();
            if (this.HistoryMindGroupBox.IsVisible)
                this.HistoryMindGroupBox.IsVisible = false;
        }

        public static string Difference(string str1, string str2) {
            if (str1 == null) {
                return str2;
            } else if (str2 == null) {
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
    }
}
