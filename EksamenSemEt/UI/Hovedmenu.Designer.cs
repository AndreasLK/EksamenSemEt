
namespace FitHubUI
{
    partial class Hovedmenu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            kunder = new Button();
            instruktører = new Button();
            planlægHold = new Button();
            bookHold = new Button();
            rapporter = new Button();
            systemstatus = new Button();
            kundeLabel = new Label();
            instruktørerLabel = new Label();
            planlægningsLabel = new Label();
            bookingLabel = new Label();
            SuspendLayout();
            // 
            // kunder
            // 
            kunder.Location = new Point(12, 12);
            kunder.Name = "kunder";
            kunder.Size = new Size(86, 30);
            kunder.TabIndex = 0;
            kunder.Text = "Kunder";
            kunder.UseVisualStyleBackColor = true;
            kunder.Click += btnKunderClick;
            // 
            // instruktører
            // 
            instruktører.Location = new Point(12, 66);
            instruktører.Name = "instruktører";
            instruktører.Size = new Size(86, 30);
            instruktører.TabIndex = 1;
            instruktører.Text = "Instruktører";
            instruktører.UseVisualStyleBackColor = true;
            instruktører.Click += btnInstruktorer_Click;
            // 
            // planlægHold
            // 
            planlægHold.Location = new Point(12, 122);
            planlægHold.Name = "planlægHold";
            planlægHold.Size = new Size(86, 30);
            planlægHold.TabIndex = 2;
            planlægHold.Text = "Planlæg hold";
            planlægHold.UseVisualStyleBackColor = true;
            planlægHold.Click += btnPlanlaegHold_Click;
            // 
            // bookHold
            // 
            bookHold.Location = new Point(12, 174);
            bookHold.Name = "bookHold";
            bookHold.Size = new Size(86, 30);
            bookHold.TabIndex = 3;
            bookHold.Text = "Book hold";
            bookHold.UseVisualStyleBackColor = true;
            bookHold.Click += btnBookHold_Click;
            // 
            // rapporter
            // 
            rapporter.Location = new Point(12, 472);
            rapporter.Name = "rapporter";
            rapporter.Size = new Size(108, 30);
            rapporter.TabIndex = 4;
            rapporter.Text = "Udskriv Rapport";
            rapporter.UseVisualStyleBackColor = true;
            rapporter.Click += btnRapportUdskriv_Click;
            // 
            // systemstatus
            // 
            systemstatus.Location = new Point(126, 472);
            systemstatus.Name = "systemstatus";
            systemstatus.Size = new Size(108, 30);
            systemstatus.TabIndex = 5;
            systemstatus.Text = "Systemstatus";
            systemstatus.UseVisualStyleBackColor = true;
            systemstatus.Click += btnSystemStatus_Click;
            // 
            // kundeLabel
            // 
            kundeLabel.AutoSize = true;
            kundeLabel.Location = new Point(104, 20);
            kundeLabel.Name = "kundeLabel";
            kundeLabel.Size = new Size(153, 15);
            kundeLabel.TabIndex = 6;
            kundeLabel.Text = "Opret eller deaktiver kunder";
            // 
            // instruktørerLabel
            // 
            instruktørerLabel.AutoSize = true;
            instruktørerLabel.Location = new Point(104, 74);
            instruktørerLabel.Name = "instruktørerLabel";
            instruktørerLabel.Size = new Size(284, 15);
            instruktørerLabel.TabIndex = 7;
            instruktørerLabel.Text = "Intruktørers tidsplaner samt oprettelse af instruktører";
            // 
            // planlægningsLabel
            // 
            planlægningsLabel.AutoSize = true;
            planlægningsLabel.Location = new Point(104, 130);
            planlægningsLabel.Name = "planlægningsLabel";
            planlægningsLabel.Size = new Size(135, 15);
            planlægningsLabel.TabIndex = 8;
            planlægningsLabel.Text = "Planlæg fremtidige hold";
            // 
            // bookingLabel
            // 
            bookingLabel.AutoSize = true;
            bookingLabel.Location = new Point(104, 182);
            bookingLabel.Name = "bookingLabel";
            bookingLabel.Size = new Size(149, 15);
            bookingLabel.TabIndex = 9;
            bookingLabel.Text = "Booking af hold for kunder";
            // 
            // Hovedmenu
            // 
            ClientSize = new Size(694, 514);
            Controls.Add(bookingLabel);
            Controls.Add(planlægningsLabel);
            Controls.Add(instruktørerLabel);
            Controls.Add(kundeLabel);
            Controls.Add(systemstatus);
            Controls.Add(rapporter);
            Controls.Add(bookHold);
            Controls.Add(planlægHold);
            Controls.Add(instruktører);
            Controls.Add(kunder);
            Name = "Hovedmenu";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Button planlægHold;
        private Button kunder;
        private Button instruktører;
        private Button bookHold;
        private Button rapporter;
        private Button systemstatus;
        private Label kundeLabel;
        private Label instruktørerLabel;
        private Label planlægningsLabel;
        private Label bookingLabel;
    }
}
