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

using HistoryMindLernen.Mobile.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

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

        private void NeuerBegriff_Clicked(object sender, EventArgs e)
        {
            StartRounde();

            ErklärungTextBox.Text = null;
            SchiebBegriffe();

            if (CheckBegriffe())
            {
                BegriffLabel.Text = Begriff.Begriff;
                NeuerBegriffKnopf.Text = "Skip Begriff";
            }
            else
            {
                BegriffLabel.Text = "Drücke \"Neuer Begriff\" um zu starten";
                NeuerBegriffKnopf.Text = "Neuer Begriff";
            }
        }

        private void Korrektur_Pressed(object sender, EventArgs e)
        {
            PrevText = ErklärungTextBox.Text;

            if (ErklärungTextBox.Text.Length > Begriff.Erklärung.Length)
            {
            }

            string differenz = Difference(Begriff.Erklärung, ErklärungTextBox.Text).Substring(0,
                (ErklärungTextBox.Text?.Length ?? 0) > Begriff.Erklärung.Length ? Begriff.Erklärung.Length : ErklärungTextBox.Text.Length);
            // What the fuck is this code? (If user types longer string then the original, substring fails fix)

            ErklärungTextBox.Text = $@"{ErklärungTextBox.Text}
{differenz}";
        }

        private void Korrektur_Released(object sender, EventArgs e)
        {
            ErklärungTextBox.Text = PrevText;
        }

        private void Auflösung_Clicked(object sender, EventArgs e)
        {
            KorregierenKnopf.IsVisible = false;
            AuflösungKnopf.IsVisible = false;
            NeuerBegriffKnopf.Text = "Neuer Begriff";

            int confident = DamerauLevenshtein(Begriff.Erklärung, ErklärungTextBox.Text ?? string.Empty);

            if (confident >= 50)
            {
                ErklärungTextBox.Text = $@"War nicht so korrekt, Confidence: {confident}%
{ErklärungTextBox.Text}

Erwartete Erklärung
{Begriff.Erklärung}";
            }
            else
            {
                ErklärungTextBox.Text = $@"Joa passt, Confidence: {confident}%
{ErklärungTextBox.Text}

Erwartete Erklärung
{Begriff.Erklärung}";
                AddPunkt();
            }

            //this.ErklärungTextBox.("Erwartete Erklärung");
            //this.ErklärungTextBox.SelectionColor = Color.Yellow;

            CheckBegriffe();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (!HistoryMindCheckBox.IsChecked && !HistoryMind2CheckBox.IsChecked && !HistoryMind3CheckBox.IsChecked)
            {
                HistoryMindCheckBox.IsChecked = true;
            }
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
                AuflösungKnopf.IsVisible = false;
                KorregierenKnopf.IsVisible = false;
                PunkteLabel.IsVisible = false;

                ErklärungTextBox.Text = $"Note: {6 - Punkte}";
                ClearPunkt();
                HistoryMindGroupBox.IsVisible = true;

                return false;
            }
            return true;
        }

        private void LoadBegriffe()
        {
            Controller controller = Controller.GetInstance();
            (bool, bool, bool) HistoryMinds = (HistoryMindCheckBox.IsChecked, HistoryMind2CheckBox.IsChecked, HistoryMind3CheckBox.IsChecked);

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

        private void StartRounde()
        {
            if (!KorregierenKnopf.IsVisible)
            {
                KorregierenKnopf.IsVisible = true;
            }

            if (!AuflösungKnopf.IsVisible)
            {
                AuflösungKnopf.IsVisible = true;
            }

            if (!PunkteLabel.IsVisible)
            {
                PunkteLabel.IsVisible = true;
            }

            if (Begriffe.Count == 0)
            {
                LoadBegriffe();
            }

            if (HistoryMindGroupBox.IsVisible)
            {
                HistoryMindGroupBox.IsVisible = false;
            }
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

            List<string> diff = set2.Count > set1.Count ? set2.Except(set1).ToList() : set1.Except(set2).ToList();

            return string.Join(" ", diff);
        }

        public static int DamerauLevenshtein(string original, string modified)
        {
            int len_orig = original.Length;
            int len_diff = modified.Length;

            int[,] matrix = new int[len_orig + 1, len_diff + 1];
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
                    int[] vals = new int[] {
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
    }
}