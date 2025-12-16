namespace EksamenSemEt.UI
{
    partial class SessionTimeEditor
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
            SessionEndTimePicker = new DateTimePicker();
            label5 = new Label();
            label3 = new Label();
            label1 = new Label();
            SessionDatePicker = new DateTimePicker();
            SessionStartTimePicker = new DateTimePicker();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(SessionEndTimePicker, 1, 2);
            tableLayoutPanel1.Controls.Add(label5, 0, 2);
            tableLayoutPanel1.Controls.Add(label3, 0, 1);
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(SessionDatePicker, 1, 0);
            tableLayoutPanel1.Controls.Add(SessionStartTimePicker, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutPanel1.Size = new Size(408, 208);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // SessionEndTimePicker
            // 
            SessionEndTimePicker.Dock = DockStyle.Bottom;
            SessionEndTimePicker.Format = DateTimePickerFormat.Time;
            SessionEndTimePicker.Location = new Point(52, 182);
            SessionEndTimePicker.Name = "SessionEndTimePicker";
            SessionEndTimePicker.Size = new Size(353, 23);
            SessionEndTimePicker.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Dock = DockStyle.Fill;
            label5.Location = new Point(3, 138);
            label5.Name = "label5";
            label5.Size = new Size(43, 70);
            label5.TabIndex = 4;
            label5.Text = "Slut* :";
            label5.TextAlign = ContentAlignment.BottomRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 69);
            label3.Name = "label3";
            label3.Size = new Size(43, 69);
            label3.TabIndex = 2;
            label3.Text = "Start* :";
            label3.TextAlign = ContentAlignment.BottomRight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(43, 69);
            label1.TabIndex = 0;
            label1.Text = "Dato* :";
            label1.TextAlign = ContentAlignment.BottomRight;
            // 
            // SessionDatePicker
            // 
            SessionDatePicker.Dock = DockStyle.Bottom;
            SessionDatePicker.Location = new Point(52, 43);
            SessionDatePicker.Name = "SessionDatePicker";
            SessionDatePicker.Size = new Size(353, 23);
            SessionDatePicker.TabIndex = 5;
            // 
            // SessionStartTimePicker
            // 
            SessionStartTimePicker.Dock = DockStyle.Bottom;
            SessionStartTimePicker.Format = DateTimePickerFormat.Time;
            SessionStartTimePicker.Location = new Point(52, 112);
            SessionStartTimePicker.Name = "SessionStartTimePicker";
            SessionStartTimePicker.Size = new Size(353, 23);
            SessionStartTimePicker.TabIndex = 6;
            // 
            // SessionTimeEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "SessionTimeEditor";
            Size = new Size(408, 208);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label5;
        private Label label3;
        private Label label1;
        private DateTimePicker SessionDatePicker;
        private DateTimePicker SessionEndTimePicker;
        private DateTimePicker SessionStartTimePicker;
    }
}
