using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using EksamenSemEt.DatabaseAccess.Repository;
using DatabaseAccessSem1;

namespace EksamenSemEt.UI
{
    public partial class LocationForm : UserControl
    {

        private LocationRepository locationRepo;
        private DataGridView dgv;
        private BindingSource bindingSource;
        private bool isLoading = false;

        public LocationForm(LocationRepository locationRepository)
        {
            this.locationRepo = locationRepository;

            InitializeComponent();
            InitializeDataGridView();
        }

        private void InitializeDataGridView()
        {
            dgv = LocationListView;
            dgv.Dock = DockStyle.Fill;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = false;

            tableLayoutPanel1.Controls.Add(dgv);
            DataGridHelper.LoadData(dgv, ref bindingSource, locationRepo.BroadSearch(SearchFieldTextBox.Text));

            var idColumn = dgv.Columns["LocationID"];
            idColumn.Visible = false;
        }


        private void LocationCreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                string locationName = LocationNameTextBox.Text.Trim();
                if (string.IsNullOrEmpty(locationName))
                {
                    MessageBox.Show("Lokation navn må ikke være tomt.", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var Certificate = new Location
                {
                    Name = locationName
                };

                locationRepo.Create(Certificate);
                DataGridHelper.LoadData(dgv, ref bindingSource, locationRepo.BroadSearch(SearchFieldTextBox.Text));

                DataGridHelper.ShowSuccess("Lokation oprettet succesfuldt.");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LocationListView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("Er du sikker på at slette denne lokation Permenent? \n NO TAKESIES BACKSIES \nEr du i tvivl er svaret nej", "Slet Lokation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                int id = Convert.ToInt32(e.Row.Cells["LocationID"].Value);
                locationRepo.Delete(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kunne ikke slette lokation: {ex.Message}");
                DataGridHelper.LoadData(dgv, ref bindingSource, locationRepo.BroadSearch(SearchFieldTextBox.Text));
            }
        }

        private void LocationListView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isLoading || e.RowIndex < 0) return;

            var row = dgv.Rows[e.RowIndex];
            string newName = row.Cells["Name"].Value?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(newName))
            {
                DataGridHelper.LoadData(dgv, ref bindingSource, locationRepo.BroadSearch(SearchFieldTextBox.Text));
                return;
            }

            try
            {
                int id = Convert.ToInt32(row.Cells["LocationID"].Value);
                var updatedLocation = new Location
                {
                    LocationID = id,
                    Name = newName
                };
                locationRepo.Update(updatedLocation);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kunne ikke opdatere certifikat: {ex.Message}");
                DataGridHelper.LoadData(dgv, ref bindingSource, locationRepo.BroadSearch(SearchFieldTextBox.Text));
            }
        }

        private void SearchFieldTextBox_TextChanged(object sender, EventArgs e)
        {
            DataGridHelper.LoadData(dgv, ref bindingSource, locationRepo.BroadSearch(SearchFieldTextBox.Text));
        }
    }
}
