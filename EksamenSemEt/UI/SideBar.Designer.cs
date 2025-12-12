namespace EksamenSemEt.UI
{
    partial class SideBar
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
            BookingButton = new Button();
            MemberButton = new Button();
            SessionButton = new Button();
            InstructorButton = new Button();
            CertificateButton = new Button();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Controls.Add(BookingButton, 0, 0);
            tableLayoutPanel1.Controls.Add(MemberButton, 0, 1);
            tableLayoutPanel1.Controls.Add(SessionButton, 0, 2);
            tableLayoutPanel1.Controls.Add(InstructorButton, 0, 3);
            tableLayoutPanel1.Controls.Add(CertificateButton, 0, 4);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 6;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(298, 614);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // BookingButton
            // 
            BookingButton.Dock = DockStyle.Fill;
            BookingButton.Location = new Point(3, 3);
            BookingButton.Name = "BookingButton";
            BookingButton.Size = new Size(292, 55);
            BookingButton.TabIndex = 0;
            BookingButton.Text = "Booking";
            BookingButton.UseVisualStyleBackColor = true;
            BookingButton.Click += BookingButton_Click;
            // 
            // MemberButton
            // 
            MemberButton.Dock = DockStyle.Fill;
            MemberButton.Location = new Point(3, 64);
            MemberButton.Name = "MemberButton";
            MemberButton.Size = new Size(292, 55);
            MemberButton.TabIndex = 1;
            MemberButton.Text = "Medlemmer";
            MemberButton.UseVisualStyleBackColor = true;
            MemberButton.Click += MemberButton_Click;
            // 
            // SessionButton
            // 
            SessionButton.Dock = DockStyle.Fill;
            SessionButton.Location = new Point(3, 125);
            SessionButton.Name = "SessionButton";
            SessionButton.Size = new Size(292, 55);
            SessionButton.TabIndex = 2;
            SessionButton.Text = "Hold";
            SessionButton.UseVisualStyleBackColor = true;
            SessionButton.Click += SessionButton_Click;
            // 
            // InstructorButton
            // 
            InstructorButton.Dock = DockStyle.Fill;
            InstructorButton.Location = new Point(3, 186);
            InstructorButton.Name = "InstructorButton";
            InstructorButton.Size = new Size(292, 55);
            InstructorButton.TabIndex = 3;
            InstructorButton.Text = "Instruktører";
            InstructorButton.UseVisualStyleBackColor = true;
            InstructorButton.Click += InstructorButton_Click;
            // 
            // CertificateButton
            // 
            CertificateButton.Dock = DockStyle.Fill;
            CertificateButton.Location = new Point(3, 247);
            CertificateButton.Name = "CertificateButton";
            CertificateButton.Size = new Size(292, 55);
            CertificateButton.TabIndex = 4;
            CertificateButton.Text = "Certifikater";
            CertificateButton.UseVisualStyleBackColor = true;
            CertificateButton.Click += CertificateButton_Click;
            // 
            // SideBar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "SideBar";
            Size = new Size(298, 614);
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Button BookingButton;
        private Button MemberButton;
        private Button SessionButton;
        private Button InstructorButton;
        private Button CertificateButton;
    }
}
