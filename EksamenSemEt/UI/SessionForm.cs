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
using Timer = System.Windows.Forms.Timer;

namespace EksamenSemEt.UI
{
    public partial class SessionForm : UserControl
    {
        private readonly SessionRepository sessionRepo;
        private readonly InstructorRepository instructorRepo;
        private readonly CertificationRepository certRepo;
        private readonly LocationRepository locationRepo;
        private readonly InstructorGroupRepository instructorGroupRepo;
        private HashSet<int> selectedInstructors = new HashSet<int>(); //Lige som liste men undgår samme data flere gange
        private bool is_Binding = false;
        private DataGridView dgv;
        private BindingSource bindingSource;
        private SessionTimeEditor sessionTimeEditor;

        public SessionForm(CertificationRepository certificationRepository, LocationRepository locationRepository, InstructorRepository instructorRepository, InstructorGroupRepository instructorGroupRepository, SessionRepository sessionRepository)
        {
            this.sessionRepo = sessionRepository;
            this.instructorRepo = instructorRepository;
            this.locationRepo = locationRepository;
            this.certRepo = certificationRepository;
            this.instructorGroupRepo = instructorGroupRepository;
            InitializeComponent();
            InitializeSessionType();
            InitializeLocation();
            InitializeInstructors();
            InitializeDataGridView();


            SessionTypeSearchComboBox.SelectedIndexChanged += OnSearchCriteriaChanged;
            LocationSearchComboBox.SelectedIndexChanged += OnSearchCriteriaChanged;
            SessionStartSearchDatePicker.ValueChanged += OnSearchCriteriaChanged;
            SessionEndSearchDatePicker.ValueChanged += OnSearchCriteriaChanged;
            MaxMembersSearchUpDown.ValueChanged += OnSearchCriteriaChanged;
            MinMembersSearchUpDown.ValueChanged += OnSearchCriteriaChanged;
            AvailableSpacesSearchUpDown.ValueChanged += OnSearchCriteriaChanged;


            SessionTypeSearchComboBox.SelectedIndex = -1;
            LocationSearchComboBox.SelectedIndex = -1;
            SessionStartSearchDatePicker.Value = DateTime.Today.AddDays(-7);
            SessionEndSearchDatePicker.Value = DateTime.Today.AddDays(14);
        }

        private void InitializeSessionType()
        {

            var comboBoxes = new List<ComboBox> { SessionTypeComboBox, SessionTypeSearchComboBox };

            foreach (var comboBox in comboBoxes)
            {
                comboBox.DataSource = certRepo.GetAll().OrderBy(c => c.Name).ToList(); //sortere efter navn
                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "CertificationID";
            }

        }

        private void InitializeLocation()
        {
            var comboBoxes = new List<ComboBox> { LocationComboBox, LocationSearchComboBox };
            foreach (var comboBox in comboBoxes)
            {
                comboBox.DataSource = locationRepo.GetAll().OrderBy(c => c.Name).ToList();
                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "LocationID";
            }

        }

        private void InitializeInstructors()
        {
            is_Binding = true;
            var instructors = instructorRepo.broadSearch(
                InstructorSearchFieldTextBox.Text.Trim(),
                SessionTypeComboBox.SelectedValue as int?
                ).ToList();

            InstructorsCheckedList.DataSource = null;

            InstructorsCheckedList.DataSource = instructors;
            InstructorsCheckedList.DisplayMember = "FullName";
            InstructorsCheckedList.ValueMember = "InstructorID";

            for (int i= 0; i < InstructorsCheckedList.Items.Count; i++)
            {
                if (InstructorsCheckedList.Items[i] is Instructor instructor)
                {
                    if (selectedInstructors.Contains(instructor.InstructorID ?? -1)){
                        InstructorsCheckedList.SetItemChecked(i, true);
                    }
                }
            }

            is_Binding = false;

        }

