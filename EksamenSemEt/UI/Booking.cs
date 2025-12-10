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
            valgtMedlemmer.Items.Clear();

            // Loop over globale aktive kunder og tilføjer dem til CheckedListBox
            foreach (var kunde in KundestyringStaticRepository.Kunder.Where(k => k.Aktiv))
            {
                // Tilføjer Kunde-objektet til CheckedListBox (ToString() viser Navn)
                valgtMedlemmer.Items.Add(kunde, false);
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
            if (valgtMedlemmer.CheckedItems.Count == 0)
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
            foreach (var item in valgtMedlemmer.CheckedItems)
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
            for (int i = valgtMedlemmer.Items.Count - 1; i >= 0; i--)
                valgtMedlemmer.SetItemChecked(i, false);
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
    }

}
