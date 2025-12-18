namespace EksamenSemEt.UI
{
    partial class HistoryControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tableLayoutPanel1 = new TableLayoutPanel();
            label2 = new Label();
            label1 = new Label();
            InfoGrid = new DataGridView();
            HistoryDGV = new DataGridView();
            label3 = new Label();
            AttendeesDGV = new DataGridView();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InfoGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HistoryDGV).BeginInit();
            ((System.ComponentModel.ISupportInitialize)AttendeesDGV).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58.6617775F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.324473F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35.01375F));
            tableLayoutPanel1.Controls.Add(label2, 0, 3);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(InfoGrid, 0, 1);
            tableLayoutPanel1.Controls.Add(HistoryDGV, 0, 4);
            tableLayoutPanel1.Controls.Add(label3, 2, 2);
            tableLayoutPanel1.Controls.Add(AttendeesDGV, 2, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 6.939502F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 11.2099648F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.252669F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 72.41993F));
            tableLayoutPanel1.Size = new Size(1091, 577);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 117);
            label2.Name = "label2";
            label2.Size = new Size(634, 52);
            label2.TabIndex = 2;
            label2.Text = "Historik :";
            label2.TextAlign = ContentAlignment.BottomLeft;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(634, 15);
            label1.TabIndex = 0;
            label1.Text = "Valgt Medlem / Instruktør : ";
            label1.TextAlign = ContentAlignment.BottomLeft;
            // 
            // InfoGrid
            // 
            InfoGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            InfoGrid.Dock = DockStyle.Fill;
            InfoGrid.Location = new Point(3, 18);
            InfoGrid.Name = "InfoGrid";
            tableLayoutPanel1.SetRowSpan(InfoGrid, 2);
            InfoGrid.Size = new Size(634, 96);
            InfoGrid.TabIndex = 1;
            // 
            // HistoryDGV
            // 
            HistoryDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            HistoryDGV.Dock = DockStyle.Fill;
            HistoryDGV.Location = new Point(3, 172);
            HistoryDGV.Name = "HistoryDGV";
            HistoryDGV.Size = new Size(634, 402);
            HistoryDGV.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(712, 54);
            label3.Name = "label3";
            label3.Size = new Size(376, 63);
            label3.TabIndex = 4;
            label3.Text = "Undervisere / Tilmeldte :";
            label3.TextAlign = ContentAlignment.BottomLeft;
            // 
            // AttendeesDGV
            // 
            AttendeesDGV.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AttendeesDGV.Dock = DockStyle.Fill;
            AttendeesDGV.Location = new Point(712, 120);
            AttendeesDGV.Name = "AttendeesDGV";
            tableLayoutPanel1.SetRowSpan(AttendeesDGV, 2);
            AttendeesDGV.Size = new Size(376, 454);
            AttendeesDGV.TabIndex = 5;
            // 
            // HistoryControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "HistoryControl";
            Size = new Size(1091, 577);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)InfoGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)HistoryDGV).EndInit();
            ((System.ComponentModel.ISupportInitialize)AttendeesDGV).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private DataGridView InfoGrid;
        private DataGridView HistoryDGV;
        private Label label3;
        private DataGridView AttendeesDGV;
    }
}
