// This file is part of the HistoryMindLernen Project
//
// Copyright (C) 2022
//
// “Commons Clause” License Condition v1.0
// The Software is provided to you by the Licensor under the License, as defined below, subject to the following condition.
//
// Without limiting other conditions in the License, the grant of rights under the License will not include,
// and the License does not grant to you,the right to Sell the Software.
// For purposes of the foregoing, “Sell” means practicing any or all of the rights granted to you under the License to provide to third parties,
// for a fee or other consideration (including without limitation fees for hosting or consulting/ support services related to the Software),
// a product or service whose value derives, entirely or substantially, from the functionality of the Software.
//
// Any license notice or attribution required by the License must also include this Commons Clause License Condition notice.
//
// Software: HistoryMindLernen
// License: AGPL v3.0
// Licensor: Frantisek Pis

using HistoryMindLernen.Database;

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
            InitializeComponent();
        }

        private void AddPunkt()
        {
            Punkte += 1;
            PunkteLabel.Text = $"Punkte: {Punkte}/6";
        }

        private void ClearPunkt()
        {
            PunkteLabel.Text = $"Punkte: 0/6";
            Punkte = 0;
        }

        private void SchiebBegriffe()
        {
            Begriff = Begriffe.ElementAt(Begriffe.Count - 1);
            Begriffe.Remove(Begriff);
        }

        private bool CheckBegriffe()
        {
            if (Begriffe.Count == 0)
            {
                AuflösungKnopf.Visible = false;
                KorregierenKnopf.Visible = false;
                PunkteLabel.Visible = false;

                ErklärungTextBox.Text = $"Note: {6 - Punkte}";
                ClearPunkt();
                HistoryMindGroupBox.Visible = true;

                return false;
            }
            return true;
        }

        private void LoadBegriffe()
        {
            Controller controller = Controller.GetInstance();
            (bool, bool, bool) HistoryMinds = (HistoryMindCheckBox.Checked, HistoryMind2CheckBox.Checked, HistoryMind3CheckBox.Checked);

            for (int i = 0; i != 6; i++)
            {
                Controller.HistoryMindResult tempResult = controller.RandomHistoryMind(HistoryMinds.Item1, HistoryMinds.Item2, HistoryMinds.Item3);

                while (Begriffe.Contains(tempResult))
                {
                    tempResult = controller.RandomHistoryMind(HistoryMinds.Item1, HistoryMinds.Item2, HistoryMinds.Item3);
                }

                Begriffe.Add(tempResult);
            }
        }

        private void HistoryMindCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!HistoryMindCheckBox.Checked && !HistoryMind2CheckBox.Checked && !HistoryMind3CheckBox.Checked)
            {
                HistoryMindCheckBox.Checked = true;
            }
        }

        private void StartRounde()
        {
            if (!KorregierenKnopf.Visible)
            {
                KorregierenKnopf.Visible = true;
            }

            if (!AuflösungKnopf.Visible)
            {
                AuflösungKnopf.Visible = true;
            }

            if (!PunkteLabel.Visible)
            {
                PunkteLabel.Visible = true;
            }

            if (Begriffe.Count == 0)
            {
                LoadBegriffe();
            }

            if (HistoryMindGroupBox.Visible)
            {
                HistoryMindGroupBox.Visible = false;
            }
        }

        private void NeuerBegriffKnopf_Click(object sender, EventArgs e)
        {
            StartRounde();

            ErklärungTextBox.Text = null;
            SchiebBegriffe();

            if (CheckBegriffe())
            {
                BegriffLabel.Text = Begriff!.Begriff;
                NeuerBegriffKnopf.Text = "Skip Begriff";
            }
            else
            {
                BegriffLabel.Text = "Drücke \"Neuer Begriff\" um zu starten";
                NeuerBegriffKnopf.Text = "Neuer Begriff";
            }
        }

        private void AuflösungKnopf_Click(object sender, EventArgs e)
        {
            KorregierenKnopf.Visible = false;
            AuflösungKnopf.Visible = false;
            NeuerBegriffKnopf.Text = "Neuer Begriff";

            int confident = DamerauLevenshtein(Begriff!.Erklärung, ErklärungTextBox.Text);

            if (confident >= 50)
            {
                ErklärungTextBox.Text = @$"War nicht so korrekt, Confidence: {confident}%
{ErklärungTextBox.Text}

Erwartete Erklärung
{Begriff.Erklärung}";
            }
            else
            {
                ErklärungTextBox.Text = @$"Joa passt, Confidence: {confident}%
{ErklärungTextBox.Text}

Erwartete Erklärung
{Begriff!.Erklärung}";
                AddPunkt();
            }

            ErklärungTextBox.Find("Erwartete Erklärung");
            ErklärungTextBox.SelectionColor = Color.Yellow;

            CheckBegriffe();
        }

        private void KorregierenKnopf_MouseDown(object sender, MouseEventArgs e)
        {
            PrevText = ErklärungTextBox.Text;

            string differenz = Difference(Begriff!.Erklärung, ErklärungTextBox.Text)[..(ErklärungTextBox.Text.Length > Begriff.Erklärung.Length ? Begriff.Erklärung.Length : ErklärungTextBox.Text.Length)];

            ErklärungTextBox.Text = @$"{ErklärungTextBox.Text}
{differenz}";

            ErklärungTextBox.Find(differenz, RichTextBoxFinds.MatchCase);
            ErklärungTextBox.SelectionColor = Color.Yellow;
        }

        private void KorregierenKnopf_MouseUp(object sender, MouseEventArgs e)
        {
            ErklärungTextBox.Text = PrevText;
        }

        public static string Difference(string str1, string str2)
        {
            if (str1 == null)
            {
                return str2;
            }
            else if (str2 == null)
            {
                return str1;
            }

            List<string> set1 = str1.Split(' ').Distinct().ToList();
            List<string> set2 = str2.Split(' ').Distinct().ToList();

            List<string>? diff = set2.Count > set1.Count ? set2.Except(set1).ToList() : set1.Except(set2).ToList();

            return string.Join(" ", diff);
        }

        public static int DamerauLevenshtein(string original, string modified)
        {
            int len_orig = original.Length;
            int len_diff = modified.Length;

            int[,]? matrix = new int[len_orig + 1, len_diff + 1];
            for (int i = 0; i <= len_orig; i++)
            {
                matrix[i, 0] = i;
            }

            for (int j = 0; j <= len_diff; j++)
            {
                matrix[0, j] = j;
            }

            for (int i = 1; i <= len_orig; i++)
            {
                for (int j = 1; j <= len_diff; j++)
                {
                    int cost = modified[j - 1] == original[i - 1] ? 0 : 1;
                    int[]? vals = new int[] {
                    matrix[i - 1, j] + 1,
                    matrix[i, j - 1] + 1,
                    matrix[i - 1, j - 1] + cost
                };
                    matrix[i, j] = vals.Min();
                    if (i > 1 && j > 1 && original[i - 1] == modified[j - 2] && original[i - 2] == modified[j - 1])
                    {
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                    }
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