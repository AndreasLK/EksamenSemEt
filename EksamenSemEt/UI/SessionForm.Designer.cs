namespace EksamenSemEt.UI
{
    partial class SessionForm
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
            label9 = new Label();
            label7 = new Label();
            label1 = new Label();
            SessionTypeComboBox = new ComboBox();
            label2 = new Label();
            LocationComboBox = new ComboBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SessionDatePicker = new DateTimePicker();
            SessionStartTimePicker = new DateTimePicker();
            SessionEndTimePicker = new DateTimePicker();
            label6 = new Label();
            InstructorSearchFieldTextBox = new TextBox();
            InstructorsCheckedList = new CheckedListBox();
            CreateSessionButton = new Button();
            label8 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            dataGridView1 = new DataGridView();
            SessionTypeSearchComboBox = new ComboBox();
            LocationSearchComboBox = new ComboBox();
            SessionStartSearchDatePicker = new DateTimePicker();
            SessionEndSearchDatePicker = new DateTimePicker();
            tableLayoutPanel1 = new TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Dock = DockStyle.Fill;
            label9.Location = new Point(98, 364);
            label9.Name = "label9";
            label9.Size = new Size(89, 62);
            label9.TabIndex = 16;
            label9.Text = "Filtrer Efter: ";
            label9.TextAlign = ContentAlignment.BottomRight;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Location = new Point(894, 52);
            label7.Name = "label7";
            label7.Size = new Size(39, 34);
            label7.TabIndex = 11;
            label7.Text = "Søg:";
            label7.TextAlign = ContentAlignment.BottomCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(89, 52);
            label1.TabIndex = 0;
            label1.Text = "Hold Type* :";
            label1.TextAlign = ContentAlignment.BottomRight;
            // 
            // SessionTypeComboBox
            // 
            tableLayoutPanel1.SetColumnSpan(SessionTypeComboBox, 3);
            SessionTypeComboBox.Dock = DockStyle.Bottom;
            SessionTypeComboBox.FormattingEnabled = true;
            SessionTypeComboBox.Location = new Point(98, 26);
            SessionTypeComboBox.Name = "SessionTypeComboBox";
            SessionTypeComboBox.Size = new Size(279, 23);
            SessionTypeComboBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 86);
            label2.Name = "label2";
            label2.Size = new Size(89, 37);
            label2.TabIndex = 1;
            label2.Text = "Lokale :";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // LocationComboBox
            // 
            tableLayoutPanel1.SetColumnSpan(LocationComboBox, 3);
            LocationComboBox.Dock = DockStyle.Top;
            LocationComboBox.FormattingEnabled = true;
            LocationComboBox.Location = new Point(98, 89);
            LocationComboBox.Name = "LocationComboBox";
            LocationComboBox.Size = new Size(279, 23);
            LocationComboBox.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(464, 0);
            label3.Name = "label3";
            label3.Size = new Size(62, 52);
            label3.TabIndex = 4;
            label3.Text = "Dato* :";
            label3.TextAlign = ContentAlignment.BottomRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(464, 52);
            label4.Name = "label4";
            label4.Size = new Size(62, 34);
            label4.TabIndex = 5;
            label4.Text = "Start* :";
            label4.TextAlign = ContentAlignment.BottomRight;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(464, 86);
            label5.Name = "label5";
            label5.Size = new Size(62, 37);
            label5.TabIndex = 6;
            label5.Text = "Slut* :";
            label5.TextAlign = ContentAlignment.BottomRight;
            // 
            // SessionDatePicker
            // 
            tableLayoutPanel1.SetColumnSpan(SessionDatePicker, 3);
            SessionDatePicker.Dock = DockStyle.Bottom;
            SessionDatePicker.Location = new Point(532, 26);
            SessionDatePicker.Name = "SessionDatePicker";
            SessionDatePicker.Size = new Size(307, 23);
            SessionDatePicker.TabIndex = 7;
            // 
            // SessionStartTimePicker
            // 
            tableLayoutPanel1.SetColumnSpan(SessionStartTimePicker, 3);
            SessionStartTimePicker.Dock = DockStyle.Bottom;
            SessionStartTimePicker.Format = DateTimePickerFormat.Time;
            SessionStartTimePicker.Location = new Point(532, 60);
            SessionStartTimePicker.Name = "SessionStartTimePicker";
            SessionStartTimePicker.ShowUpDown = true;
            SessionStartTimePicker.Size = new Size(307, 23);
            SessionStartTimePicker.TabIndex = 8;
            // 
            // SessionEndTimePicker
            // 
            tableLayoutPanel1.SetColumnSpan(SessionEndTimePicker, 3);
            SessionEndTimePicker.Dock = DockStyle.Bottom;
            SessionEndTimePicker.Format = DateTimePickerFormat.Time;
            SessionEndTimePicker.Location = new Point(532, 97);
            SessionEndTimePicker.Name = "SessionEndTimePicker";
            SessionEndTimePicker.ShowUpDown = true;
            SessionEndTimePicker.Size = new Size(307, 23);
            SessionEndTimePicker.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label6, 7);
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(894, 0);
            label6.Name = "label6";
            label6.Size = new Size(550, 52);
            label6.TabIndex = 10;
            label6.Text = "Vælg Instruktør(er) :";
            label6.TextAlign = ContentAlignment.BottomLeft;
            // 
            // InstructorSearchFieldTextBox
            // 
            tableLayoutPanel1.SetColumnSpan(InstructorSearchFieldTextBox, 6);
            InstructorSearchFieldTextBox.Dock = DockStyle.Bottom;
            InstructorSearchFieldTextBox.Location = new Point(939, 60);
            InstructorSearchFieldTextBox.Name = "InstructorSearchFieldTextBox";
            InstructorSearchFieldTextBox.Size = new Size(505, 23);
            InstructorSearchFieldTextBox.TabIndex = 12;
            // 
            // InstructorsCheckedList
            // 
            tableLayoutPanel1.SetColumnSpan(InstructorsCheckedList, 7);
            InstructorsCheckedList.Dock = DockStyle.Fill;
            InstructorsCheckedList.FormattingEnabled = true;
            InstructorsCheckedList.Location = new Point(894, 89);
            InstructorsCheckedList.Name = "InstructorsCheckedList";
            tableLayoutPanel1.SetRowSpan(InstructorsCheckedList, 4);
            InstructorsCheckedList.Size = new Size(550, 220);
            InstructorsCheckedList.TabIndex = 13;
            // 
            // CreateSessionButton
            // 
            tableLayoutPanel1.SetColumnSpan(CreateSessionButton, 4);
            CreateSessionButton.Dock = DockStyle.Fill;
            CreateSessionButton.Location = new Point(464, 170);
            CreateSessionButton.Name = "CreateSessionButton";
            CreateSessionButton.Size = new Size(375, 87);
            CreateSessionButton.TabIndex = 14;
            CreateSessionButton.Text = "Opret Hold";
            CreateSessionButton.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Dock = DockStyle.Fill;
            label8.Location = new Point(3, 364);
            label8.Name = "label8";
            label8.Size = new Size(89, 62);
            label8.TabIndex = 15;
            label8.Text = "Alle Hold :";
            label8.TextAlign = ContentAlignment.BottomRight;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Dock = DockStyle.Fill;
            label10.Location = new Point(193, 364);
            label10.Name = "label10";
            label10.Size = new Size(89, 62);
            label10.TabIndex = 17;
            label10.Text = "Hold Type :";
            label10.TextAlign = ContentAlignment.BottomLeft;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Dock = DockStyle.Fill;
            label11.Location = new Point(464, 364);
            label11.Name = "label11";
            label11.Size = new Size(62, 62);
            label11.TabIndex = 18;
            label11.Text = "Lokale :";
            label11.TextAlign = ContentAlignment.BottomLeft;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Dock = DockStyle.Fill;
            label12.Location = new Point(728, 364);
            label12.Name = "label12";
            label12.Size = new Size(111, 62);
            label12.TabIndex = 19;
            label12.Text = "Kun Hold Efter :";
            label12.TextAlign = ContentAlignment.BottomLeft;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Dock = DockStyle.Fill;
            label13.Location = new Point(1078, 364);
            label13.Name = "label13";
            label13.Size = new Size(122, 62);
            label13.TabIndex = 20;
            label13.Text = "Kun Hold Før :";
            label13.TextAlign = ContentAlignment.BottomLeft;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(dataGridView1, 17);
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 471);
            dataGridView1.Name = "dataGridView1";
            tableLayoutPanel1.SetRowSpan(dataGridView1, 11);
            dataGridView1.Size = new Size(1441, 567);
            dataGridView1.TabIndex = 21;
            // 
            // SessionTypeSearchComboBox
            // 
            tableLayoutPanel1.SetColumnSpan(SessionTypeSearchComboBox, 3);
            SessionTypeSearchComboBox.Dock = DockStyle.Fill;
            SessionTypeSearchComboBox.FormattingEnabled = true;
            SessionTypeSearchComboBox.Location = new Point(193, 429);
            SessionTypeSearchComboBox.Name = "SessionTypeSearchComboBox";
            SessionTypeSearchComboBox.Size = new Size(265, 23);
            SessionTypeSearchComboBox.TabIndex = 22;
            // 
            // LocationSearchComboBox
            // 
            tableLayoutPanel1.SetColumnSpan(LocationSearchComboBox, 3);
            LocationSearchComboBox.Dock = DockStyle.Fill;
            LocationSearchComboBox.FormattingEnabled = true;
            LocationSearchComboBox.Location = new Point(464, 429);
            LocationSearchComboBox.Name = "LocationSearchComboBox";
            LocationSearchComboBox.Size = new Size(258, 23);
            LocationSearchComboBox.TabIndex = 23;
            // 
            // SessionStartSearchDatePicker
            // 
            tableLayoutPanel1.SetColumnSpan(SessionStartSearchDatePicker, 4);
            SessionStartSearchDatePicker.Dock = DockStyle.Fill;
            SessionStartSearchDatePicker.Location = new Point(728, 429);
            SessionStartSearchDatePicker.Name = "SessionStartSearchDatePicker";
            SessionStartSearchDatePicker.Size = new Size(344, 23);
            SessionStartSearchDatePicker.TabIndex = 24;
            // 
            // SessionEndSearchDatePicker
            // 
            tableLayoutPanel1.SetColumnSpan(SessionEndSearchDatePicker, 5);
            SessionEndSearchDatePicker.Dock = DockStyle.Fill;
            SessionEndSearchDatePicker.Location = new Point(1078, 429);
            SessionEndSearchDatePicker.Name = "SessionEndSearchDatePicker";
            SessionEndSearchDatePicker.Size = new Size(366, 23);
            SessionEndSearchDatePicker.TabIndex = 25;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 20;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.28403759F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.57981229F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.81690145F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.511737F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.161972F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.58215952F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.40610337F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.335681F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 6.7488265F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.701878F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.69483566F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.81690145F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.69718313F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.497653F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Controls.Add(label9, 1, 7);
            tableLayoutPanel1.Controls.Add(label7, 10, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(SessionTypeComboBox, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 2);
            tableLayoutPanel1.Controls.Add(LocationComboBox, 1, 2);
            tableLayoutPanel1.Controls.Add(label3, 5, 0);
            tableLayoutPanel1.Controls.Add(label4, 5, 1);
            tableLayoutPanel1.Controls.Add(label5, 5, 2);
            tableLayoutPanel1.Controls.Add(SessionDatePicker, 6, 0);
            tableLayoutPanel1.Controls.Add(SessionStartTimePicker, 6, 1);
            tableLayoutPanel1.Controls.Add(SessionEndTimePicker, 6, 2);
            tableLayoutPanel1.Controls.Add(label6, 10, 0);
            tableLayoutPanel1.Controls.Add(InstructorSearchFieldTextBox, 11, 1);
            tableLayoutPanel1.Controls.Add(InstructorsCheckedList, 10, 2);
            tableLayoutPanel1.Controls.Add(CreateSessionButton, 5, 4);
            tableLayoutPanel1.Controls.Add(label8, 0, 7);
            tableLayoutPanel1.Controls.Add(label10, 2, 7);
            tableLayoutPanel1.Controls.Add(label11, 5, 7);
            tableLayoutPanel1.Controls.Add(label12, 8, 7);
            tableLayoutPanel1.Controls.Add(label13, 12, 7);
            tableLayoutPanel1.Controls.Add(dataGridView1, 0, 9);
            tableLayoutPanel1.Controls.Add(SessionTypeSearchComboBox, 2, 8);
            tableLayoutPanel1.Controls.Add(LocationSearchComboBox, 5, 8);
            tableLayoutPanel1.Controls.Add(SessionStartSearchDatePicker, 8, 8);
            tableLayoutPanel1.Controls.Add(SessionEndSearchDatePicker, 12, 8);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 20;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.26609039F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.5542748F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4.226705F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 8.933718F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5.9558115F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 4.034582F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.Size = new Size(1904, 1041);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // SessionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "SessionForm";
            Size = new Size(1904, 1041);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label9;
        private Label label7;
        private Label label1;
        private ComboBox SessionTypeComboBox;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label2;
        private ComboBox LocationComboBox;
        private Label label3;
        private Label label4;
        private Label label5;
        private DateTimePicker SessionDatePicker;
        private DateTimePicker SessionStartTimePicker;
        private DateTimePicker SessionEndTimePicker;
        private Label label6;
        private TextBox InstructorSearchFieldTextBox;
        private CheckedListBox InstructorsCheckedList;
        private Button CreateSessionButton;
        private Label label8;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private DataGridView dataGridView1;
        private ComboBox SessionTypeSearchComboBox;
        private ComboBox LocationSearchComboBox;
        private DateTimePicker SessionStartSearchDatePicker;
        private DateTimePicker SessionEndSearchDatePicker;
    }
}