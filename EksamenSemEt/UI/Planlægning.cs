using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace FitHubUI
{
    public partial class Planlægning : Form
    {
        // Holdmodel
        public class Hold
        {
            public int HoldId { get; set; }
            public string HoldType { get; set; }
            public DateTime DatoTid { get; set; }
            public Instruktører.Instruktør Instruktør { get; set; }
            public int MaxAntal { get; set; }
        }

        public Planlægning()
        {
            // Initialiserer designer-genererede kontroller
            InitializeComponent();

            // Konfigurerer datagrid (kolonner, valg mm.)
            SetupGrid();

            // Fylder holdtyper og instruktører i comboboxes
            LoadInstruktørerOgHoldtyper();

            // Viser eksisterende hold i grid
            UpdateDataGrid();
        }

        private void SetupGrid()
        {
            // Slår automatic column-generation fra (vi tilføjer kolonner manuelt)
            kommendeHold.AutoGenerateColumns = false;
            // Sætter selectionmode til at vælge hele rækker
            kommendeHold.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            // Forhindrer valg af flere rækker
            kommendeHold.MultiSelect = false;
            // Forhindrer at brugeren kan indsætte nye tomme rækker i gridet
            kommendeHold.AllowUserToAddRows = false;

            // Hvis der endnu ikke er kolonner, opret dem
            if (kommendeHold.Columns.Count == 0)
            {
                // Tilføjer kolonner: ID, Type, Dato/Tid, Instruktør, Max antal
                kommendeHold.Columns.Add("HoldId", "ID");
                kommendeHold.Columns.Add("HoldType", "Type");
                kommendeHold.Columns.Add("DatoTid", "Dato / Tid");
                kommendeHold.Columns.Add("Instruktør", "Instruktør");
                kommendeHold.Columns.Add("MaxAntal", "Max antal");
            }
        }

        private void LoadInstruktørerOgHoldtyper()
        {
            // Ryd eksisterende holdtyper og tilføj faste værdier
            holdType.Items.Clear();
            holdType.Items.AddRange(new[] { "SkovYoga", "Trailrunning", "Styrketræning", "Cardio" });
            // Sæt defaultvalg til første element
            holdType.SelectedIndex = 0;

            // Filtrer og fyld instruktør-listen baseret på valgt holdtype
            FilterInstruktørerEfterHoldtype();

            // Sæt standard startdato og antal
            startDato.Value = DateTime.Now;
            maxAntalPåHold.Value = 10;
        }

        private void holdType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Når holdtype ændres i combobox, opdater instruktørliste
            FilterInstruktørerEfterHoldtype();
        }

        private void FilterInstruktørerEfterHoldtype()
        {
            // Ryd instruktør-combobox før refill
            Instruktør.Items.Clear();

            // Læs valgt holdtype fra combobox
            string valgtType = holdType.SelectedItem?.ToString();
            // Hent alle instruktører fra repository (kald til repository)
            var alle = Instruktører.InstruktørRepository.HentAlleInstruktører();
            List<Instruktører.Instruktør> filtreret = new();

            // Filtrer instruktører efter certificering ved hjælp af LINQ
            switch (valgtType)
            {
                case "SkovYoga":
                    // Vælg kun instruktører med SkovYoga-certifikat (LINQ Where + ToList)
                    filtreret = alle.Where(i => i.CertifikatSkovYoga).ToList();
                    break;
                case "Trailrunning":
                    // Vælg kun instruktører med Trailrunning-certifikat (LINQ Where + ToList)
                    filtreret = alle.Where(i => i.CertifikatTrailrunning).ToList();
                    break;
                default:
                    // For øvrige holdtyper: vis alle instruktører
                    filtreret = alle.ToList();
                    break;
            }

            // Loop: tilføj hver filtreret instruktør til combobox
            foreach (var inst in filtreret)
                Instruktør.Items.Add(inst); // kald: Items.Add bruger Instruktør.ToString() til visning

            // Aktivér/deaktivér knapper afhængig af om der er instruktører
            opretHold.Enabled = filtreret.Any();   // kald: Any() tjekker om listen indeholder elementer
            opdaterHold.Enabled = filtreret.Any();

            // Hvis der er elementer, vælg første som default
            if (Instruktør.Items.Count > 0)
                Instruktør.SelectedIndex = 0;
        }

        private bool InstruktørKanUndervise(Instruktører.Instruktør inst, string type)
        {
            // Returnerer true hvis instruktøren har certifikat for den givne type
            return type switch
            {
                "SkovYoga" => inst.CertifikatSkovYoga,
                "Trailrunning" => inst.CertifikatTrailrunning,
                _ => true
            };
        }

        private void opretHold_Click(object sender, EventArgs e)
        {
            // Læs valgt instruktør og holdtype fra UI
            var instr = (Instruktører.Instruktør)Instruktør.SelectedItem;
            var type = holdType.SelectedItem.ToString();

            // Tjek certificering før oprettelse
            if (!InstruktørKanUndervise(instr, type))
            {
                MessageBox.Show("Denne instruktør har ikke certifikat til denne holdtype.");
                return;
            }

            // Opret nyt Hold-objekt med værdier fra UI
            var nyt = new Hold
            {
                // TODO[DB-ERSTAT]: Undgå client-side id-generation. Brug DB til at generere id 
                HoldId = HoldRepository.HoldListe.Count + 1, // kald: brug repository-størrelse for nyt id
                HoldType = type,
                DatoTid = startDato.Value,
                Instruktør = instr,
                MaxAntal = (int)maxAntalPåHold.Value
            };

            // TODO[DB-ERSTAT]: Erstat denne Add med holdRepository.AddAsync(nyt)
            // Tilføj hold til global repository (gemmer holdet)
            HoldRepository.HoldListe.Add(nyt);

            // Opdater gridvisning så brugeren ser det nye hold
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            // Ryd alle rækker i grid før genopbygning
            kommendeHold.Rows.Clear();

            // Loop: tilføj hver hold-række til grid
            foreach (var h in HoldRepository.HoldListe)
            {
                // TODO[DB-ERSTAT]: Erstat direkte iteration af HoldRepository.HoldListe med GetAllAsync() fra repository
                // Tilføj række med ID, type, formateret dato, instruktørnavn og max antal
                kommendeHold.Rows.Add(
                    h.HoldId,
                    h.HoldType,
                    h.DatoTid.ToString("g"),
                    h.Instruktør?.Navn ?? "",
                    h.MaxAntal
                );
            }
        }

        private void opdaterHold_Click(object sender, EventArgs e)
        {
            // Hvis ingen række er valgt, gør intet
            if (kommendeHold.SelectedRows.Count == 0) return;

            // Læs HoldId fra valgte række
            int id = Convert.ToInt32(kommendeHold.SelectedRows[0].Cells[0].Value);
            // Hent hold fra repository baseret på id (LINQ FirstOrDefault)
            var hold = HoldRepository.HoldListe.FirstOrDefault(h => h.HoldId == id);
            if (hold == null) return;

            // Læs valgt instruktør og type fra UI
            var instr = (Instruktører.Instruktør)Instruktør.SelectedItem;
            var type = holdType.SelectedItem.ToString();

            // Tjek certificering før opdatering
            if (!InstruktørKanUndervise(instr, type))
            {
                MessageBox.Show("Instruktøren har ikke certifikat til denne holdtype.");
                return;
            }

            // Opdater felter på hold-objektet
            hold.HoldType = type;
            hold.DatoTid = startDato.Value;
            hold.Instruktør = instr;
            hold.MaxAntal = (int)maxAntalPåHold.Value;

            // Opdater grid så ændringerne vises
            UpdateDataGrid();
        }

        private void sletHold_Click(object sender, EventArgs e)
        {
            // Hvis ingen række er valgt, gør intet
            if (kommendeHold.SelectedRows.Count == 0) return;

            // Læs HoldId fra valgt række
            int id = Convert.ToInt32(kommendeHold.SelectedRows[0].Cells[0].Value);

            // TODO[DB-ERSTAT]: Erstat RemoveAll med holdRepository.DeleteAsync(id)
            // Fjern alle hold med dette id fra repository
            HoldRepository.HoldListe.RemoveAll(h => h.HoldId == id);

            // Opdater grid efter sletning
            UpdateDataGrid();
        }

        private void kommendeHold_SelectionChanged(object sender, EventArgs e)
        {
            // Hvis ingen række er valgt, gør intet
            if (kommendeHold.SelectedRows.Count == 0) return;

            // Læs valgt HoldId
            int id = Convert.ToInt32(kommendeHold.SelectedRows[0].Cells[0].Value);
            // Hent hold fra repository
            var hold = HoldRepository.HoldListe.FirstOrDefault(h => h.HoldId == id);
            if (hold == null) return;

            // Udfyld UI felter med holdets data
            holdType.SelectedItem = hold.HoldType;
            startDato.Value = hold.DatoTid;
            maxAntalPåHold.Value = hold.MaxAntal;

            // Loop: find index i instruktør-combobox, som matcher holdets instruktør
            for (int i = 0; i < Instruktør.Items.Count; i++)
            {
                // Tjek om dette combo-item har samme InstruktørId som holdets instruktør
                if (((Instruktører.Instruktør)Instruktør.Items[i]).InstruktørId == hold.Instruktør.InstruktørId)
                {
                    // Sæt combobox selection til det fundne indeks
                    Instruktør.SelectedIndex = i;
                    break;
                }
            }
        }
    }

}