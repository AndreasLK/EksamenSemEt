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
using System.Xml.Linq;

namespace EksamenSemEt.UI
{
    public enum HistoryMode { Member, Instructor}
    public partial class HistoryControl : UserControl //Lavet af Andreas
    {
        private readonly SessionRepository sessionRepository;
        private readonly MemberRepository memberRepository;
        private readonly InstructorRepository instructorRepository;

        private readonly MemberGroupRepository memberGroupRepository;
        private readonly InstructorGroupRepository instructorGroupRepository;

        private HistoryMode mode;
        private int entityID;
        private Button backButton;

        public event EventHandler CloseRequested;
        public HistoryControl(
            SessionRepository sessionRepo,
            MemberRepository memberRepo,
            InstructorRepository instructorRepo,
            MemberGroupRepository memberGroupRepo,
            InstructorGroupRepository instructorGroupRepo
            )
        {
            InitializeComponent();

            this.sessionRepository = sessionRepo;
            this.memberRepository = memberRepo;
            this.instructorRepository = instructorRepo;
            this.memberGroupRepository = memberGroupRepo;
            this.instructorGroupRepository = instructorGroupRepo;

            SetupDynamicBackButton();
            ConfigureGrids();
            HistoryDGV.SelectionChanged += HistoryDGV_SelectionChanged;
            HistoryDGV.UserDeletingRow += HistoryDGV_UserDeletingRow;
            HistoryDGV.DataBindingComplete += HistoryDGV_DataBindingComplete;

        }

        private void SetupDynamicBackButton()
        {
            backButton = new Button();
            backButton.Text = "Tilbage";
            backButton.Size = new Size(75, 30);
            backButton.Location = new Point(this.Width - 85, 5); //Man burde ungdå "magic numbers" men klokken er 5 om morgenen så vi siger fuck it, i det her tilfælde
            backButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            backButton.Click += (s, e) => CloseRequested?.Invoke(this, EventArgs.Empty);
            this.Controls.Add(backButton);
            backButton.BringToFront();
        }

        private void ConfigureGrids()
        {
            InfoGrid.ReadOnly = true;
            InfoGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            InfoGrid.AllowUserToAddRows = false;
            InfoGrid.RowHeadersVisible = false;

            HistoryDGV.ReadOnly = false;
            HistoryDGV.AllowUserToDeleteRows = true;
            HistoryDGV.AllowUserToAddRows = false;
            HistoryDGV.AutoGenerateColumns = true;

            HistoryDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            HistoryDGV.MultiSelect = false;
            HistoryDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            AttendeesDGV.ReadOnly = true;
            AttendeesDGV.AutoGenerateColumns = true;
            AttendeesDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            AttendeesDGV.MultiSelect = false;
            AttendeesDGV.AllowUserToAddRows = false;
            AttendeesDGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void HistoryDGV_DataBindingComplete(object? sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewColumn col in HistoryDGV.Columns)
            {
                col.ReadOnly = true;
            }
        }

        public void LoadHistory(Member member)
        {
            mode = HistoryMode.Member;
            entityID = member.MemberID ?? -1;
            InfoGrid.DataSource = new List<Member> { member };

            RefreshSessionList();
        }

        public void LoadHistory(Instructor instructor)
        {
            mode = HistoryMode.Instructor;
            entityID = instructor.InstructorID ?? -1;
            InfoGrid.DataSource = new List<Instructor> { instructor };

            RefreshSessionList();
        }

        private void RefreshSessionList()
        {
            IEnumerable<Session> sessions;
            if (mode == HistoryMode.Member)
            {
                sessions = sessionRepository.GetSessionsByMember(entityID);
            }
            else
            {
                sessions = sessionRepository.GetSessionsByInstructor(entityID);
            }

            var displayList = sessions.Select(s => new {
                s.SessionID,
                Dato = s.DateTime.ToString("dd-MM-yyyy"),
                Tid = s.DateTime.ToString("HH:mm"),
                Hold = s.SessionTypeName ?? s.SessionType.ToString(),
                Lokation = s.LocationName ?? s.LocationID.ToString()
            }).ToList();

            HistoryDGV.DataSource = displayList;
            AttendeesDGV.DataSource = null;
        }

        private void HistoryDGV_SelectionChanged(object? sender, EventArgs e)
        {
            if (HistoryDGV.CurrentRow == null)
            {
                AttendeesDGV.DataSource = null;
                return;
            }

            if (HistoryDGV.CurrentRow.Cells["SessionID"].Value is not int sessionID) return;

            if (mode == HistoryMode.Member)
            {
                var instructors = instructorRepository.broadSearch(searchString: "", sessionID: sessionID);

                AttendeesDGV.DataSource = instructors.Select(i => new {
                    Navn = $"{i.FirstName} {i.LastName}"
                }).ToList();
            }
            else
            {
                var members = memberRepository.broadSearch("", sesionID: sessionID);

                AttendeesDGV.DataSource = members.Select(m => new {
                    Navn = $"{m.FirstName} {m.LastName}",
                    Tlf = m.PhoneNumber,
                    Type = m.MemberType
                }).ToList();
            }
        }

        private void HistoryDGV_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.Cells["SessionID"].Value is not int sessionID)
            {
                e.Cancel = true;
                return;
            }

            string msg = mode == HistoryMode.Member
                ? "Vil du afmelde medlemmet fra dette hold?"
                : "Vil du fjerne instruktøren fra dette hold?";

            var result = MessageBox.Show(msg, "Slet fra hold", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }
            try
            {
                if (mode == HistoryMode.Member)
                {
                    memberGroupRepository.DeleteGroup(sessionID, entityID);
                }
                else
                {
                    instructorGroupRepository.DeleteGroup(sessionID, entityID);
                }

                e.Cancel = true;
                RefreshSessionList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl: " + ex.Message);
                e.Cancel = true;
            }
        }

    }
}
