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
            tildmeldteMedlemmer = new DataGridView();
            label1 = new Label();
            comboBox1 = new ComboBox();
            bookTilHold = new Button();
            ((System.ComponentModel.ISupportInitialize)kommendeHold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tildmeldteMedlemmer).BeginInit();
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
            // tildmeldteMedlemmer
            // 
            tildmeldteMedlemmer.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tildmeldteMedlemmer.Location = new Point(279, 12);
            tildmeldteMedlemmer.Name = "tildmeldteMedlemmer";
            tildmeldteMedlemmer.Size = new Size(240, 150);
            tildmeldteMedlemmer.TabIndex = 1;
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
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(101, 186);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 3;
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
            // Booking
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(bookTilHold);
            Controls.Add(comboBox1);
            Controls.Add(label1);
            Controls.Add(tildmeldteMedlemmer);
            Controls.Add(kommendeHold);
            Name = "Booking";
            Text = "Booking";
            ((System.ComponentModel.ISupportInitialize)kommendeHold).EndInit();
            ((System.ComponentModel.ISupportInitialize)tildmeldteMedlemmer).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView kommendeHold;
        private DataGridView tildmeldteMedlemmer;
        private Label label1;
        private ComboBox comboBox1;
        private Button bookTilHold;
    }
}