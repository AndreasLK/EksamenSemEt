using DatabaseAccessSem1;
using DatabaseAccessSem1.Repository;
using EksamenSemEt.DatabaseAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EksamenSemEt.UI
{
    public partial class CertificateForm : UserControl
    {
        private CertificationRepository certRepo;
        private SessionRepository sessionRepo;
        private DataGridView dgv;
        private BindingSource bindingSource;
        private bool isLoading = false;

        public CertificateForm(CertificationRepository certificationRepository, SessionRepository sessionRepo)
        {
            this.sessionRepo = sessionRepo;
            this.certRepo = certificationRepository;
            InitializeComponent();
            InitializeDataGridView();
        }


        private void InitializeDataGridView()
        {
            dgv = CertificationListView;
            dgv.Dock = DockStyle.Fill;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = false;

            tableLayoutPanel1.Controls.Add(dgv);
            DataGridHelper.LoadData(dgv, ref bindingSource, certRepo.BroadSearch(SearchFieldTextBox.Text));

            var idColumn = dgv.Columns["CertificationID"];
            idColumn.Visible = false;
        }
        private void CertificateCreateButton_Click(object sender, EventArgs e)
        {
            try
            {
                string certName = CertificateNameTextBox.Text.Trim();
                if (string.IsNullOrEmpty(certName))
                {
                    MessageBox.Show("Certifikat navn må ikke være tomt.", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var Certificate = new Certificate
                {
                    Name = certName
                };

                certRepo.Create(Certificate);
                DataGridHelper.LoadData(dgv, ref bindingSource, certRepo.BroadSearch(SearchFieldTextBox.Text));

                DataGridHelper.ShowSuccess("Certifikat oprettet succesfuldt.");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Der opstod en fejl: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CertificationListView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("Er du sikker på at slette dette certifikat Permenent? \nALLE hold med denne type bliver også slettet \n  \n NO TAKESIES BACKSIES \nEr du i tvivl er svaret nej", "Slet Certifikat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                int id = Convert.ToInt32(e.Row.Cells["CertificationID"].Value);
                certRepo.Delete(id, sessionRepo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kunne ikke slette certifikat: {ex.Message}");
                DataGridHelper.LoadData(dgv, ref bindingSource, certRepo.BroadSearch(SearchFieldTextBox.Text));
            }
        }

        private void CertificationListView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isLoading || e.RowIndex < 0) return;

            var row = dgv.Rows[e.RowIndex];
            string newName = row.Cells["Name"].Value?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(newName))
            {
                DataGridHelper.LoadData(dgv, ref bindingSource, certRepo.BroadSearch(SearchFieldTextBox.Text));
                return;
            }

            try
            {
                int id = Convert.ToInt32(row.Cells["CertificationID"].Value);
                var updatedCert = new Certificate
                {
                    CertificationID = id,
                    Name = newName
                };
                certRepo.Update(updatedCert);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kunne ikke opdatere certifikat: {ex.Message}");
                DataGridHelper.LoadData(dgv, ref bindingSource, certRepo.BroadSearch(SearchFieldTextBox.Text));
            }
        }

        private void SearchFieldTextBox_TextChanged(object sender, EventArgs e)
        {
            DataGridHelper.LoadData(dgv, ref bindingSource, certRepo.BroadSearch(SearchFieldTextBox.Text));
        }
    }
}
