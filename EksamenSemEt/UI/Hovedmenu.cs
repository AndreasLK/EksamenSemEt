using EksamenSemEt.UI;

namespace FitHubUI
{
    public partial class Hovedmenu : Form
    {
        public Hovedmenu()
        {
            InitializeComponent();
        }

        // Åbner kundestyringsvinduet som modal dialog
        private void btnKunderClick(object sender, EventArgs e)
        {
            var kunderForm = new Kundestyring();
            kunderForm.ShowDialog();
        }

        // Åbner instruktøradministrationen, hvor instruktører oprettes og redigeres
        private void btnInstruktorer_Click(object sender, EventArgs e)
        {
            var instrForm = new Instruktører();
            instrForm.ShowDialog();
        }

        // Åbner holdplanlægning, hvor hold oprettes og administreres
        private void btnPlanlaegHold_Click(object sender, EventArgs e)
        {
            var planForm = new Planlægning();
            planForm.ShowDialog();
        }

        // Åbner bookingvinduet, hvor medlemmer tilmeldes hold
        private void btnBookHold_Click(object sender, EventArgs e)
        {
            var bookingForm = new Booking();
            bookingForm.ShowDialog();
        }

        // Kald til rapportgenerering
        private void btnRapportUdskriv_Click(object sender, EventArgs e)
        {
            // her indsættes kode til udskrivning af rapport
        }

        // Kald til systemstatus-check
        private void btnSystemStatus_Click(object sender, EventArgs e)
        {
            // her indsættes kode der checker efter database connection
        }
    }
}