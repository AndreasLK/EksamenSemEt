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
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            AddInstructorButton = new Button();
            label2 = new Label();
            LastNameTextBox = new TextBox();
            FirstNameTextBox = new TextBox();
            label1 = new Label();
            label3 = new Label();
            checkedListBox1 = new CheckedListBox();
            tableLayoutPanel3 = new TableLayoutPanel();
            checkedListBox2 = new CheckedListBox();
            memberListView = new DataGridView();
            label6 = new Label();
            SearchFieldText = new TextBox();
            label4 = new Label();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)memberListView).BeginInit();
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
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 70F));
            tableLayoutPanel1.Size = new Size(1904, 1041);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 6;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.7468872F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.7468872F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20.7468872F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.8630676F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24.8962669F));
            tableLayoutPanel2.Controls.Add(AddInstructorButton, 4, 3);
            tableLayoutPanel2.Controls.Add(label2, 2, 2);
            tableLayoutPanel2.Controls.Add(LastNameTextBox, 3, 2);
            tableLayoutPanel2.Controls.Add(FirstNameTextBox, 3, 1);
            tableLayoutPanel2.Controls.Add(label1, 2, 1);
            tableLayoutPanel2.Controls.Add(label3, 4, 1);
            tableLayoutPanel2.Controls.Add(checkedListBox1, 5, 1);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 5;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel2.Size = new Size(1898, 306);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // AddInstructorButton
            // 
            AddInstructorButton.Dock = DockStyle.Fill;
            AddInstructorButton.Location = new Point(1182, 216);
            AddInstructorButton.Name = "AddInstructorButton";
            AddInstructorButton.Size = new Size(238, 39);
            AddInstructorButton.TabIndex = 13;
            AddInstructorButton.Text = "Tilføj Instruktør";
            AddInstructorButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(396, 137);
            label2.Name = "label2";
            label2.Size = new Size(68, 15);
            label2.TabIndex = 1;
            label2.Text = "Efternavn* :";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LastNameTextBox
            // 
            LastNameTextBox.Location = new Point(789, 140);
            LastNameTextBox.Name = "LastNameTextBox";
            LastNameTextBox.Size = new Size(257, 23);
            LastNameTextBox.TabIndex = 2;
            // 
            // FirstNameTextBox
            // 
            FirstNameTextBox.Location = new Point(789, 18);
            FirstNameTextBox.Name = "FirstNameTextBox";
            FirstNameTextBox.Size = new Size(257, 23);
            FirstNameTextBox.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(396, 15);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 14;
            label1.Text = "Fornavn* :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1182, 15);
            label3.Name = "label3";
            label3.Size = new Size(68, 15);
            label3.TabIndex = 15;
            label3.Text = "Certifikater:";
            // 
            // checkedListBox1
            // 
            checkedListBox1.Dock = DockStyle.Fill;
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(1426, 18);
            checkedListBox1.Name = "checkedListBox1";
            tableLayoutPanel2.SetRowSpan(checkedListBox1, 2);
            checkedListBox1.Size = new Size(469, 192);
            checkedListBox1.TabIndex = 16;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 3;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel3.Controls.Add(checkedListBox2, 2, 1);
            tableLayoutPanel3.Controls.Add(memberListView, 0, 1);
            tableLayoutPanel3.Controls.Add(label6, 0, 0);
            tableLayoutPanel3.Controls.Add(SearchFieldText, 1, 0);
            tableLayoutPanel3.Controls.Add(label4, 2, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 315);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
            tableLayoutPanel3.Size = new Size(1898, 723);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // checkedListBox2
            // 
            checkedListBox2.Dock = DockStyle.Fill;
            checkedListBox2.FormattingEnabled = true;
            checkedListBox2.Location = new Point(1528, 111);
            checkedListBox2.Name = "checkedListBox2";
            checkedListBox2.Size = new Size(367, 609);
            checkedListBox2.TabIndex = 17;
            // 
            // memberListView
            // 
            memberListView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel3.SetColumnSpan(memberListView, 2);
            memberListView.Dock = DockStyle.Fill;
            memberListView.Location = new Point(3, 111);
            memberListView.Name = "memberListView";
            memberListView.Size = new Size(1519, 609);
            memberListView.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(3, 0);
            label6.Name = "label6";
            label6.Size = new Size(30, 108);
            label6.TabIndex = 1;
            label6.Text = "Søg:";
            label6.TextAlign = ContentAlignment.BottomRight;
            // 
            // SearchFieldText
            // 
            SearchFieldText.Dock = DockStyle.Bottom;
            SearchFieldText.Location = new Point(39, 82);
            SearchFieldText.Name = "SearchFieldText";
            SearchFieldText.Size = new Size(1483, 23);
            SearchFieldText.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Bottom;
            label4.Location = new Point(1528, 93);
            label4.Name = "label4";
            label4.Size = new Size(367, 15);
            label4.TabIndex = 18;
            label4.Text = "Certifikater";
            // 
            // InstructorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "InstructorForm";
            Size = new Size(1904, 1041);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)memberListView).EndInit();
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
        private CheckedListBox checkedListBox1;
        private TableLayoutPanel tableLayoutPanel3;
        private CheckedListBox checkedListBox2;
        private DataGridView memberListView;
        private Label label6;
        private TextBox SearchFieldText;
        private Label label4;
    }
}