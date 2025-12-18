using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DatabaseAccessSem1.Repository;
using EksamenSemEt.UI;
using Sem1BackupForms;
using Sem1BackupForms.Forms;

namespace EksamenSemEt.UI
{
    public partial class SideBar : UserControl
    {
        public event EventHandler BookingClicked;
        public event EventHandler MemberClicked;
        public event EventHandler SessionClicked;
        public event EventHandler InstructorClicked;
        public event EventHandler CertificateClicked;
        public event EventHandler LocationClicked;
        public SideBar()
        {
            InitializeComponent();
        }

        private void BookingButton_Click(object sender, EventArgs e)
        {
            BookingClicked?.Invoke(this, EventArgs.Empty);
        }

        private void MemberButton_Click(object sender, EventArgs e)
        {
            MemberClicked?.Invoke(this, EventArgs.Empty);
        }

        private void SessionButton_Click(object sender, EventArgs e)
        {
            SessionClicked?.Invoke(this, EventArgs.Empty);
        }

        private void InstructorButton_Click(object sender, EventArgs e)
        {
            InstructorClicked?.Invoke(this, EventArgs.Empty);
        }

        private void CertificateButton_Click(object sender, EventArgs e)
        {
            CertificateClicked?.Invoke(this, EventArgs.Empty);
        }

        private void LocationButton_Click(object sender, EventArgs e)
        {
            LocationClicked?.Invoke(this, EventArgs.Empty);
        }

        public void ReFormatAllButtons()
        {
            var allButtons = new List<Button>
            {
                BookingButton,
                MemberButton,
                SessionButton,
                InstructorButton,
                CertificateButton,
                LocationButton
            };

            foreach (Button button in allButtons)
            {
                button.BackColor = SystemColors.Control;
            }

        }
    }
}
