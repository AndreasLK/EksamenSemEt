namespace FitHubUI
{
    partial class Kundestyring
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listeAfKunder = new DataGridView();
            kundeNavn = new TextBox();
            kundeEmail = new TextBox();
            kundeAktiv = new CheckBox();
            opretKunde = new Button();
            gemKunde = new Button();
            deaktiverKunde = new Button();
            ((System.ComponentModel.ISupportInitialize)listeAfKunder).BeginInit();
            SuspendLayout();
            // 
            // listeAfKunder
            // 
            listeAfKunder.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            listeAfKunder.Location = new Point(12, 12);
            listeAfKunder.Name = "listeAfKunder";
            listeAfKunder.Size = new Size(240, 150);
            listeAfKunder.TabIndex = 0;
            // 
            // kundeNavn
            // 
            kundeNavn.Location = new Point(272, 12);
            kundeNavn.Name = "kundeNavn";
            kundeNavn.Size = new Size(100, 23);
            kundeNavn.TabIndex = 1;
            kundeNavn.Text = "Navn";
            // 
            // kundeEmail
            // 
            kundeEmail.Location = new Point(272, 54);
            kundeEmail.Name = "kundeEmail";
            kundeEmail.Size = new Size(100, 23);
            kundeEmail.TabIndex = 2;
            kundeEmail.Text = "E-Mail";
            // 
            // kundeAktiv
            // 
            kundeAktiv.AutoSize = true;
            kundeAktiv.Location = new Point(272, 83);
            kundeAktiv.Name = "kundeAktiv";
            kundeAktiv.Size = new Size(53, 19);
            kundeAktiv.TabIndex = 4;
            kundeAktiv.Text = "Aktiv";
            kundeAktiv.UseVisualStyleBackColor = true;
            // 
            // opretKunde
            // 
            opretKunde.Location = new Point(258, 108);
            opretKunde.Name = "opretKunde";
            opretKunde.Size = new Size(75, 23);
            opretKunde.TabIndex = 5;
            opretKunde.Text = "Opret";
            opretKunde.UseVisualStyleBackColor = true;
            // 
            // gemKunde
            // 
            gemKunde.Location = new Point(339, 108);
            gemKunde.Name = "gemKunde";
            gemKunde.Size = new Size(75, 23);
            gemKunde.TabIndex = 6;
            gemKunde.Text = "Gem";
            gemKunde.UseVisualStyleBackColor = true;
            // 
            // deaktiverKunde
            // 
            deaktiverKunde.Location = new Point(258, 137);
            deaktiverKunde.Name = "deaktiverKunde";
            deaktiverKunde.Size = new Size(75, 23);
            deaktiverKunde.TabIndex = 7;
            deaktiverKunde.Text = "Deaktiver";
            deaktiverKunde.UseVisualStyleBackColor = true;
            // 
            // Kundestyring
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(deaktiverKunde);
            Controls.Add(gemKunde);
            Controls.Add(opretKunde);
            Controls.Add(kundeAktiv);
            Controls.Add(kundeEmail);
            Controls.Add(kundeNavn);
            Controls.Add(listeAfKunder);
            Name = "Kundestyring";
            Text = "Kundestyring";
            ((System.ComponentModel.ISupportInitialize)listeAfKunder).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView listeAfKunder;
        private TextBox kundeNavn;
        private TextBox kundeEmail;
        private CheckBox kundeAktiv;
        private Button opretKunde;
        private Button gemKunde;
        private Button deaktiverKunde;
    }
}