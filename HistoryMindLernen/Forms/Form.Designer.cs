namespace HistoryMindLernen
{
    partial class Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.BegriffLabel = new System.Windows.Forms.Label();
            this.NeuerBegriffKnopf = new System.Windows.Forms.Button();
            this.ErklärungTextBox = new System.Windows.Forms.RichTextBox();
            this.AuflösungKnopf = new System.Windows.Forms.Button();
            this.PunkteLabel = new System.Windows.Forms.Label();
            this.KorregierenKnopf = new System.Windows.Forms.Button();
            this.HistoryMindGroupBox = new System.Windows.Forms.GroupBox();
            this.HistoryMind3CheckBox = new System.Windows.Forms.CheckBox();
            this.HistoryMind2CheckBox = new System.Windows.Forms.CheckBox();
            this.HistoryMindCheckBox = new System.Windows.Forms.CheckBox();
            this.TempLabel = new System.Windows.Forms.Label();
            this.HistoryMindGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // BegriffLabel
            // 
            this.BegriffLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BegriffLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.BegriffLabel.Location = new System.Drawing.Point(372, 56);
            this.BegriffLabel.Name = "BegriffLabel";
            this.BegriffLabel.Size = new System.Drawing.Size(336, 37);
            this.BegriffLabel.TabIndex = 0;
            this.BegriffLabel.Text = "Drücke \"Neuer Begriff\" um zu starten\r\n\r\n";
            this.BegriffLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NeuerBegriffKnopf
            // 
            this.NeuerBegriffKnopf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.NeuerBegriffKnopf.Cursor = System.Windows.Forms.Cursors.Default;
            this.NeuerBegriffKnopf.FlatAppearance.BorderSize = 0;
            this.NeuerBegriffKnopf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NeuerBegriffKnopf.Location = new System.Drawing.Point(278, 481);
            this.NeuerBegriffKnopf.Name = "NeuerBegriffKnopf";
            this.NeuerBegriffKnopf.Size = new System.Drawing.Size(171, 31);
            this.NeuerBegriffKnopf.TabIndex = 1;
            this.NeuerBegriffKnopf.Text = "Neuer Begriff";
            this.NeuerBegriffKnopf.UseVisualStyleBackColor = false;
            this.NeuerBegriffKnopf.Click += new System.EventHandler(this.NeuerBegriffKnopf_Click);
            // 
            // ErklärungTextBox
            // 
            this.ErklärungTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(62)))), ((int)(((byte)(70)))));
            this.ErklärungTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ErklärungTextBox.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ErklärungTextBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.ErklärungTextBox.Location = new System.Drawing.Point(72, 124);
            this.ErklärungTextBox.Name = "ErklärungTextBox";
            this.ErklärungTextBox.Size = new System.Drawing.Size(947, 295);
            this.ErklärungTextBox.TabIndex = 2;
            this.ErklärungTextBox.Text = "";
            // 
            // AuflösungKnopf
            // 
            this.AuflösungKnopf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.AuflösungKnopf.Cursor = System.Windows.Forms.Cursors.Default;
            this.AuflösungKnopf.FlatAppearance.BorderSize = 0;
            this.AuflösungKnopf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AuflösungKnopf.Location = new System.Drawing.Point(660, 481);
            this.AuflösungKnopf.Name = "AuflösungKnopf";
            this.AuflösungKnopf.Size = new System.Drawing.Size(171, 31);
            this.AuflösungKnopf.TabIndex = 3;
            this.AuflösungKnopf.Text = "Auflösung";
            this.AuflösungKnopf.UseVisualStyleBackColor = false;
            this.AuflösungKnopf.Visible = false;
            this.AuflösungKnopf.Click += new System.EventHandler(this.AuflösungKnopf_Click);
            // 
            // PunkteLabel
            // 
            this.PunkteLabel.AutoSize = true;
            this.PunkteLabel.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PunkteLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.PunkteLabel.Location = new System.Drawing.Point(936, 478);
            this.PunkteLabel.Name = "PunkteLabel";
            this.PunkteLabel.Size = new System.Drawing.Size(111, 28);
            this.PunkteLabel.TabIndex = 4;
            this.PunkteLabel.Text = "Punkte: 0/6";
            this.PunkteLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.PunkteLabel.Visible = false;
            // 
            // KorregierenKnopf
            // 
            this.KorregierenKnopf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(105)))));
            this.KorregierenKnopf.Cursor = System.Windows.Forms.Cursors.Default;
            this.KorregierenKnopf.FlatAppearance.BorderSize = 0;
            this.KorregierenKnopf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KorregierenKnopf.Location = new System.Drawing.Point(471, 481);
            this.KorregierenKnopf.Name = "KorregierenKnopf";
            this.KorregierenKnopf.Size = new System.Drawing.Size(171, 31);
            this.KorregierenKnopf.TabIndex = 5;
            this.KorregierenKnopf.Text = "Korregieren (Hilfe)";
            this.KorregierenKnopf.UseVisualStyleBackColor = false;
            this.KorregierenKnopf.Visible = false;
            this.KorregierenKnopf.MouseDown += new System.Windows.Forms.MouseEventHandler(this.KorregierenKnopf_MouseDown);
            this.KorregierenKnopf.MouseUp += new System.Windows.Forms.MouseEventHandler(this.KorregierenKnopf_MouseUp);
            // 
            // HistoryMindGroupBox
            // 
            this.HistoryMindGroupBox.Controls.Add(this.HistoryMind3CheckBox);
            this.HistoryMindGroupBox.Controls.Add(this.HistoryMind2CheckBox);
            this.HistoryMindGroupBox.Controls.Add(this.HistoryMindCheckBox);
            this.HistoryMindGroupBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HistoryMindGroupBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.HistoryMindGroupBox.Location = new System.Drawing.Point(12, 436);
            this.HistoryMindGroupBox.Name = "HistoryMindGroupBox";
            this.HistoryMindGroupBox.Size = new System.Drawing.Size(173, 121);
            this.HistoryMindGroupBox.TabIndex = 6;
            this.HistoryMindGroupBox.TabStop = false;
            this.HistoryMindGroupBox.Text = "History Mind auswählen";
            // 
            // HistoryMind3CheckBox
            // 
            this.HistoryMind3CheckBox.AutoSize = true;
            this.HistoryMind3CheckBox.Checked = true;
            this.HistoryMind3CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HistoryMind3CheckBox.Location = new System.Drawing.Point(30, 82);
            this.HistoryMind3CheckBox.Name = "HistoryMind3CheckBox";
            this.HistoryMind3CheckBox.Size = new System.Drawing.Size(104, 19);
            this.HistoryMind3CheckBox.TabIndex = 2;
            this.HistoryMind3CheckBox.Text = "History Mind 3";
            this.HistoryMind3CheckBox.UseVisualStyleBackColor = true;
            this.HistoryMind3CheckBox.CheckedChanged += new System.EventHandler(this.HistoryMindCheckBox_CheckedChanged);
            // 
            // HistoryMind2CheckBox
            // 
            this.HistoryMind2CheckBox.AutoSize = true;
            this.HistoryMind2CheckBox.Checked = true;
            this.HistoryMind2CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HistoryMind2CheckBox.Location = new System.Drawing.Point(30, 57);
            this.HistoryMind2CheckBox.Name = "HistoryMind2CheckBox";
            this.HistoryMind2CheckBox.Size = new System.Drawing.Size(104, 19);
            this.HistoryMind2CheckBox.TabIndex = 1;
            this.HistoryMind2CheckBox.Text = "History Mind 2";
            this.HistoryMind2CheckBox.UseVisualStyleBackColor = true;
            this.HistoryMind2CheckBox.CheckedChanged += new System.EventHandler(this.HistoryMindCheckBox_CheckedChanged);
            // 
            // HistoryMindCheckBox
            // 
            this.HistoryMindCheckBox.AutoSize = true;
            this.HistoryMindCheckBox.Checked = true;
            this.HistoryMindCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.HistoryMindCheckBox.Location = new System.Drawing.Point(30, 33);
            this.HistoryMindCheckBox.Name = "HistoryMindCheckBox";
            this.HistoryMindCheckBox.Size = new System.Drawing.Size(104, 19);
            this.HistoryMindCheckBox.TabIndex = 0;
            this.HistoryMindCheckBox.Text = "History Mind 1";
            this.HistoryMindCheckBox.UseVisualStyleBackColor = true;
            this.HistoryMindCheckBox.CheckedChanged += new System.EventHandler(this.HistoryMindCheckBox_CheckedChanged);
            // 
            // TempLabel
            // 
            this.TempLabel.AutoSize = true;
            this.TempLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.TempLabel.Location = new System.Drawing.Point(722, 560);
            this.TempLabel.Name = "TempLabel";
            this.TempLabel.Size = new System.Drawing.Size(380, 15);
            this.TempLabel.TabIndex = 3;
            this.TempLabel.Text = "BETA | Alles was man sieht oder macht kann sich in der Zukunft ändern";
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(49)))));
            this.ClientSize = new System.Drawing.Size(1114, 584);
            this.Controls.Add(this.TempLabel);
            this.Controls.Add(this.HistoryMindGroupBox);
            this.Controls.Add(this.KorregierenKnopf);
            this.Controls.Add(this.PunkteLabel);
            this.Controls.Add(this.AuflösungKnopf);
            this.Controls.Add(this.ErklärungTextBox);
            this.Controls.Add(this.NeuerBegriffKnopf);
            this.Controls.Add(this.BegriffLabel);
            this.Name = "Form";
            this.Text = "Geschichte | History Mind lernen leicht gemacht";
            this.HistoryMindGroupBox.ResumeLayout(false);
            this.HistoryMindGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label BegriffLabel;
        private Button NeuerBegriffKnopf;
        private RichTextBox ErklärungTextBox;
        private Button AuflösungKnopf;
        private Label PunkteLabel;
        private Button KorregierenKnopf;
        private GroupBox HistoryMindGroupBox;
        private CheckBox HistoryMind3CheckBox;
        private CheckBox HistoryMind2CheckBox;
        private CheckBox HistoryMindCheckBox;
        private Label TempLabel;
    }
}