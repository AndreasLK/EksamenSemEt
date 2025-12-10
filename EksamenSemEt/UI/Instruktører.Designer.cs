namespace FitHubUI
{
    partial class Instruktører
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.instruktørGrid = new System.Windows.Forms.DataGridView();
            this.navnInstruktør = new System.Windows.Forms.TextBox();
            this.emailInstruktør = new System.Windows.Forms.TextBox();
            this.certifikater = new System.Windows.Forms.ComboBox();
            this.opretInstruktør = new System.Windows.Forms.Button();
            this.gemInstruktør = new System.Windows.Forms.Button();
            this.sletInstruktør = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.vælgInstruktørKommendeHold = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.kommendeHold = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.instruktørGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kommendeHold)).BeginInit();
            this.SuspendLayout();
            // 
            // instruktørGrid
            // 
            this.instruktørGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.instruktørGrid.Location = new System.Drawing.Point(12, 12);
            this.instruktørGrid.Name = "instruktørGrid";
            this.instruktørGrid.RowTemplate.Height = 25;
            this.instruktørGrid.Size = new System.Drawing.Size(600, 180);
            this.instruktørGrid.TabIndex = 0;
            this.instruktørGrid.SelectionChanged += new System.EventHandler(this.instruktører_SelectionChanged);
            // 
            // navnInstruktør
            // 
            this.navnInstruktør.Location = new System.Drawing.Point(80, 210);
            this.navnInstruktør.Name = "navnInstruktør";
            this.navnInstruktør.Size = new System.Drawing.Size(200, 23);
            this.navnInstruktør.TabIndex = 1;
            // 
            // emailInstruktør
            // 
            this.emailInstruktør.Location = new System.Drawing.Point(80, 240);
            this.emailInstruktør.Name = "emailInstruktør";
            this.emailInstruktør.Size = new System.Drawing.Size(200, 23);
            this.emailInstruktør.TabIndex = 2;
            // 
            // certifikater
            // 
            this.certifikater.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.certifikater.FormattingEnabled = true;
            this.certifikater.Location = new System.Drawing.Point(290, 210);
            this.certifikater.Name = "certifikater";
            this.certifikater.Size = new System.Drawing.Size(150, 23);
            this.certifikater.TabIndex = 3;
            // 
            // opretInstruktør
            // 
            this.opretInstruktør.Location = new System.Drawing.Point(12, 280);
            this.opretInstruktør.Name = "opretInstruktør";
            this.opretInstruktør.Size = new System.Drawing.Size(75, 23);
            this.opretInstruktør.TabIndex = 4;
            this.opretInstruktør.Text = "Opret";
            this.opretInstruktør.UseVisualStyleBackColor = true;
            this.opretInstruktør.Click += new System.EventHandler(this.opretInstruktør_Click);
            // 
            // gemInstruktør
            // 
            this.gemInstruktør.Location = new System.Drawing.Point(110, 280);
            this.gemInstruktør.Name = "gemInstruktør";
            this.gemInstruktør.Size = new System.Drawing.Size(75, 23);
            this.gemInstruktør.TabIndex = 5;
            this.gemInstruktør.Text = "Gem";
            this.gemInstruktør.UseVisualStyleBackColor = true;
            this.gemInstruktør.Click += new System.EventHandler(this.gemInstruktør_Click);
            // 
            // sletInstruktør
            // 
            this.sletInstruktør.Location = new System.Drawing.Point(210, 280);
            this.sletInstruktør.Name = "sletInstruktør";
            this.sletInstruktør.Size = new System.Drawing.Size(75, 23);
            this.sletInstruktør.TabIndex = 6;
            this.sletInstruktør.Text = "Slet";
            this.sletInstruktør.UseVisualStyleBackColor = true;
            this.sletInstruktør.Click += new System.EventHandler(this.sletInstruktør_Click);
            // 
            // Labels ...
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 213);
            this.label1.Text = "Navn:";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 243);
            this.label2.Text = "Email:";
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(290, 192);
            this.label4.Text = "Certifikat:";
            // 
            // vælgInstruktørKommendeHold
            // 
            this.vælgInstruktørKommendeHold.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.vælgInstruktørKommendeHold.Location = new System.Drawing.Point(130, 330);
            this.vælgInstruktørKommendeHold.Name = "vælgInstruktørKommendeHold";
            this.vælgInstruktørKommendeHold.Size = new System.Drawing.Size(200, 23);
            this.vælgInstruktørKommendeHold.SelectedIndexChanged += new System.EventHandler(this.vælgInstruktørKommendeHold_SelectedIndexChanged);
            // 
            // label3 (Vælg instruktør)
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 333);
            this.label3.Text = "Vælg instruktør:";
            // 
            // kommendeHold
            // 
            this.kommendeHold.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.kommendeHold.Location = new System.Drawing.Point(12, 370);
            this.kommendeHold.Name = "kommendeHold";
            this.kommendeHold.Size = new System.Drawing.Size(600, 200);
            this.kommendeHold.TabIndex = 12;
            // 
            // Instruktører (Form settings)
            // 
            this.ClientSize = new System.Drawing.Size(640, 600);
            this.Controls.Add(this.kommendeHold);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.vælgInstruktørKommendeHold);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sletInstruktør);
            this.Controls.Add(this.gemInstruktør);
            this.Controls.Add(this.opretInstruktør);
            this.Controls.Add(this.certifikater);
            this.Controls.Add(this.emailInstruktør);
            this.Controls.Add(this.navnInstruktør);
            this.Controls.Add(this.instruktørGrid);
            this.Name = "Instruktører";
            this.Text = "Instruktøradministration";
            ((System.ComponentModel.ISupportInitialize)(this.instruktørGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kommendeHold)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView instruktørGrid;
        private System.Windows.Forms.TextBox navnInstruktør;
        private System.Windows.Forms.TextBox emailInstruktør;
        private System.Windows.Forms.ComboBox certifikater;
        private System.Windows.Forms.Button opretInstruktør;
        private System.Windows.Forms.Button gemInstruktør;
        private System.Windows.Forms.Button sletInstruktør;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox vælgInstruktørKommendeHold;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView kommendeHold;
    }
}