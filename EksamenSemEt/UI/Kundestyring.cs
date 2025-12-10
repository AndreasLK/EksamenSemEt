using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FitHubUI
{

    public class Kunde
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Email { get; set; }
        public bool Aktiv { get; set; }

        // Gør at kunden vises med navn i UI-kontroller
        public override string ToString() => Navn;
    }

    public partial class Kundestyring : Form
    {
        // Global liste deles med hele systemet
        private BindingList<Kunde> kunder = KundestyringStaticRepository.Kunder;

        public Kundestyring()
        {
            InitializeComponent(); // sætter UI op

            // Binder Load-hændelse til metode der fylder data ved opstart
            Load += Kundestyring_Load;

            // Binder grid-selection event til metode, der viser kundeinfo i tekstfelter
            listeAfKunder.SelectionChanged += listeAfKunder_SelectionChanged;
            // Binder knapper til deres respektive CRUD-metoder
            opretKunde.Click += opretKunde_Click;
            gemKunde.Click += gemKunde_Click;
            deaktiverKunde.Click += deaktiverKunde_Click;
        }

        private void Kundestyring_Load(object sender, EventArgs e)
        {
            // Binder kunde-listen til gridet
            listeAfKunder.DataSource = kunder;

            // Konfigurerer datagridvisningen
            listeAfKunder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            listeAfKunder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            listeAfKunder.MultiSelect = false;
            listeAfKunder.ReadOnly = true;
            listeAfKunder.RowHeadersVisible = false;

            // Tilføjer dummy-kunder kun første gang programmet kører
            // TODO[DB]: Fjern hardcoded dummy-data. I stedet: hent kunder fra DB
            if (kunder.Count == 0)
            {
                kunder.Add(new Kunde { Id = 1, Navn = "Peter Hansen", Email = "peter@test.dk", Aktiv = true });
                kunder.Add(new Kunde { Id = 2, Navn = "Lise Jensen", Email = "lise@test.dk", Aktiv = false });
            }
        }

        private void listeAfKunder_SelectionChanged(object sender, EventArgs e)
        {
            // Stop hvis ingen række er valgt
            if (listeAfKunder.CurrentRow == null) return;

            // Henter kundeobjektet fra gridet
            var kunde = listeAfKunder.CurrentRow.DataBoundItem as Kunde;
            if (kunde == null) return;

            // Opdaterer UI med oplysninger om den valgte kunde
            kundeNavn.Text = kunde.Navn;
            kundeEmail.Text = kunde.Email;
            kundeAktiv.Checked = kunde.Aktiv;
        }

        private void opretKunde_Click(object sender, EventArgs e)
        {
            // Opretter nyt kundeobjekt baseret på værdier i tekstfelter
            var nyKunde = new Kunde
            {
                Id = kunder.Count + 1,
                Navn = kundeNavn.Text,
                Email = kundeEmail.Text,
                Aktiv = kundeAktiv.Checked
            };

            // TODO[DB-ERSTAT]: Erstat denne lokale Add med DB-oprettelse via repository.
            kunder.Add(nyKunde);
        }

        private void gemKunde_Click(object sender, EventArgs e)
        {
            // Stop hvis ingen række er valgt
            if (listeAfKunder.CurrentRow == null) return;

            // Finder det valgte kundeobjekt
            var kunde = listeAfKunder.CurrentRow.DataBoundItem as Kunde;
            if (kunde == null) return;

            // Opdaterer kundens data med de nye værdier
            kunde.Navn = kundeNavn.Text;
            kunde.Email = kundeEmail.Text;
            kunde.Aktiv = kundeAktiv.Checked;

            // TODO[DB-ERSTAT]: Erstat denne lokale opdatering/Refresh med et Update-kald til repository.
            // Tvinger opdatering af gridvisning
            listeAfKunder.Refresh();
        }

        private void deaktiverKunde_Click(object sender, EventArgs e)
        {
            // Stop hvis ingen række er valgt
            if (listeAfKunder.CurrentRow == null) return;

            // Henter den valgte kunde
            var kunde = listeAfKunder.CurrentRow.DataBoundItem as Kunde;

            // Viser bekræftelsesdialog
            var confirm = MessageBox.Show(
                "Er du sikker på at du vil deaktivere kunden?",
                "Bekræft",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            // Hvis JA → fjern kunden fra systemet
            if (confirm == DialogResult.Yes)
            {
                // TODO[DB-ERSTAT]: Erstat denne fjernelse fra lokal liste med slet/update i DB.
                kunder.Remove(kunde); // fjerner kunden fra global liste
            }
        }
    }
}