        private void InitializeDataGridView() {
            dgv = SessionListView;

            dgv.Dock = DockStyle.Fill;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = false;

            sessionTimeEditor = new SessionTimeEditor();
            sessionTimeEditor.Visible = false;
            sessionTimeEditor.EditingDone += (s, e) =>CommitEditorChanges();
            sessionTimeEditor.Leave += (s, e) => CommitEditorChanges();
            dgv.CellValueChanged += dgv_CellValueChanged;
            dgv.Controls.Add(sessionTimeEditor);



            tableLayoutPanel1.Controls.Add(dgv);
            
            LoadsearchData();

            var idColumn = dgv.Columns["SessionID"];
            idColumn.ReadOnly = true;
            idColumn.DefaultCellStyle.BackColor = Color.LightGray;
            idColumn.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            idColumn.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void CommitEditorChanges() {
            if (!sessionTimeEditor.Visible) return;

            var row = dgv.CurrentRow;
            if (row == null) return;

            row.Cells["DateTime"].Value = sessionTimeEditor.GetNewStart();
            row.Cells["SessionDuration"].Value = sessionTimeEditor.GetNewDuration();

            sessionTimeEditor.Visible = false;
        }

        private void dgv_Scroll(object sender, ScrollEventArgs e)
        {
            if (sessionTimeEditor.Visible) CommitEditorChanges();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            string dateColName = "DateTime";
            string durationColName = "SessionDuration";

            if (dgv.Columns[e.ColumnIndex].Name == dateColName ||
                dgv.Columns[e.ColumnIndex].Name == durationColName)
            {
                var row = dgv.Rows[e.RowIndex];

                int dateColIndex = dgv.Columns[dateColName].Index;
                int durationIndex = dgv.Columns[durationColName].Index;

                Rectangle dateRect = dgv.GetCellDisplayRectangle(dateColIndex, e.RowIndex, true);

                Rectangle durationRect = dgv.GetCellDisplayRectangle(durationIndex, e.RowIndex, true);

                int totaltWidth = dateRect.Width + durationRect.Width;

                sessionTimeEditor.Location = new Point(dateRect.Width, dateRect.Y);
                sessionTimeEditor.Size = new Size(totaltWidth, dateRect.Height);

                DateTime start = Convert.ToDateTime(row.Cells[dateColName].Value);
                int duration = Convert.ToInt32(row.Cells[durationColName].Value);

                sessionTimeEditor.SetValues(start, duration);
                sessionTimeEditor.Visible = true;
                sessionTimeEditor.Focus();
            } else
            {
                CommitEditorChanges();
            }

        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (is_Binding || e.RowIndex < 0) return;

            var row = dgv.Rows[e.RowIndex];

            try
            {
                var updatedSession = new Session
                {
                    SessionID = Convert.ToInt32(row.Cells["SessionID"].Value),
                    SessionType = Convert.ToInt32(row.Cells["SessionType"].Value),
                    LocationID = row.Cells["LocationID"].Value as int?,

                    DateTime = Convert.ToDateTime(row.Cells["DateTime"].Value),
                    SessionDuration = Convert.ToInt32(row.Cells["SessionDuration"].Value),
                    MaxMembers = Convert.ToInt32(row.Cells["MaxMembers"].Value)
                };

                sessionRepo.Update(updatedSession);

            } catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved opdatering: {ex.Message}");
                LoadsearchData();
            }
        }

        private void OnSearchCriteriaChanged(object? sender, EventArgs e)
        {
            LoadsearchData();
        }


        private void LoadsearchData()
        {
            is_Binding = true;
            DataGridHelper.LoadData(dgv, ref bindingSource, sessionRepo.Search(
                sessionType: (SessionTypeSearchComboBox.SelectedValue is int typeId && typeId > 0) ? typeId : null,
                dateTimeStart: SessionStartSearchDatePicker.Value.Date,
                dateTimeEnd: SessionEndSearchDatePicker.Value.Date,
                maxMembers: MaxMembersSearchUpDown.Value > 0 ? (int)MaxMembersSearchUpDown.Value : null,
                minMembers: MinMembersSearchUpDown.Value > 0 ? (int)MinMembersSearchUpDown.Value : null,
                locationID: (LocationSearchComboBox.SelectedValue is int locID && locID > 0) ? locID : null,
                minSlots: AvailableSpacesSearchUpDown.Value > 0 ? (int)AvailableSpacesSearchUpDown.Value : null
                ));

            is_Binding = false;
        }

        private void CreateSessionButton_Click(object sender, EventArgs e)
        {
            if (selectedInstructors.Count == 0)
            {
                DialogResult result = MessageBox.Show("Du har ikke valgt nogen instruktør \nTryk OK for at forsætte", "Ingen Instruktør Valgt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (result == DialogResult.Cancel) return;
            }

            try
            {
                int sessionType = SessionTypeComboBox.SelectedValue as int? ?? throw new Exception("Ugyldig session type");
                //TimeSpan duration = SessionEndTimePicker.Value.TimeOfDay - SessionStartTimePicker.Value.TimeOfDay;
                //int durationInSeconds = (int)duration.TotalSeconds;

                //if (durationInSeconds < 0) {
                //    MessageBox.Show("Holdtider skal være positive (start først, så slut)", "Ugyldig varighed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    return;
                //}

                //DateTime fullSessionStart = SessionDatePicker.Value.Date + SessionStartTimePicker.Value.TimeOfDay;

                Session newSession = sessionRepo.Create(new Session
                {
                    SessionType = sessionType,
                    DateTime = new DateTime(1994, 05, 25) ,//fullSessionStart,
                    SessionDuration = 125, //durationInSeconds,
                    MaxMembers = (int)maxMembersNumericUpDown.Value,
                    LocationID = LocationComboBox.SelectedValue as int?
                });

                if (newSession.SessionID.HasValue)
                {
                    int createID = newSession.SessionID.Value;

                    foreach (int instructorID in selectedInstructors)
                    {
                        instructorGroupRepo.Create(new InstructorGroup { InstructorID = instructorID, SessionID = createID });
                    }

                    DataGridHelper.ShowSuccess("Hold Oprettet");

                    selectedInstructors.Clear();
                    InitializeInstructors();

                    LoadsearchData();
                }
                else {
                    MessageBox.Show("Fejl ved oprettelse af hold \n Intet ID fra Database?", "Fejl ved Oprettelse af Hold", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }catch (Exception ex) {
                MessageBox.Show($"Fejl ved oprettelse af hold \n FejlMeddelse \n {ex}", "Fejl ved Oprettelse af Hold", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SessionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeInstructors();
        }

        private void InstructorSearchFieldTextBox_TextChanged(object sender, EventArgs e)
        {
            InitializeInstructors();
        }

        private void InstructorsCheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (is_Binding) return;

            var list = sender as CheckedListBox;
            if (list.Items[e.Index] is Instructor instructor) {
                if (e.NewValue == CheckState.Checked)
                {
                    selectedInstructors.Add(instructor.InstructorID ?? -1);
                }
                else
                {
                    selectedInstructors.Remove(instructor.InstructorID ?? -1);
                }
            }
        }

        
    }
}
