using EksamenSemEt.UI;

namespace FitHubUI
{
    public partial class Hovedmenu : Form
    {
        public Hovedmenu()
        {
            InitializeComponent();
        }

        private void btnKunderClick(object sender, EventArgs e)
        {
            var kunderForm = new Kundestyring();
            kunderForm.ShowDialog();
        }

        private void btnInstruktorer_Click(object sender, EventArgs e)
        {
            var instrForm = new Instruktører();
            instrForm.ShowDialog();
        }

        private void btnPlanlaegHold_Click(object sender, EventArgs e)
        {
            var planForm = new Planlægning();
            planForm.ShowDialog();
        }

        private void btnBookHold_Click(object sender, EventArgs e)
        {
            var bookingForm = new Booking();
            bookingForm.ShowDialog();
        }

        private void btnRapportUdskriv_Click(object sender, EventArgs e)
        {

        }

        private void btnSystemStatus_Click(object sender, EventArgs e)
        {
            var statusForm = new SystemStatus();
            statusForm.ShowDialog();
        }
    }
}