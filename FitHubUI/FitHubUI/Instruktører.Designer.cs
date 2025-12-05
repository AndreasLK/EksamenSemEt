namespace FitHubUI
{
    partial class Instruktører
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
            instuktører = new DataGridView();
            opretInstruktør = new Button();
            gemInstruktør = new Button();
            sletInstruktør = new Button();
            navnInstruktør = new TextBox();
            emailInstruktør = new TextBox();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            kommendeHold = new DataGridView();
            label3 = new Label();
            tilmeldteTilHold = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)instuktører).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kommendeHold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tilmeldteTilHold).BeginInit();
            SuspendLayout();
            // 
            // instuktører
            // 
            instuktører.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            instuktører.Location = new Point(12, 12);
            instuktører.Name = "instuktører";
            instuktører.Size = new Size(343, 132);
            instuktører.TabIndex = 0;
            // 
            // opretInstruktør
            // 
            opretInstruktør.Location = new Point(12, 208);
            opretInstruktør.Name = "opretInstruktør";
            opretInstruktør.Size = new Size(75, 23);
            opretInstruktør.TabIndex = 1;
            opretInstruktør.Text = "Opret";
            opretInstruktør.UseVisualStyleBackColor = true;
            // 
            // gemInstruktør
            // 
            gemInstruktør.Location = new Point(146, 208);
            gemInstruktør.Name = "gemInstruktør";
            gemInstruktør.Size = new Size(75, 23);
            gemInstruktør.TabIndex = 2;
            gemInstruktør.Text = "Gem";
            gemInstruktør.UseVisualStyleBackColor = true;
            // 
            // sletInstruktør
            // 
            sletInstruktør.Location = new Point(280, 208);
            sletInstruktør.Name = "sletInstruktør";
            sletInstruktør.Size = new Size(75, 23);
            sletInstruktør.TabIndex = 3;
            sletInstruktør.Text = "Slet";
            sletInstruktør.UseVisualStyleBackColor = true;
            // 
            // navnInstruktør
            // 
            navnInstruktør.Location = new Point(12, 150);
            navnInstruktør.Name = "navnInstruktør";
            navnInstruktør.Size = new Size(100, 23);
            navnInstruktør.TabIndex = 4;
            navnInstruktør.Text = "Navn";
            // 
            // emailInstruktør
            // 
            emailInstruktør.Location = new Point(12, 179);
            emailInstruktør.Name = "emailInstruktør";
            emailInstruktør.Size = new Size(100, 23);
            emailInstruktør.TabIndex = 5;
            emailInstruktør.Text = "E-mail";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(108, 271);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(121, 23);
            comboBox1.TabIndex = 6;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 274);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 274);
            label2.Name = "label2";
            label2.Size = new Size(90, 15);
            label2.TabIndex = 8;
            label2.Text = "Vælg Instruktør:";
            // 
            // kommendeHold
            // 
            kommendeHold.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            kommendeHold.Location = new Point(12, 300);
            kommendeHold.Name = "kommendeHold";
            kommendeHold.Size = new Size(343, 108);
            kommendeHold.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 446);
            label3.Name = "label3";
            label3.Size = new Size(86, 15);
            label3.TabIndex = 10;
            label3.Text = "Vælg tilmeldte:";
            // 
            // tilmeldteTilHold
            // 
            tilmeldteTilHold.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tilmeldteTilHold.Location = new Point(12, 467);
            tilmeldteTilHold.Name = "tilmeldteTilHold";
            tilmeldteTilHold.Size = new Size(343, 108);
            tilmeldteTilHold.TabIndex = 11;
            // 
            // Instruktører
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 587);
            Controls.Add(tilmeldteTilHold);
            Controls.Add(label3);
            Controls.Add(kommendeHold);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(emailInstruktør);
            Controls.Add(navnInstruktør);
            Controls.Add(sletInstruktør);
            Controls.Add(gemInstruktør);
            Controls.Add(opretInstruktør);
            Controls.Add(instuktører);
            Name = "Instruktører";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)instuktører).EndInit();
            ((System.ComponentModel.ISupportInitialize)kommendeHold).EndInit();
            ((System.ComponentModel.ISupportInitialize)tilmeldteTilHold).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView instuktører;
        private Button opretInstruktør;
        private Button gemInstruktør;
        private Button sletInstruktør;
        private TextBox navnInstruktør;
        private TextBox emailInstruktør;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private DataGridView kommendeHold;
        private Label label3;
        private DataGridView tilmeldteTilHold;
    }
}