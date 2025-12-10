namespace FitHubUI
{
    partial class Booking
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
            kommendeHold = new DataGridView();
            tilmeldteMedlemmer = new DataGridView();
            label1 = new Label();
            bookTilHold = new Button();
            afmeldMedlem = new Button();
            valgtMedlemmer = new CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)kommendeHold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tilmeldteMedlemmer).BeginInit();
            SuspendLayout();
            // 
            // kommendeHold
            // 
            kommendeHold.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            kommendeHold.Location = new Point(12, 12);
            kommendeHold.Name = "kommendeHold";
            kommendeHold.Size = new Size(240, 150);
            kommendeHold.TabIndex = 0;
            // 
            // tilmeldteMedlemmer
            // 
            tilmeldteMedlemmer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tilmeldteMedlemmer.Location = new Point(279, 12);
            tilmeldteMedlemmer.Name = "tilmeldteMedlemmer";
            tilmeldteMedlemmer.Size = new Size(240, 150);
            tilmeldteMedlemmer.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 189);
            label1.Name = "label1";
            label1.Size = new Size(83, 15);
            label1.TabIndex = 2;
            label1.Text = "Vælg Medlem:";
            // 
            // bookTilHold
            // 
            bookTilHold.Location = new Point(279, 185);
            bookTilHold.Name = "bookTilHold";
            bookTilHold.Size = new Size(103, 23);
            bookTilHold.TabIndex = 4;
            bookTilHold.Text = "Book Medlem";
            bookTilHold.UseVisualStyleBackColor = true;
            // 
            // afmeldMedlem
            // 
            afmeldMedlem.Location = new Point(416, 185);
            afmeldMedlem.Name = "afmeldMedlem";
            afmeldMedlem.Size = new Size(103, 23);
            afmeldMedlem.TabIndex = 5;
            afmeldMedlem.Text = "Afmeld Medlem";
            afmeldMedlem.UseVisualStyleBackColor = true;
            // 
            // valgtMedlemmer
            // 
            valgtMedlemmer.FormattingEnabled = true;
            valgtMedlemmer.Location = new Point(101, 189);
            valgtMedlemmer.Name = "valgtMedlemmer";
            valgtMedlemmer.Size = new Size(151, 94);
            valgtMedlemmer.TabIndex = 6;
            // 
            // Booking
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(578, 450);
            Controls.Add(valgtMedlemmer);
            Controls.Add(afmeldMedlem);
            Controls.Add(bookTilHold);
            Controls.Add(label1);
            Controls.Add(tilmeldteMedlemmer);
            Controls.Add(kommendeHold);
            Name = "Booking";
            Text = "Booking";
            ((System.ComponentModel.ISupportInitialize)kommendeHold).EndInit();
            ((System.ComponentModel.ISupportInitialize)tilmeldteMedlemmer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView kommendeHold;
        private DataGridView tilmeldteMedlemmer;
        private Label label1;
        private Button bookTilHold;
        private Button afmeldMedlem;
        private CheckedListBox valgtMedlemmer;
    }
}