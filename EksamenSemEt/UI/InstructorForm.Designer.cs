namespace Sem1BackupForms.Forms
{
    partial class InstructorForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            FirstNameTextBox = new TextBox();
            LastNameTextBox = new TextBox();
            label3 = new Label();
            CertificatesCreateCheckedList = new CheckedListBox();
            AddInstructorButton = new Button();
            tableLayoutPanel3 = new TableLayoutPanel();
            CertificateCheckedList = new CheckedListBox();
            InstructorListView = new DataGridView();
            label6 = new Label();
            SearchFieldText = new TextBox();
            label4 = new Label();
            pageSetupDialog1 = new PageSetupDialog();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InstructorListView).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel3, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.Size = new Size(2176, 1388);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 6;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.53108549F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.4457321F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.682028F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.9953918F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52.26554F));
            tableLayoutPanel2.Controls.Add(label1, 1, 1);
            tableLayoutPanel2.Controls.Add(label2, 1, 2);
            tableLayoutPanel2.Controls.Add(FirstNameTextBox, 2, 1);
            tableLayoutPanel2.Controls.Add(LastNameTextBox, 2, 2);
            tableLayoutPanel2.Controls.Add(label3, 3, 1);
            tableLayoutPanel2.Controls.Add(CertificatesCreateCheckedList, 4, 1);
            tableLayoutPanel2.Controls.Add(AddInstructorButton, 4, 3);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 4);
            tableLayoutPanel2.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel2.Size = new Size(2170, 408);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.Control;
            label1.Dock = DockStyle.Fill;
            label1.ForeColor = SystemColors.ControlText;
            label1.Location = new Point(3, 20);
            label1.Name = "label1";
            label1.Size = new Size(92, 163);
            label1.TabIndex = 14;
            label1.Text = "Fornavn* :";
            label1.TextAlign = ContentAlignment.TopRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.Control;
            label2.Dock = DockStyle.Fill;
            label2.ForeColor = SystemColors.ControlText;
            label2.Location = new Point(3, 183);
            label2.Name = "label2";
            label2.Size = new Size(92, 102);
            label2.TabIndex = 1;
            label2.Text = "Efternavn* :";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // FirstNameTextBox
            // 
            FirstNameTextBox.BackColor = SystemColors.Window;
            FirstNameTextBox.Dock = DockStyle.Fill;
            FirstNameTextBox.ForeColor = SystemColors.WindowText;
            FirstNameTextBox.Location = new Point(101, 24);
            FirstNameTextBox.Margin = new Padding(3, 4, 3, 4);
            FirstNameTextBox.Name = "FirstNameTextBox";
            FirstNameTextBox.Size = new Size(503, 27);
            FirstNameTextBox.TabIndex = 3;
            // 
            // LastNameTextBox
            // 
            LastNameTextBox.BackColor = SystemColors.Window;
            LastNameTextBox.Dock = DockStyle.Fill;
            LastNameTextBox.ForeColor = SystemColors.WindowText;
            LastNameTextBox.Location = new Point(101, 187);
            LastNameTextBox.Margin = new Padding(3, 4, 3, 4);
            LastNameTextBox.Name = "LastNameTextBox";
            LastNameTextBox.Size = new Size(503, 27);
            LastNameTextBox.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = SystemColors.Control;
            label3.Dock = DockStyle.Fill;
            label3.ForeColor = SystemColors.ControlText;
            label3.Location = new Point(610, 20);
            label3.Name = "label3";
            label3.Size = new Size(139, 163);
            label3.TabIndex = 15;
            label3.Text = "Certifikater:";
            label3.TextAlign = ContentAlignment.TopRight;
            // 
            // CertificatesCreateCheckedList
            // 
            CertificatesCreateCheckedList.BackColor = SystemColors.Window;
            CertificatesCreateCheckedList.CheckOnClick = true;
            CertificatesCreateCheckedList.Dock = DockStyle.Fill;
            CertificatesCreateCheckedList.ForeColor = SystemColors.WindowText;
            CertificatesCreateCheckedList.FormattingEnabled = true;
            CertificatesCreateCheckedList.Location = new Point(755, 24);
            CertificatesCreateCheckedList.Margin = new Padding(3, 4, 3, 4);
            CertificatesCreateCheckedList.Name = "CertificatesCreateCheckedList";
            tableLayoutPanel2.SetRowSpan(CertificatesCreateCheckedList, 2);
            CertificatesCreateCheckedList.Size = new Size(276, 257);
            CertificatesCreateCheckedList.TabIndex = 16;
            // 
            // AddInstructorButton
            // 
            AddInstructorButton.BackColor = SystemColors.Window;
            AddInstructorButton.Dock = DockStyle.Fill;
            AddInstructorButton.ForeColor = SystemColors.WindowText;
            AddInstructorButton.Location = new Point(755, 289);
            AddInstructorButton.Margin = new Padding(3, 4, 3, 4);
            AddInstructorButton.Name = "AddInstructorButton";
            AddInstructorButton.Size = new Size(276, 53);
            AddInstructorButton.TabIndex = 13;
            AddInstructorButton.Text = "Tilføj Instruktør";
            AddInstructorButton.UseVisualStyleBackColor = false;
            AddInstructorButton.Click += AddInstructorButton_Click;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.BackColor = SystemColors.Control;
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel3.Controls.Add(CertificateCheckedList, 2, 1);
            tableLayoutPanel3.Controls.Add(InstructorListView, 0, 1);
            tableLayoutPanel3.Controls.Add(label6, 0, 0);
            tableLayoutPanel3.Controls.Add(SearchFieldText, 1, 0);
            tableLayoutPanel3.Controls.Add(label4, 2, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.ForeColor = SystemColors.ControlText;
            tableLayoutPanel3.Location = new Point(3, 420);
            tableLayoutPanel3.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tableLayoutPanel3.Size = new Size(2170, 964);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // CertificateCheckedList
            // 
            CertificateCheckedList.BackColor = SystemColors.Window;
            CertificateCheckedList.CheckOnClick = true;
            CertificateCheckedList.Dock = DockStyle.Fill;
            CertificateCheckedList.ForeColor = SystemColors.WindowText;
            CertificateCheckedList.FormattingEnabled = true;
            CertificateCheckedList.Location = new Point(1747, 148);
            CertificateCheckedList.Margin = new Padding(3, 4, 3, 4);
            CertificateCheckedList.Name = "CertificateCheckedList";
            CertificateCheckedList.Size = new Size(420, 812);
            CertificateCheckedList.TabIndex = 17;
            // 
            // InstructorListView
            // 
            InstructorListView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel3.SetColumnSpan(InstructorListView, 2);
            InstructorListView.Dock = DockStyle.Fill;
            InstructorListView.Location = new Point(3, 148);
            InstructorListView.Margin = new Padding(3, 4, 3, 4);
            InstructorListView.Name = "InstructorListView";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            InstructorListView.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            InstructorListView.RowHeadersWidth = 51;
            InstructorListView.Size = new Size(1738, 812);
            InstructorListView.TabIndex = 3;
            InstructorListView.UserDeletingRow += InstructorListView_UserDeletingRow;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(3, 0);
            label6.Name = "label6";
            label6.Size = new Size(38, 144);
            label6.TabIndex = 1;
            label6.Text = "Søg:";
            label6.TextAlign = ContentAlignment.BottomRight;
            // 
            // SearchFieldText
            // 
            SearchFieldText.BackColor = SystemColors.HighlightText;
            SearchFieldText.BorderStyle = BorderStyle.FixedSingle;
            SearchFieldText.Dock = DockStyle.Bottom;
            SearchFieldText.ForeColor = SystemColors.WindowText;
            SearchFieldText.Location = new Point(47, 113);
            SearchFieldText.Margin = new Padding(3, 4, 3, 4);
            SearchFieldText.Name = "SearchFieldText";
            SearchFieldText.Size = new Size(1694, 27);
            SearchFieldText.TabIndex = 2;
            SearchFieldText.TextChanged += SearchFieldText_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = SystemColors.Control;
            label4.Dock = DockStyle.Bottom;
            label4.ForeColor = SystemColors.ControlText;
            label4.Location = new Point(1747, 124);
            label4.Name = "label4";
            label4.Size = new Size(420, 20);
            label4.TabIndex = 18;
            label4.Text = "Certifikater";
            // 
            // InstructorForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            Controls.Add(tableLayoutPanel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "InstructorForm";
            Size = new Size(2176, 1388);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)InstructorListView).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private CheckBox ActivityCheckBox;
        private MonthCalendar BirthdaySelector;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button AddInstructorButton;
        private Label label2;
        private TextBox LastNameTextBox;
        private TextBox FirstNameTextBox;
        private Label label1;
        private Label label3;
        private CheckedListBox CertificatesCreateCheckedList;
        private TableLayoutPanel tableLayoutPanel3;
        private CheckedListBox CertificateCheckedList;
        private DataGridView InstructorListView;
        private Label label6;
        private TextBox SearchFieldText;
        private Label label4;
        private PageSetupDialog pageSetupDialog1;
    }
}