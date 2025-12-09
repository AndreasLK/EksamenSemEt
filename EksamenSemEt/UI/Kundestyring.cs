using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FitHubUI
{
    // Dummy klasse til test af program — SKAL fjernes senere
    public class Kunde
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Email { get; set; }
        public bool Aktiv { get; set; }
    }

    public partial class Kundestyring : Form
    {
        //også en del at dummy klasse til test - SKAL fjernes senere
        private BindingList<Kunde> kunder = new BindingList<Kunde>();

        public Kundestyring()
        {
            InitializeComponent();
            Load += Kundestyring_Load;

            listeAfKunder.SelectionChanged += listeAfKunder_SelectionChanged;
            opretKunde.Click += opretKunde_Click;
            gemKunde.Click += gemKunde_Click;
            deaktiverKunde.Click += deaktiverKunde_Click;
        }

        // Form Load
        private void Kundestyring_Load(object sender, EventArgs e)
        {
            listeAfKunder.DataSource = kunder;

            listeAfKunder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            listeAfKunder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            listeAfKunder.MultiSelect = false;
            listeAfKunder.ReadOnly = true;
            listeAfKunder.RowHeadersVisible = false;

            // Dummy data
            kunder.Add(new Kunde { Id = 1, Navn = "Peter Hansen", Email = "peter@test.dk", Aktiv = true });
            kunder.Add(new Kunde { Id = 2, Navn = "Lise Jensen", Email = "lise@test.dk", Aktiv = false });
        }

        // Når der vælges kunde
        private void listeAfKunder_SelectionChanged(object sender, EventArgs e)
        {
            if (listeAfKunder.CurrentRow == null) return;

            var kunde = listeAfKunder.CurrentRow.DataBoundItem as Kunde;
            if (kunde == null) return;

            kundeNavn.Text = kunde.Navn;
            kundeEmail.Text = kunde.Email;
            kundeAktiv.Checked = kunde.Aktiv;
        }

        // Opret kunde
        private void opretKunde_Click(object sender, EventArgs e)
        {
            var nyKunde = new Kunde
            {
                Id = kunder.Count + 1,
                Navn = kundeNavn.Text,
                Email = kundeEmail.Text,
                Aktiv = kundeAktiv.Checked
            };

            kunder.Add(nyKunde);
        }

        // Gem / Opdater kunde
        private void gemKunde_Click(object sender, EventArgs e)
        {
            if (listeAfKunder.CurrentRow == null) return;

            var kunde = listeAfKunder.CurrentRow.DataBoundItem as Kunde;
            if (kunde == null) return;

            kunde.Navn = kundeNavn.Text;
            kunde.Email = kundeEmail.Text;
            kunde.Aktiv = kundeAktiv.Checked;

            listeAfKunder.Refresh();
        }

        // Deaktiver kunde
        private void deaktiverKunde_Click(object sender, EventArgs e)
        {
            if (listeAfKunder.CurrentRow == null) return;

            var kunde = listeAfKunder.CurrentRow.DataBoundItem as Kunde;

            var confirm = MessageBox.Show(
                "Er du sikker på at du vil deaktivere kunden?",
                "Bekræft",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                kunder.Remove(kunde);
            }
        }
    }
}