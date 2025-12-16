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
            AvailableSpacesSearchUpDown = new NumericUpDown();
            label17 = new Label();
            MaxMembersSearchUpDown = new NumericUpDown();
            MinMembersSearchUpDown = new NumericUpDown();
            label16 = new Label();
            label15 = new Label();
            maxMembersNumericUpDown = new NumericUpDown();
            label14 = new Label();
            SessionEndSearchDatePicker = new DateTimePicker();
            SessionStartSearchDatePicker = new DateTimePicker();
            LocationSearchComboBox = new ComboBox();
            SessionTypeSearchComboBox = new ComboBox();
            SessionListView = new DataGridView();
            label13 = new Label();
            label12 = new Label();
            label11 = new Label();
            label10 = new Label();
            label8 = new Label();
            CreateSessionButton = new Button();
            InstructorsCheckedList = new CheckedListBox();
            InstructorSearchFieldTextBox = new TextBox();
            label6 = new Label();
            LocationComboBox = new ComboBox();
            label2 = new Label();
            SessionTypeComboBox = new ComboBox();
            label1 = new Label();
            label7 = new Label();
            label9 = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            sessionTimeEditor1 = new SessionTimeEditor();
            ((System.ComponentModel.ISupportInitialize)AvailableSpacesSearchUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxMembersSearchUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinMembersSearchUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)maxMembersNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SessionListView).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // AvailableSpacesSearchUpDown
            // 
            tableLayoutPanel1.SetColumnSpan(AvailableSpacesSearchUpDown, 2);
            AvailableSpacesSearchUpDown.Dock = DockStyle.Fill;
            AvailableSpacesSearchUpDown.Location = new Point(1632, 429);
            AvailableSpacesSearchUpDown.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            AvailableSpacesSearchUpDown.Name = "AvailableSpacesSearchUpDown";
            AvailableSpacesSearchUpDown.Size = new Size(269, 23);
            AvailableSpacesSearchUpDown.TabIndex = 33;
            // 
            // label17
            // 
            label17.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label17, 2);
            label17.Dock = DockStyle.Fill;
            label17.Location = new Point(1632, 364);
            label17.Name = "label17";
            label17.Size = new Size(269, 62);
            label17.TabIndex = 32;
            label17.Text = "Mere End XX Ledige Pladser :";
            label17.TextAlign = ContentAlignment.BottomLeft;
            // 
            // MaxMembersSearchUpDown
            // 
            MaxMembersSearchUpDown.Dock = DockStyle.Fill;
            MaxMembersSearchUpDown.Location = new Point(1482, 429);
            MaxMembersSearchUpDown.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            MaxMembersSearchUpDown.Name = "MaxMembersSearchUpDown";
            MaxMembersSearchUpDown.Size = new Size(144, 23);
            MaxMembersSearchUpDown.TabIndex = 31;
            // 
            // MinMembersSearchUpDown
            // 
            MinMembersSearchUpDown.Dock = DockStyle.Fill;
            MinMembersSearchUpDown.Location = new Point(1332, 429);
            MinMembersSearchUpDown.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            MinMembersSearchUpDown.Name = "MinMembersSearchUpDown";
            MinMembersSearchUpDown.Size = new Size(144, 23);
            MinMembersSearchUpDown.TabIndex = 30;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Dock = DockStyle.Fill;
            label16.Location = new Point(1482, 364);
            label16.Name = "label16";
            label16.Size = new Size(144, 62);
            label16.TabIndex = 29;
            label16.Text = "Max. Medlemmer :";
            label16.TextAlign = ContentAlignment.BottomLeft;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Dock = DockStyle.Fill;
            label15.Location = new Point(1332, 364);
            label15.Name = "label15";
            label15.Size = new Size(144, 62);
            label15.TabIndex = 28;
            label15.Text = "Min. Medlemmer :";
            label15.TextAlign = ContentAlignment.BottomLeft;
            // 
            // maxMembersNumericUpDown
            // 
            tableLayoutPanel1.SetColumnSpan(maxMembersNumericUpDown, 3);
            maxMembersNumericUpDown.Dock = DockStyle.Fill;
            maxMembersNumericUpDown.Location = new Point(115, 126);
            maxMembersNumericUpDown.Maximum = new decimal(new int[] { 99999, 0, 0, 0 });
            maxMembersNumericUpDown.Name = "maxMembersNumericUpDown";
            maxMembersNumericUpDown.Size = new Size(262, 23);
            maxMembersNumericUpDown.TabIndex = 27;
            maxMembersNumericUpDown.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Dock = DockStyle.Fill;
            label14.Location = new Point(3, 123);
            label14.Name = "label14";
            label14.Size = new Size(106, 36);
            label14.TabIndex = 26;
            label14.Text = "Max Medlemmer :";
            label14.TextAlign = ContentAlignment.TopRight;
            // 
            // SessionEndSearchDatePicker
            // 
            tableLayoutPanel1.SetColumnSpan(SessionEndSearchDatePicker, 4);
            SessionEndSearchDatePicker.Dock = DockStyle.Fill;
            SessionEndSearchDatePicker.Location = new Point(997, 429);
            SessionEndSearchDatePicker.Name = "SessionEndSearchDatePicker";
            SessionEndSearchDatePicker.Size = new Size(329, 23);
            SessionEndSearchDatePicker.TabIndex = 25;
            // 
            // SessionStartSearchDatePicker
            // 
            tableLayoutPanel1.SetColumnSpan(SessionStartSearchDatePicker, 4);
            SessionStartSearchDatePicker.Dock = DockStyle.Fill;
            SessionStartSearchDatePicker.Location = new Point(657, 429);
            SessionStartSearchDatePicker.Name = "SessionStartSearchDatePicker";
            SessionStartSearchDatePicker.Size = new Size(334, 23);
            SessionStartSearchDatePicker.TabIndex = 24;
            // 
            // LocationSearchComboBox
            // 
            tableLayoutPanel1.SetColumnSpan(LocationSearchComboBox, 3);
            LocationSearchComboBox.Dock = DockStyle.Fill;
            LocationSearchComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            LocationSearchComboBox.FormattingEnabled = true;
            LocationSearchComboBox.Location = new Point(464, 429);
            LocationSearchComboBox.MaxDropDownItems = 100;
            LocationSearchComboBox.Name = "LocationSearchComboBox";
            LocationSearchComboBox.Size = new Size(187, 23);
            LocationSearchComboBox.Sorted = true;
            LocationSearchComboBox.TabIndex = 23;
            // 
            // SessionTypeSearchComboBox
            // 
            tableLayoutPanel1.SetColumnSpan(SessionTypeSearchComboBox, 3);
            SessionTypeSearchComboBox.Dock = DockStyle.Fill;
            SessionTypeSearchComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SessionTypeSearchComboBox.FormattingEnabled = true;
            SessionTypeSearchComboBox.Location = new Point(193, 429);
            SessionTypeSearchComboBox.MaxDropDownItems = 100;
            SessionTypeSearchComboBox.Name = "SessionTypeSearchComboBox";
            SessionTypeSearchComboBox.Size = new Size(265, 23);
            SessionTypeSearchComboBox.Sorted = true;
            SessionTypeSearchComboBox.TabIndex = 22;
            // 
            // SessionListView
            // 
            SessionListView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(SessionListView, 17);
            SessionListView.Dock = DockStyle.Fill;
            SessionListView.Location = new Point(3, 471);
            SessionListView.Name = "SessionListView";
            tableLayoutPanel1.SetRowSpan(SessionListView, 11);
            SessionListView.Size = new Size(1473, 567);
            SessionListView.TabIndex = 21;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Dock = DockStyle.Fill;
            label13.Location = new Point(997, 364);
            label13.Name = "label13";
            label13.Size = new Size(155, 62);
            label13.TabIndex = 20;
            label13.Text = "Kun Hold Før :";
            label13.TextAlign = ContentAlignment.BottomLeft;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Dock = DockStyle.Fill;
            label12.Location = new Point(657, 364);
            label12.Name = "label12";
            label12.Size = new Size(134, 62);
            label12.TabIndex = 19;
            label12.Text = "Kun Hold Efter :";
            label12.TextAlign = ContentAlignment.BottomLeft;
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
            // label8
            // 
            label8.AutoSize = true;
            label8.Dock = DockStyle.Fill;
            label8.Location = new Point(3, 364);
            label8.Name = "label8";
            label8.Size = new Size(106, 62);
            label8.TabIndex = 15;
            label8.Text = "Alle Hold :";
            label8.TextAlign = ContentAlignment.BottomRight;
            // 
            // CreateSessionButton
            // 
            tableLayoutPanel1.SetColumnSpan(CreateSessionButton, 4);
            CreateSessionButton.Dock = DockStyle.Fill;
            CreateSessionButton.Location = new Point(464, 162);
            CreateSessionButton.Name = "CreateSessionButton";
            CreateSessionButton.Size = new Size(327, 95);
            CreateSessionButton.TabIndex = 14;
            CreateSessionButton.Text = "Opret Hold";
            CreateSessionButton.UseVisualStyleBackColor = true;
            CreateSessionButton.Click += CreateSessionButton_Click;
            // 
            // InstructorsCheckedList
            // 
            tableLayoutPanel1.SetColumnSpan(InstructorsCheckedList, 7);
            InstructorsCheckedList.Dock = DockStyle.Fill;
            InstructorsCheckedList.FormattingEnabled = true;
            InstructorsCheckedList.Location = new Point(846, 89);
            InstructorsCheckedList.Name = "InstructorsCheckedList";
            tableLayoutPanel1.SetRowSpan(InstructorsCheckedList, 4);
            InstructorsCheckedList.Size = new Size(630, 220);
            InstructorsCheckedList.TabIndex = 13;
            InstructorsCheckedList.ItemCheck += InstructorsCheckedList_ItemCheck;
            // 
            // InstructorSearchFieldTextBox
            // 
            tableLayoutPanel1.SetColumnSpan(InstructorSearchFieldTextBox, 6);
            InstructorSearchFieldTextBox.Dock = DockStyle.Bottom;
            InstructorSearchFieldTextBox.Location = new Point(891, 60);
            InstructorSearchFieldTextBox.Name = "InstructorSearchFieldTextBox";
            InstructorSearchFieldTextBox.Size = new Size(585, 23);
            InstructorSearchFieldTextBox.TabIndex = 0;
            InstructorSearchFieldTextBox.TextChanged += InstructorSearchFieldTextBox_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            tableLayoutPanel1.SetColumnSpan(label6, 7);
            label6.Dock = DockStyle.Fill;
            label6.Location = new Point(846, 0);
            label6.Name = "label6";
            label6.Size = new Size(630, 52);
            label6.TabIndex = 10;
            label6.Text = "Vælg Instruktør(er) :";
            label6.TextAlign = ContentAlignment.BottomLeft;
            // 
            // LocationComboBox
            // 
            tableLayoutPanel1.SetColumnSpan(LocationComboBox, 3);
            LocationComboBox.Dock = DockStyle.Top;
            LocationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            LocationComboBox.FormattingEnabled = true;
            LocationComboBox.Location = new Point(115, 89);
            LocationComboBox.MaxDropDownItems = 100;
            LocationComboBox.Name = "LocationComboBox";
            LocationComboBox.Size = new Size(262, 23);
            LocationComboBox.Sorted = true;
            LocationComboBox.TabIndex = 10;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 86);
            label2.Name = "label2";
            label2.Size = new Size(106, 37);
            label2.TabIndex = 1;
            label2.Text = "Lokale :";
            label2.TextAlign = ContentAlignment.TopRight;
            // 
            // SessionTypeComboBox
            // 
            tableLayoutPanel1.SetColumnSpan(SessionTypeComboBox, 3);
            SessionTypeComboBox.Dock = DockStyle.Bottom;
            SessionTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            SessionTypeComboBox.FormattingEnabled = true;
            SessionTypeComboBox.Location = new Point(115, 26);
            SessionTypeComboBox.MaxDropDownItems = 100;
            SessionTypeComboBox.Name = "SessionTypeComboBox";
            SessionTypeComboBox.Size = new Size(262, 23);
            SessionTypeComboBox.Sorted = true;
            SessionTypeComboBox.TabIndex = 10;
            SessionTypeComboBox.SelectedIndexChanged += SessionTypeComboBox_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(106, 52);
            label1.TabIndex = 0;
            label1.Text = "Hold Type* :";
            label1.TextAlign = ContentAlignment.BottomRight;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Dock = DockStyle.Fill;
            label7.Location = new Point(846, 52);
            label7.Name = "label7";
            label7.Size = new Size(39, 34);
            label7.TabIndex = 11;
            label7.Text = "Søg:";
            label7.TextAlign = ContentAlignment.BottomCenter;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Dock = DockStyle.Fill;
            label9.Location = new Point(115, 364);
            label9.Name = "label9";
            label9.Size = new Size(72, 62);
            label9.TabIndex = 16;
            label9.Text = "Filtrer Efter: ";
            label9.TextAlign = ContentAlignment.BottomRight;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 20;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.882353F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.09663868F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.28403759F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.57981229F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.81690145F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.7815125F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.352941F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.58215952F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.40610337F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5.567227F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8.455882F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 1.701878F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.9936974F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.464286F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.87815142F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 7.87815142F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.31092429F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 11.8172274F));
            tableLayoutPanel1.Controls.Add(label9, 1, 7);
            tableLayoutPanel1.Controls.Add(label7, 10, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(SessionTypeComboBox, 1, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 2);
            tableLayoutPanel1.Controls.Add(LocationComboBox, 1, 2);
            tableLayoutPanel1.Controls.Add(label6, 10, 0);
            tableLayoutPanel1.Controls.Add(InstructorSearchFieldTextBox, 11, 1);
            tableLayoutPanel1.Controls.Add(InstructorsCheckedList, 10, 2);
            tableLayoutPanel1.Controls.Add(CreateSessionButton, 5, 4);
            tableLayoutPanel1.Controls.Add(label8, 0, 7);
            tableLayoutPanel1.Controls.Add(label10, 2, 7);
            tableLayoutPanel1.Controls.Add(label11, 5, 7);
            tableLayoutPanel1.Controls.Add(label12, 8, 7);
            tableLayoutPanel1.Controls.Add(label13, 12, 7);
            tableLayoutPanel1.Controls.Add(SessionListView, 0, 9);
            tableLayoutPanel1.Controls.Add(SessionTypeSearchComboBox, 2, 8);
            tableLayoutPanel1.Controls.Add(LocationSearchComboBox, 5, 8);
            tableLayoutPanel1.Controls.Add(SessionStartSearchDatePicker, 8, 8);
            tableLayoutPanel1.Controls.Add(SessionEndSearchDatePicker, 12, 8);
            tableLayoutPanel1.Controls.Add(label14, 0, 3);
            tableLayoutPanel1.Controls.Add(maxMembersNumericUpDown, 1, 3);
            tableLayoutPanel1.Controls.Add(label15, 16, 7);
            tableLayoutPanel1.Controls.Add(label16, 17, 7);
            tableLayoutPanel1.Controls.Add(MinMembersSearchUpDown, 16, 8);
            tableLayoutPanel1.Controls.Add(MaxMembersSearchUpDown, 17, 8);
            tableLayoutPanel1.Controls.Add(label17, 18, 7);
            tableLayoutPanel1.Controls.Add(AvailableSpacesSearchUpDown, 18, 8);
            tableLayoutPanel1.Controls.Add(sessionTimeEditor1, 5, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 20;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.26609039F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.5542748F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 3.45821333F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 9.702209F));
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
            // sessionTimeEditor1
            // 
            tableLayoutPanel1.SetColumnSpan(sessionTimeEditor1, 4);
            sessionTimeEditor1.Location = new Point(464, 3);
            sessionTimeEditor1.Name = "sessionTimeEditor1";
            tableLayoutPanel1.SetRowSpan(sessionTimeEditor1, 4);
            sessionTimeEditor1.Size = new Size(327, 153);
            sessionTimeEditor1.TabIndex = 34;
            // 
            // SessionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "SessionForm";
            Size = new Size(1904, 1041);
            ((System.ComponentModel.ISupportInitialize)AvailableSpacesSearchUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxMembersSearchUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinMembersSearchUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)maxMembersNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)SessionListView).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private NumericUpDown AvailableSpacesSearchUpDown;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label9;
        private Label label7;
        private Label label1;
        private ComboBox SessionTypeComboBox;
        private Label label2;
        private ComboBox LocationComboBox;
        private Label label6;
        private TextBox InstructorSearchFieldTextBox;
        private CheckedListBox InstructorsCheckedList;
        private Button CreateSessionButton;
        private Label label8;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private DataGridView SessionListView;
        private ComboBox SessionTypeSearchComboBox;
        private ComboBox LocationSearchComboBox;
        private DateTimePicker SessionStartSearchDatePicker;
        private DateTimePicker SessionEndSearchDatePicker;
        private Label label14;
        private NumericUpDown maxMembersNumericUpDown;
        private Label label15;
        private Label label16;
        private NumericUpDown MinMembersSearchUpDown;
        private NumericUpDown MaxMembersSearchUpDown;
        private Label label17;
        private SessionTimeEditor sessionTimeEditor1;
    }
}