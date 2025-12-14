using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FitHubUI
{
    public partial class Booking : Form
    {
        private BindingSource holdSource = new();
        private BindingSource tilmeldteSource = new();

        public Booking()
        {
            // Initialiserer WinForms-kontrollerne i designeren
            InitializeComponent();

            // Opsætter DataGridView-indstillinger (kolonner, selection etc.)
            SetupGrids();

            // Loader aktive kunder ind i CheckedListBox
            LoadKunder();

            // Loader hold fra HoldRepository ind i grid
            LoadHold();

            // Bind event: kald kommendeHold_SelectionChanged når valgt række ændres
            kommendeHold.SelectionChanged += kommendeHold_SelectionChanged;
            // Bind event: kald bookTilHold_Click når Book-knappen klikkes
            bookTilHold.Click += bookTilHold_Click;
            // Bind event: kald afmeldMedlem_Click når Afmeld-knappen klikkes
            afmeldMedlem.Click += afmeldMedlem_Click;
        }

        private void SetupGrids()
        {
            // Sætter grid så kolonner fylder pladsen
            kommendeHold.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Sætter selection til at vælge hele rækker
            kommendeHold.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // Forhindrer multi-select
            kommendeHold.MultiSelect = false;
            // Gør grid readonly
            kommendeHold.ReadOnly = true;
            // Skjuler row headers
            kommendeHold.RowHeadersVisible = false;

            // Sætter samme egenskaber for tilmeldteMedlemmer-grid
            tilmeldteMedlemmer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tilmeldteMedlemmer.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tilmeldteMedlemmer.MultiSelect = false;
            tilmeldteMedlemmer.ReadOnly = true;
            tilmeldteMedlemmer.RowHeadersVisible = false;
        }

        private void LoadKunder()
        {
            // Tømmer CheckedListBox før genfyldning
            valgtMedlem.Items.Clear();

            // Loop over globale aktive kunder og tilføjer dem til CheckedListBox
            foreach (var kunde in KundestyringStaticRepository.Kunder.Where(k => k.Aktiv))
            {
                // Tilføjer Kunde-objektet til CheckedListBox (ToString() viser Navn)
                valgtMedlem.Items.Add(kunde, false);
            }
        }

        private void LoadHold()
        {
            // Henter hold fra HoldRepository, projicerer felter til visning og binder til holdSource
            holdSource.DataSource = HoldRepository.HoldListe.Select(h => new
            {
                h.HoldId,
                h.HoldType,
                Dato = h.DatoTid.ToString("g"), // formaterer dato/tid til kort streng
                Instruktør = h.Instruktør?.Navn,
                h.MaxAntal
            }).ToList();

            // Binder holdSource til DataGridView så hold vises i UI
            kommendeHold.DataSource = holdSource;
        }

        private void kommendeHold_SelectionChanged(object sender, EventArgs e)
        {
            // Hvis ingen række er valgt, gør intet
            if (kommendeHold.SelectedRows.Count == 0) return;

            // Læser HoldId fra den valgte række i grid
            int holdId = Convert.ToInt32(kommendeHold.SelectedRows[0].Cells["HoldId"].Value);

            // Henter tilmeldte medlemmer for holdet og projicerer til visningsobjekt
            var tilmeldte = BookingRepository.HentTilmeldte(holdId)
                .Select(m => new { m.Id, m.Navn, m.Email })
                .ToList();

            // Binder den projicerede liste til tilmeldteMedlemmer-grid
            tilmeldteMedlemmer.DataSource = tilmeldte;
        }

        private void bookTilHold_Click(object sender, EventArgs e)
        {
            // Valider: tjek at et hold er valgt i grid
            if (kommendeHold.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vælg et hold.");
                return;
            }

            // Valider: tjek at mindst ét medlem er valgt i CheckedListBox
            if (valgtMedlem.CheckedItems.Count == 0)
            {
                MessageBox.Show("Vælg mindst ét medlem.");
                return;
            }

            // Læs HoldId fra valgt række
            int holdId = Convert.ToInt32(kommendeHold.SelectedRows[0].Cells["HoldId"].Value);
            // Hent Hold-objekt fra repository
            var hold = HoldRepository.HoldListe.First(h => h.HoldId == holdId);
            // TODO[DB-ERSTAT]: Erstat BookingRepository.HentTilmeldte med DB-opslag: await bookingRepository.GetAttendeesAsync(holdId)
            // Hent nuværende tilmeldte for holdet
            var tilmeldte = BookingRepository.HentTilmeldte(holdId);

            // Opret lister til at samle resultater
            var succesList = new List<string>();
            var fejlList = new List<string>();

            // Loop: prøv at booke hver checked kunde
            foreach (var item in valgtMedlem.CheckedItems)
            {
                // Cast det valgte item til Kunde
                var medlem = (Kunde)item;

                // TODO[DB-ERSTAT]: I stedet for kun at tjekke den lokale liste, brug et transaction-isoleret DB-check/insert
                // Tjek om medlem allerede er tilmeldt (dobbeltbooking)
                if (tilmeldte.Any(k => k.Id == medlem.Id))
                {
                    // Tilføj fejlbesked for dette medlem
                    fejlList.Add($"{medlem.Navn} (allerede tilmeldt)");
                    continue; // spring til næste medlem
                }

                // Tjek kapacitet: hvis fuldt, læg fejlbesked
                if (tilmeldte.Count >= hold.MaxAntal)
                {
                    fejlList.Add($"{medlem.Navn} (holdet er fyldt)");
                    continue; // spring til næste medlem
                }

                // TODO[DB-ERSTAT]: Erstat dette med bookingRepository.AddBookingAsync(holdId, medlem.Id) inde i en transaktion
                // Book medlem via repository (gemmer globalt)
                BookingRepository.BookMedlem(holdId, medlem);
                // Opdater lokal kopi så kapacitets-check virker for efterfølgende medlemmer
                tilmeldte.Add(medlem);
                // Registrer succes
                succesList.Add(medlem.Navn);
            }

            // Opdater visning ved at genkalde selection-changed logik
            kommendeHold_SelectionChanged(null, null);

            // Byg resultatbesked til brugeren
            var msg = "";
            if (succesList.Any())
                msg += $"Tilmeldt: {string.Join(", ", succesList)}\n";
            if (fejlList.Any())
                msg += $"Kunne ikke tilmelde: {string.Join(", ", fejlList)}";

            // Vis resultat-oplysninger
            MessageBox.Show(msg, "Booking resultat");

            // Loop: fjern alle checks i CheckedListBox efter booking (ryd op i UI)
            for (int i = valgtMedlem.Items.Count - 1; i >= 0; i--)
                valgtMedlem.SetItemChecked(i, false);
        }

        private void afmeldMedlem_Click(object sender, EventArgs e)
        {
            // Valider: tjek at et hold er valgt
            if (kommendeHold.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vælg et hold først.");
                return;
            }

            // Valider: tjek at et medlem er valgt i tilmeldte-grid
            if (tilmeldteMedlemmer.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vælg et medlem der skal afmeldes.");
                return;
            }

            // Læs HoldId fra valgt række
            int holdId = Convert.ToInt32(kommendeHold.SelectedRows[0].Cells["HoldId"].Value);

            // Læs medlemId fra valgt række i tilmeldte-grid
            int medlemId = Convert.ToInt32(tilmeldteMedlemmer.SelectedRows[0].Cells["Id"].Value);

            // Find Kunde-objekt i den globale kundeliste
            var medlem = KundestyringStaticRepository.Kunder.First(k => k.Id == medlemId);

            // TODO[DB-ERSTAT]: Erstat BookingRepository.AfmeldMedlem med bookingRepository.RemoveBookingAsync(holdId, medlemId)
            // Fjern medlem fra booking via repository
            BookingRepository.AfmeldMedlem(holdId, medlem);

            // Bekræft afmelding til bruger
            MessageBox.Show($"{medlem.Navn} er afmeldt holdet.");

            // Opdater visningen efter afmelding
            kommendeHold_SelectionChanged(null, null);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Booking_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void Tilbage_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void Lokation_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void checkedListBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged_1(object sender, EventArgs e)
        {

        }

        private List<Medlem> AlleMedlemmer;

        public Booking()
        {
            InitializeComponent();
            IndlaesMedlemmer();
        }

        private void IndlaesMedlemmer()
        {
            // Antages at du har et repository til at hente data
            MedlemRepository repo = new MedlemRepository();
            AlleMedlemmer = repo.HentAlleMedlemmer();

            // Renser ListBoxen og fylder den med medlemmer
            medlemsListBox.Items.Clear();

            // Ved at bruge AddRange med ToString() overskrevet, vises FuldeNavn
            medlemsListBox.Items.AddRange(AlleMedlemmer.ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {

        }

        private void Instruktør_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}
