namespace FitHubUI
{
    partial class Planlægning
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            holdType = new ComboBox();
            Instruktør = new ComboBox();
            startDato = new DateTimePicker();
            maxAntalPåHold = new NumericUpDown();
            opretHold = new Button();
            opdaterHold = new Button();
            sletHold = new Button();
            kommendeHold = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)maxAntalPåHold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kommendeHold).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 0;
            label1.Text = "Holdtype:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 38);
            label2.Name = "label2";
            label2.Size = new Size(61, 15);
            label2.TabIndex = 1;
            label2.Text = "Instruktør:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 69);
            label3.Name = "label3";
            label3.Size = new Size(61, 15);
            label3.TabIndex = 2;
            label3.Text = "Start dato:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 100);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 3;
            label4.Text = "Max antal:";
            // 
            // holdType
            // 
            holdType.FormattingEnabled = true;
            holdType.Location = new Point(77, 6);
            holdType.Name = "holdType";
            holdType.Size = new Size(121, 23);
            holdType.TabIndex = 4;
            // 
            // Instruktør
            // 
            Instruktør.FormattingEnabled = true;
            Instruktør.Location = new Point(77, 35);
            Instruktør.Name = "Instruktør";
            Instruktør.Size = new Size(121, 23);
            Instruktør.TabIndex = 5;
            // 
            // startDato
            // 
            startDato.Location = new Point(77, 63);
            startDato.Name = "startDato";
            startDato.Size = new Size(200, 23);
            startDato.TabIndex = 6;
            // 
            // maxAntalPåHold
            // 
            maxAntalPåHold.Location = new Point(77, 98);
            maxAntalPåHold.Name = "maxAntalPåHold";
            maxAntalPåHold.Size = new Size(120, 23);
            maxAntalPåHold.TabIndex = 7;
            // 
            // opretHold
            // 
            opretHold.Location = new Point(12, 140);
            opretHold.Name = "opretHold";
            opretHold.Size = new Size(75, 23);
            opretHold.TabIndex = 8;
            opretHold.Text = "Opret";
            opretHold.UseVisualStyleBackColor = true;
            // 
            // opdaterHold
            // 
            opdaterHold.Location = new Point(108, 140);
            opdaterHold.Name = "opdaterHold";
            opdaterHold.Size = new Size(75, 23);
            opdaterHold.TabIndex = 9;
            opdaterHold.Text = "Opdater";
            opdaterHold.UseVisualStyleBackColor = true;
            // 
            // sletHold
            // 
            sletHold.Location = new Point(202, 140);
            sletHold.Name = "sletHold";
            sletHold.Size = new Size(75, 23);
            sletHold.TabIndex = 10;
            sletHold.Text = "Slet";
            sletHold.UseVisualStyleBackColor = true;
            // 
            // kommendeHold
            // 
            kommendeHold.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            kommendeHold.Location = new Point(12, 181);
            kommendeHold.Name = "kommendeHold";
            kommendeHold.Size = new Size(265, 150);
            kommendeHold.TabIndex = 11;
            // 
            // Planlægning
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(kommendeHold);
            Controls.Add(sletHold);
            Controls.Add(opdaterHold);
            Controls.Add(opretHold);
            Controls.Add(maxAntalPåHold);
            Controls.Add(startDato);
            Controls.Add(Instruktør);
            Controls.Add(holdType);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Planlægning";
            Text = "Planlægning";
            ((System.ComponentModel.ISupportInitialize)maxAntalPåHold).EndInit();
            ((System.ComponentModel.ISupportInitialize)kommendeHold).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox holdType;
        private ComboBox Instruktør;
        private DateTimePicker startDato;
        private NumericUpDown maxAntalPåHold;
        private Button opretHold;
        private Button opdaterHold;
        private Button sletHold;
        private DataGridView kommendeHold;
    }
}