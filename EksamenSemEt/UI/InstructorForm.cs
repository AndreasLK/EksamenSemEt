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

namespace Sem1BackupForms.Forms
{
    public partial class InstructorForm : UserControl
    {
        private InstructorRepository instructorRepo;
        private CertificationRepository certificateRepo;
        private DataGridView dgv;
        private BindingSource bindingSource;
        private bool isLoading = false;

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

            LoadData(instructorRepo.broadSearch(SearchFieldText.Text).ToList());

            var types = certificateRepo.GetAll().ToList();

            var idColumn = dgv.Columns["InstructorID"];
            idColumn.ReadOnly = true;
            idColumn.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            idColumn.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightGray;
            idColumn.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;



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

        private void LoadData(List<Instructor> instructors)
        {
            var sortableList = new SortableBindingList<Instructor>(instructors);

            bindingSource = new BindingSource();
            bindingSource.DataSource = sortableList;
            dgv.DataSource = bindingSource;


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

                LoadData(instructorRepo.broadSearch(SearchFieldText.Text).ToList());

                ShowSuccessToast("Instruktøren er oprettet succesfuldt!");
            }
            catch (Exception ex) // Add 'Exception ex' here
            {
                // Show the actual error message
                MessageBox.Show($"Der opstod en fejl: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }



        }

        private void ShowSuccessToast(string message)
        {
            // 1. Create the simplified form
            Form toast = new Form();
            toast.FormBorderStyle = FormBorderStyle.None;
            toast.StartPosition = FormStartPosition.CenterScreen;
            toast.Size = new Size(300, 60);
            toast.BackColor = Color.SeaGreen; // Grøn baggrundsfarve
            toast.TopMost = true; // Altid øverst
            toast.ShowInTaskbar = false; // Skal ikke vises som "seperat" app

            Label lbl = new Label();
            lbl.Text = message;
            lbl.ForeColor = Color.White;
            lbl.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            toast.Controls.Add(lbl);

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1500; // 1.5 Sekund
            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                toast.Close(); // Close the popup
                toast.Dispose(); // Clean up memory
            };

            // 4. Show it and start timer
            timer.Start();
            toast.Show();
        }

        private void SearchFieldText_TextChanged(object sender, EventArgs e)
        {
            LoadData(instructorRepo.broadSearch(SearchFieldText.Text).ToList());
        }

        private void Dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || isLoading) return;

            var updatedInstructor = dgv.Rows[e.RowIndex].DataBoundItem as Instructor;

            if (updatedInstructor != null)
            {
                if(string.IsNullOrWhiteSpace(updatedInstructor.LastName) || string.IsNullOrWhiteSpace(updatedInstructor.LastName)){
                    LoadData(instructorRepo.broadSearch(SearchFieldText.Text).ToList());
                    return;
                }

                try
                {
                    instructorRepo.Update(updatedInstructor);
                } catch (Exception ex)
                {
                    MessageBox.Show($"Kunne ikke gemme ændring: {ex.Message}");

                    LoadData(instructorRepo.broadSearch(SearchFieldText.Text).ToList());
                }
                
            }
        }
    }



}
