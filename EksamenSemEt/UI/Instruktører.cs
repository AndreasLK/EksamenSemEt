using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static FitHubUI.Instruktører;

namespace FitHubUI
{
    public partial class Instruktører : Form
    {
        // Model for Instruktører
        public class Instruktør
        {
            public int InstruktørId { get; set; }
            public string Navn { get; set; }
            public string Email { get; set; }
            public bool CertifikatSkovYoga { get; set; }
            public bool CertifikatTrailrunning { get; set; }

            // Gør at objektet vises med navn i UI-kontroller (CheckedListBox / ComboBox osv.)
            public override string ToString() => Navn;
        }

        // TODO[DB-ERSTAT]: Erstat denne statiske in-memory repository-klasse med et DB-backed repository (IInstruktørRepository).
        //           Implementér: GetAllAsync, GetByIdAsync, AddAsync, UpdateAsync, DeleteAsync.
        public static class InstruktørRepository
        {
            // Internt lager af instruktører
            private static readonly List<Instruktør> instruktører = new List<Instruktør>
        {
            new Instruktør
            {
                InstruktørId = 1,
                Navn = "Søren Madsen",
                Email = "soren@mail.dk",
                CertifikatSkovYoga = true,
                CertifikatTrailrunning = false
            },
            new Instruktør
            {
                InstruktørId = 2,
                Navn = "Mette Andersen",
                Email = "mette@mail.dk",
                CertifikatSkovYoga = true,
                CertifikatTrailrunning = true
            },
            new Instruktør
            {
                InstruktørId = 3,
                Navn = "John Pedersen",
                Email = "john@mail.dk",
                CertifikatSkovYoga = false,
                CertifikatTrailrunning = true
            }
        };

            // Returnerer hele listen (bruges til at fylde UI)
            public static List<Instruktør> HentAlleInstruktører() => instruktører;

            // Finder enkelt instruktør på id (bruges ved opdatering/slet)
            public static Instruktør HentInstruktør(int id) =>
                instruktører.FirstOrDefault(i => i.InstruktørId == id);

            // Tilføjer en ny instruktør til den interne liste
            public static void TilføjInstruktør(Instruktør inst) =>
                instruktører.Add(inst);

            // Opdaterer eksisterende instruktør (finder på id og sætter felter)
            public static void OpdaterInstruktør(Instruktør inst)
            {
                var e = HentInstruktør(inst.InstruktørId); // kald til HentInstruktør
                if (e == null) return;

                // Sæt felter på den fundne instruktør
                e.Navn = inst.Navn;
                e.Email = inst.Email;
                e.CertifikatSkovYoga = inst.CertifikatSkovYoga;
                e.CertifikatTrailrunning = inst.CertifikatTrailrunning;
            }

            // Sletter instruktør(e) med matching id
            public static void SletInstruktør(int id) =>
                instruktører.RemoveAll(i => i.InstruktørId == id);
        }

        public Instruktører()
        {
            InitializeComponent(); // initialiserer designer-genererede UI-kontroller

            // Fyld grid og dropdowns ved opstart
            LoadInstruktørListe();      // kald: opretter datasource til instruktørGrid
            LoadInstruktørDropdown();   // kald: fylder dropdown til valg af instruktør
            LoadCertifikatDropdown();   // kald: fylder certifikatvalg
        }

        private void LoadInstruktørListe()
        {
            // Henter alle instruktører fra repository, projicerer til anonym type til visning
            instruktørGrid.DataSource = InstruktørRepository.HentAlleInstruktører()
                .Select(i => new
                {
                    i.InstruktørId,
                    i.Navn,
                    i.Email,
                    SkovYoga = i.CertifikatSkovYoga ? "Ja" : "Nej",   // LINQ-projektion: formatér certifikat
                    Trailrun = i.CertifikatTrailrunning ? "Ja" : "Nej" // LINQ-projektion: formatér certifikat
                })
                .ToList(); // Materialiserer resultatet til en liste og binder til grid (kald ToList)
        }

        private void LoadInstruktørDropdown()
        {
            // Ryd eksisterende items i dropdown før genfyldning
            vælgInstruktørKommendeHold.Items.Clear();

            // Loop: tilføj hver instruktør fra repository til dropdown
            foreach (var inst in InstruktørRepository.HentAlleInstruktører())
                vælgInstruktørKommendeHold.Items.Add(inst); // kald: Items.Add(object) bruger Instruktør.ToString()

            // Hvis der er mindst én instruktør, vælg første element i dropdown
            if (vælgInstruktørKommendeHold.Items.Count > 0)
                vælgInstruktørKommendeHold.SelectedIndex = 0; // kald: sæt default selection
        }

        private void LoadCertifikatDropdown()
        {
            // Ryd og tilføj de faste valgmuligheder for certifikater
            certifikater.Items.Clear();
            certifikater.Items.Add("Ingen");       // kald: tilføj "Ingen"
            certifikater.Items.Add("SkovYoga");    // kald: tilføj "SkovYoga"
            certifikater.Items.Add("Trailrunning");// kald: tilføj "Trailrunning"
            certifikater.Items.Add("Begge");       // kald: tilføj "Begge"

            // Sæt default selection til første entry
            certifikater.SelectedIndex = 0; // kald: vælg "Ingen"
        }

