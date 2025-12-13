namespace EksamenSemEt.UI
{
    partial class CertificateForm
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
            CertificationListView = new DataGridView();
            SearchFieldTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            CertificateNameTextBox = new TextBox();
            CertificateCreateButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CertificationListView).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 10;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 2.9624753F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 9.019092F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.906517F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10.0065832F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.Controls.Add(CertificationListView, 2, 4);
            tableLayoutPanel1.Controls.Add(SearchFieldTextBox, 3, 3);
            tableLayoutPanel1.Controls.Add(label1, 2, 3);
            tableLayoutPanel1.Controls.Add(label2, 3, 1);
            tableLayoutPanel1.Controls.Add(CertificateNameTextBox, 4, 1);
            tableLayoutPanel1.Controls.Add(CertificateCreateButton, 4, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 10;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(1519, 800);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // CertificationListView
            // 
            CertificationListView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            tableLayoutPanel1.SetColumnSpan(CertificationListView, 3);
            CertificationListView.Dock = DockStyle.Fill;
            CertificationListView.Location = new Point(307, 323);
            CertificationListView.Name = "CertificationListView";
            tableLayoutPanel1.SetRowSpan(CertificationListView, 6);
            CertificationListView.Size = new Size(448, 474);
            CertificationListView.TabIndex = 0;
            CertificationListView.CellValueChanged += CertificationListView_CellValueChanged;
            CertificationListView.UserDeletingRow += CertificationListView_UserDeletingRow;
            // 
            // SearchFieldTextBox
            // 
            SearchFieldTextBox.BorderStyle = BorderStyle.FixedSingle;
            tableLayoutPanel1.SetColumnSpan(SearchFieldTextBox, 2);
            SearchFieldTextBox.Dock = DockStyle.Bottom;
            SearchFieldTextBox.Location = new Point(352, 294);
            SearchFieldTextBox.Name = "SearchFieldTextBox";
            SearchFieldTextBox.Size = new Size(403, 23);
            SearchFieldTextBox.TabIndex = 1;
            SearchFieldTextBox.TextChanged += SearchFieldTextBox_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Bottom;
            label1.Location = new Point(307, 305);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 2;
            label1.Text = "Søg :";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Bottom;
            label2.Location = new Point(352, 145);
            label2.Name = "label2";
            label2.Size = new Size(131, 15);
            label2.TabIndex = 3;
            label2.Text = "Nyt Certifikat Navn* :";
            // 
            // CertificateNameTextBox
            // 
            CertificateNameTextBox.Dock = DockStyle.Bottom;
            CertificateNameTextBox.Location = new Point(489, 134);
            CertificateNameTextBox.Name = "CertificateNameTextBox";
            CertificateNameTextBox.Size = new Size(266, 23);
            CertificateNameTextBox.TabIndex = 4;
            // 
            // CertificateCreateButton
            // 
            CertificateCreateButton.Dock = DockStyle.Fill;
            CertificateCreateButton.Location = new Point(489, 180);
            CertificateCreateButton.Margin = new Padding(3, 20, 3, 3);
            CertificateCreateButton.Name = "CertificateCreateButton";
            CertificateCreateButton.Size = new Size(266, 57);
            CertificateCreateButton.TabIndex = 5;
            CertificateCreateButton.Text = "Tilføj Certifikat";
            CertificateCreateButton.UseVisualStyleBackColor = true;
            CertificateCreateButton.Click += CertificateCreateButton_Click;
            // 
            // CertificateForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "CertificateForm";
            Size = new Size(1519, 800);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CertificationListView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private DataGridView CertificationListView;
        private TextBox SearchFieldTextBox;
        private Label label1;
        private Label label2;
        private TextBox CertificateNameTextBox;
        private Button CertificateCreateButton;
    }
}