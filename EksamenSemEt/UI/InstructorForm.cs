using DatabaseAccessSem1;
using DatabaseAccessSem1.Repository;
using EksamenSemEt.DatabaseAccess.Repository;
using EksamenSemEt.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Sem1BackupForms.Forms
{
    public partial class InstructorForm : UserControl
    {
        private InstructorRepository instructorRepo;
        private CertificationRepository certificateRepo;
        private DataGridView dgv;
        private BindingSource bindingSource;
        private bool isLoading = false;
        private int queryLimit = 99999; //Mindre gør siden hurtigere at loade
        public InstructorForm(InstructorRepository instructorRepo, CertificationRepository certificateRepo)
        {
            this.instructorRepo = instructorRepo;
            this.certificateRepo = certificateRepo;
            InitializeComponent();

            LoadAllCertificatesIntoList(CertificateCheckedList);
            LoadAllCertificatesIntoList(CertificatesCreateCheckedList);

            InitializeDataGridView();

            this.CertificateCheckedList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.CertificateCheckedList_ItemCheck);

        }


        private void InitializeDataGridView()
        {
            dgv = InstructorListView;
            dgv.Dock = DockStyle.Fill;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = false;

            dgv.SelectionChanged += Dgv_SelectionChanged;

            dgv.CellValueChanged += Dgv_CellValueChanged;

            tableLayoutPanel3.Controls.Add(dgv);

            DataGridHelper.LoadData(dgv, ref bindingSource, instructorRepo.broadSearch(searchString: SearchFieldText.Text, limit: queryLimit));

            var types = certificateRepo.GetAll().ToList();

            var idColumn = dgv.Columns["InstructorID"];
            idColumn.ReadOnly = true;
            idColumn.DefaultCellStyle.BackColor = Color.LightGray;
            idColumn.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            idColumn.DefaultCellStyle.SelectionForeColor = Color.Black;

            var fullNameColumn = dgv.Columns["FullName"];
            fullNameColumn.Visible = false;


        }

        private void Dgv_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count > 0)
            {
                var selectedInstructor = dgv.SelectedRows[0].DataBoundItem as Instructor;

                if (selectedInstructor != null)
                {
                    UpdateCheckListForInstructor(selectedInstructor.InstructorID);
                }
            }
        }

        private void UpdateCheckListForInstructor(int? instructorID)
        {
            isLoading = true;
            List<Certificate> certs = certificateRepo.GetByInstructorID(instructorID ?? -1).ToList();

            for (int i = 0; i < CertificateCheckedList.Items.Count; i++)
            {
                var certInList = CertificateCheckedList.Items[i] as Certificate;

                if (certInList != null)
                {
                    bool instructorHasCert = certs.Exists(currentItem => currentItem.CertificationID == certInList.CertificationID);

                    CertificateCheckedList.SetItemChecked(i, instructorHasCert);
                }
            }
            isLoading = false;
        }

        private void LoadAllCertificatesIntoList(CheckedListBox box)
        {
            var certificates = certificateRepo.GetAll().ToList();

            box.DataSource = certificates;
            box.DisplayMember = "Name";
            box.ValueMember = "CertificationID";
        }

        public void CertificateCheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isLoading) return;

            if (dgv.SelectedRows.Count <= 0) return;
            {
                var selectedInstructor = dgv.SelectedRows[0].DataBoundItem as Instructor;
                if (selectedInstructor == null) return;


                var clickedfCert = CertificateCheckedList.Items[e.Index] as Certificate;
                if (clickedfCert == null) return;


                if (e.NewValue == CheckState.Checked)
                {
                    certificateRepo.CreateGroup(selectedInstructor.InstructorID ?? -1, clickedfCert.CertificationID ?? -1);
                }
                else
                {
                    certificateRepo.RemoveGroup(selectedInstructor.InstructorID ?? -1, clickedfCert.CertificationID ?? -1);
                }
            }
        }

        private void AddInstructorButton_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = FirstNameTextBox.Text.Trim();
                string lastName = LastNameTextBox.Text.Trim();

                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                {
                    MessageBox.Show("Fornavn og Efternavn er påkrævet.", "Obligatoriske Felter Mangler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var instructor = new Instructor
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                Instructor createdInstructor = instructorRepo.Create(instructor);

                foreach (var item in CertificatesCreateCheckedList.CheckedItems)
                {
                    var cert = item as Certificate;

                    if (cert != null)
                    {
                        certificateRepo.CreateGroup(createdInstructor.InstructorID ?? -1, cert.CertificationID ?? -1);
                    }
                }

                DataGridHelper.LoadData(dgv, ref bindingSource, instructorRepo.broadSearch(searchString: SearchFieldText.Text, limit: queryLimit));

                DataGridHelper.ShowSuccess("Instruktøren er oprettet succesfuldt!");
            }
            catch (Exception ex) // Add 'Exception ex' here
            {
                // Show the actual error message
                MessageBox.Show($"Der opstod en fejl: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void SearchFieldText_TextChanged(object sender, EventArgs e)
        {
            DataGridHelper.LoadData(dgv, ref bindingSource, instructorRepo.broadSearch(searchString: SearchFieldText.Text, limit: queryLimit));
        }

        private void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || isLoading) return;

            var updatedInstructor = dgv.Rows[e.RowIndex].DataBoundItem as Instructor;

            if (updatedInstructor != null)
            {
                if (string.IsNullOrWhiteSpace(updatedInstructor.LastName) || string.IsNullOrWhiteSpace(updatedInstructor.LastName))
                {
                    DataGridHelper.LoadData(dgv, ref bindingSource, instructorRepo.broadSearch(searchString: SearchFieldText.Text, limit: queryLimit));
                    return;
                }

                try
                {
                    instructorRepo.Update(updatedInstructor);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kunne ikke gemme ændring: {ex.Message}");

                    DataGridHelper.LoadData(dgv, ref bindingSource, instructorRepo.broadSearch(searchString: SearchFieldText.Text, limit: queryLimit));
                }

            }
        }

        private void InstructorListView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("Er du sikker på at slette denne instruktør Permenent? \n NO TAKESIES BACKSIES \n \nEr du i tvivl er svaret nej", "Slet Instruktør", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                int id = Convert.ToInt32(e.Row.Cells["InstructorID"].Value);
                instructorRepo.Delete(id, certificateRepo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kunne ikke slette instruktør: {ex.Message}");
                e.Cancel = true;

            }
        }

    }

}