        private void opretInstruktør_Click(object sender, EventArgs e)
        {
            // Opretter et nyt Instruktør-objekt baseret på brugerinput
            var inst = new Instruktør
            {
                InstruktørId = InstruktørRepository.HentAlleInstruktører().Count + 1, // kald: HentAlleInstruktører for at bestemme næste id
                Navn = navnInstruktør.Text,
                Email = emailInstruktør.Text,
                // Brug ternærtjek på SelectedItem for at sætte certifikater
                CertifikatSkovYoga = certifikater.SelectedItem.ToString() is "SkovYoga" or "Begge",
                CertifikatTrailrunning = certifikater.SelectedItem.ToString() is "Trailrunning" or "Begge"
            };

            InstruktørRepository.TilføjInstruktør(inst); // kald: gem ny instruktør i repository

            // Genopfrisk UI: genindlæs grid og dropdown så ny instruktør vises
            LoadInstruktørListe();    // kald: opdater grid datasource
            LoadInstruktørDropdown();// kald: opdater dropdownliste
        }

        private void gemInstruktør_Click(object sender, EventArgs e)
        {
            // Hvis ingen rækker er valgt i grid, gør intet
            if (instruktørGrid.SelectedRows.Count == 0) return;

            // Læs id fra den valgte række og hent instans fra repository
            int id = Convert.ToInt32(instruktørGrid.SelectedRows[0].Cells["InstruktørId"].Value); // kald: læs celleværdi
            var inst = InstruktørRepository.HentInstruktør(id); // kald: hent instruktør fra repo

            // Opdater felter fra UI
            inst.Navn = navnInstruktør.Text;
            inst.Email = emailInstruktør.Text;

            // Nulstil certifikatfelter før at sætte efter valg
            inst.CertifikatSkovYoga = false;
            inst.CertifikatTrailrunning = false;

            // Brug switch på SelectedItem for at sætte korrekte certifikater
            switch (certifikater.SelectedItem.ToString())
            {
                case "SkovYoga": inst.CertifikatSkovYoga = true; break; // sæt skov-yoga
                case "Trailrunning": inst.CertifikatTrailrunning = true; break; // sæt trailrunning
                case "Begge": inst.CertifikatSkovYoga = inst.CertifikatTrailrunning = true; break; // sæt begge
            }

            InstruktørRepository.OpdaterInstruktør(inst); // kald: skriv opdateringer tilbage til repository

            // Genopfrisk UI for at vise opdaterede data
            LoadInstruktørListe();    // kald: opdater grid datasource
            LoadInstruktørDropdown();// kald: opdater dropdownliste
        }

        private void sletInstruktør_Click(object sender, EventArgs e)
        {
            // Stop hvis ingen række er valgt
            if (instruktørGrid.SelectedRows.Count == 0) return;

            // Læs id fra valgt række og slet via repository
            int id = Convert.ToInt32(instruktørGrid.SelectedRows[0].Cells["InstruktørId"].Value); // kald: læs celleværdi
            InstruktørRepository.SletInstruktør(id); // kald: slet instruktør i repository

            // Opdater UI efter sletning
            LoadInstruktørListe();    // kald: opdater grid datasource
            LoadInstruktørDropdown();// kald: opdater dropdownliste
        }

        private void vælgInstruktørKommendeHold_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Hent valgt instruktør fra dropdown og stop hvis ingen valgt
            var inst = vælgInstruktørKommendeHold.SelectedItem as Instruktør; // kald: læs SelectedItem
            if (inst == null) return;

            // Filtrér globale HoldRepository efter instruktørens id, projicer til visningsobjekt og binder til grid
            kommendeHold.DataSource = HoldRepository.HoldListe
                .Where(h => h.Instruktør.InstruktørId == inst.InstruktørId) // LINQ: filtrer kun hold med denne instruktør
                .Select(h => new
                {
                    h.HoldId,
                    h.HoldType,
                    Dato = h.DatoTid.ToString("g"), // formater dato for visning
                    h.MaxAntal
                })
                .ToList(); // Materialiser og bind til grid (kald: ToList)
        }

        private void instruktører_SelectionChanged(object sender, EventArgs e)
        {
            // Hvis ingen rækker er valgt, gør intet
            if (instruktørGrid.SelectedRows.Count == 0) return;

            // Læs id fra valgt række og hent instruktør fra repository
            int id = Convert.ToInt32(instruktørGrid.SelectedRows[0].Cells["InstruktørId"].Value); // kald: læs celleværdi
            var inst = InstruktørRepository.HentInstruktør(id); // kald: hent instruktør

            // Udfyld inputfelter med instruktørens data
            navnInstruktør.Text = inst.Navn;
            emailInstruktør.Text = inst.Email;

            // Opdater certifikat-dropdown så det matcher instruktørens certifikater
            if (inst.CertifikatSkovYoga && inst.CertifikatTrailrunning)
                certifikater.SelectedItem = "Begge";      // kald: sæt dropdown til "Begge"
            else if (inst.CertifikatSkovYoga)
                certifikater.SelectedItem = "SkovYoga";   // kald: sæt dropdown til "SkovYoga"
            else if (inst.CertifikatTrailrunning)
                certifikater.SelectedItem = "Trailrunning";// kald: sæt dropdown til "Trailrunning"
            else
                certifikater.SelectedItem = "Ingen";      // kald: sæt dropdown til "Ingen"
        }
    }
}