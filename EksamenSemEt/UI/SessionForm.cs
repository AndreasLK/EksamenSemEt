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
        private bool is_Binding = false;
        private DataGridView dgv;
        private BindingSource bindingSource;
        private SessionTimeEditor sessionTimeEditor;

        private readonly Timer debounceTimer;

        public SessionForm(CertificationRepository certificationRepository, LocationRepository locationRepository, InstructorRepository instructorRepository, InstructorGroupRepository instructorGroupRepository, SessionRepository sessionRepository)
        {
            this.sessionRepo = sessionRepository;
            this.instructorRepo = instructorRepository;
            this.locationRepo = locationRepository;
            this.certRepo = certificationRepository;
            this.instructorGroupRepo = instructorGroupRepository;
            InitializeComponent();

            debounceTimer = new Timer();
            debounceTimer.Interval = 50;
            debounceTimer.Tick += DebounceTimer_Tick;

            InstructorCreateSelector.Configure(instructorRepo, instructorGroupRepo);
            InstructorSearchSelector.Configure(instructorRepo, instructorGroupRepo);

            SessionTypeComboBox.SelectedIndexChanged += SessionTypeComboBox_SelectedIndexChanged;

            InitializeSessionType();
            InitializeLocation();
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (debounceTimer != null)
                {
                    debounceTimer.Dispose();
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }


        private void InitializeSessionType()
        {

            var comboBoxes = new List<ComboBox> { SessionTypeComboBox, SessionTypeSearchComboBox };

            foreach (var comboBox in comboBoxes)
            {

                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "CertificationID";
                comboBox.DataSource = certRepo.GetAll().OrderBy(c => c.Name).ToList(); //sortere efter navn
            }

        }

        private void InitializeLocation()
        {
            var comboBoxes = new List<ComboBox> { LocationComboBox, LocationSearchComboBox };
            foreach (var comboBox in comboBoxes)
            {

                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "LocationID";
                comboBox.DataSource = locationRepo.GetAll().OrderBy(c => c.Name).ToList();
            }

        }

        private void InitializeDataGridView()
        {
            dgv = SessionListView;

            dgv.Dock = DockStyle.Fill;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = true;
            dgv.ReadOnly = false;

            sessionTimeEditor = new SessionTimeEditor();
            sessionTimeEditor.Visible = false;
            sessionTimeEditor.EditingDone += (s, e) => CommitEditorChanges();
            sessionTimeEditor.Leave += (s, e) => CommitEditorChanges();
            dgv.CellValueChanged += dgv_CellValueChanged;
            dgv.CellClick += dgv_CellClick;
            dgv.Scroll += Dgv_Scroll;
            dgv.Controls.Add(sessionTimeEditor);

            dgv.UserDeletingRow += SessionListView_UserDeletingRow;


            dgv.SelectionChanged += SessionListView_SelectionChanged;

            tableLayoutPanel1.Controls.Add(dgv);

            LoadsearchData();

            var idColumn = dgv.Columns["SessionID"];
            idColumn.ReadOnly = true;
            idColumn.DefaultCellStyle.BackColor = Color.LightGray;
            idColumn.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            idColumn.DefaultCellStyle.SelectionForeColor = Color.Black;
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            debounceTimer.Stop();
            PerformLiveInstructorsBinding();
        }

        private void SessionListView_SelectionChanged(object? sender, EventArgs e)
        {
            if (is_Binding) return;

            debounceTimer.Stop();
            debounceTimer.Start();
        }

        private void PerformLiveInstructorsBinding()
        {

            if (dgv.CurrentRow == null || dgv.CurrentRow.Cells["SessionID"].Value == null)
            {
                InstructorSearchSelector.ClearSessionBinding();
                return;
            }

            var row = dgv.CurrentRow;
            int? sessionID = row.Cells["SessionID"].Value as int?;
            int? sessionTypeID = row.Cells["SessionType"].Value as int?;

            if (sessionID.HasValue && sessionTypeID.HasValue)
            {
                InstructorSearchSelector.BindToSession(sessionID.Value, sessionTypeID.Value);

                InstructorCreateSelector.ClearSelection();
            }
            else
            {
                InstructorSearchSelector.ClearSessionBinding();
            }
        }

        private void SessionTypeComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            int? typeId = SessionTypeComboBox.SelectedValue as int?;
            InstructorCreateSelector.SetSessionType(typeId);
        }

        private void CommitEditorChanges()
        {
            if (!sessionTimeEditor.Visible) return;

            var row = dgv.CurrentRow;
            if (row == null) return;

            row.Cells["DateTime"].Value = sessionTimeEditor.GetNewStart();
            row.Cells["SessionDuration"].Value = sessionTimeEditor.GetNewDuration();

            sessionTimeEditor.Visible = false;
        }

        private void Dgv_Scroll(object sender, ScrollEventArgs e)
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

                int scale = 4;
                sessionTimeEditor.Location = new Point(dateRect.X, dateRect.Y);
                sessionTimeEditor.Size = new Size(totaltWidth, dateRect.Height * scale);

                DateTime start = Convert.ToDateTime(row.Cells[dateColName].Value);
                int duration = Convert.ToInt32(row.Cells[durationColName].Value);

                sessionTimeEditor.SetValues(start, duration);
                sessionTimeEditor.Visible = true;
                sessionTimeEditor.BackColor = SystemColors.ControlDark;
                sessionTimeEditor.Focus();
            }
            else
            {
                CommitEditorChanges();
            }

        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (is_Binding || e.RowIndex < 0) return;

            var row = dgv.Rows[e.RowIndex];
            string colName = dgv.Columns[e.ColumnIndex].Name;

            try
            {
                int sessionID = Convert.ToInt32(row.Cells["SessionID"].Value);
                int newMax = Convert.ToInt32(row.Cells["MaxMembers"].Value);

                int currentCount = sessionRepo.GetMemberCount(sessionID);

                if (newMax < currentCount)
                {
                    MessageBox.Show($"Du kan ikke sætte max deltagere til {newMax} \nDer er allerede {currentCount} tilmeldte", "Ugyldigt input", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    is_Binding = true;
                    row.Cells["MaxMembers"].Value = currentCount;
                    row.Cells["SlotsAvailable"].Value = 0;
                    is_Binding = false;
                    return;
                }
                row.Cells["SlotsAvailable"].Value = newMax - currentCount;


                var updatedSession = new Session
                {
                    SessionID = sessionID,
                    SessionType = Convert.ToInt32(row.Cells["SessionType"].Value),
                    LocationID = row.Cells["LocationID"].Value as int?,

                    DateTime = Convert.ToDateTime(row.Cells["DateTime"].Value),
                    SessionDuration = Convert.ToInt32(row.Cells["SessionDuration"].Value),
                    MaxMembers = Convert.ToInt32(row.Cells["MaxMembers"].Value)
                };

                sessionRepo.Update(updatedSession);

            }
            catch (Exception ex)
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
            List<int> selectedIDs = InstructorCreateSelector.GetSelectedID();

            if (selectedIDs.Count == 0)
            {
                DialogResult result = MessageBox.Show("Du har ikke valgt nogen instruktør \nTryk Ja for at fortsætte uden, eller Nej for at annullere.", "Ingen Instruktør Valgt", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No) return;
            }

            try
            {
                int? sessionType = SessionTypeComboBox.SelectedValue as int?;
                if (sessionType == null)
                {
                    MessageBox.Show("Vælg venligst en holdtype");
                    return;
                }

                DateTime startDateTime = CreationTimeEditor.GetNewStart();
                int durationInSeconds = CreationTimeEditor.GetNewDuration();

                if (durationInSeconds <= 0)
                {
                    MessageBox.Show("Holdtider skal være positive (start først, så slut)", "Ugyldig varighed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                Session newSession = sessionRepo.Create(new Session
                {
                    SessionType = sessionType.Value,
                    DateTime = startDateTime,
                    SessionDuration = durationInSeconds,
                    MaxMembers = (int)maxMembersNumericUpDown.Value,
                    LocationID = LocationComboBox.SelectedValue as int?
                });

                if (newSession.SessionID.HasValue)
                {
                    int createID = newSession.SessionID.Value;
                    foreach (int instructorID in selectedIDs)
                    {
                        instructorGroupRepo.Create(new InstructorGroup { InstructorID = instructorID, SessionID = createID });
                    }

                    DataGridHelper.ShowSuccess("Hold Oprettet");

                    InstructorCreateSelector.ClearSelection();
                    CreationTimeEditor.SetValues(DateTime.Now, 60 * 45); //45 min

                    LoadsearchData();
                }
                else
                {
                    MessageBox.Show("Fejl ved oprettelse af hold \n Intet ID fra Database?", "Fejl ved Oprettelse af Hold", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved oprettelse af hold \n FejlMeddelse \n {ex}", "Fejl ved Oprettelse af Hold", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SessionListView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show( "Er du sikker på at slette dette hold Permanent? \n NO TAKESIES BACKSIES \n \nEr du i tvivl er svaret nej", "Slet Hold", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                int id = Convert.ToInt32(e.Row.Cells["SessionID"].Value);

                sessionRepo.Delete(id);
            } catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved sletning af hold: {ex.Message}", "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);

                e.Cancel = true;
            }
        }
    }
}
