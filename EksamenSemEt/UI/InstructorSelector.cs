using DatabaseAccessSem1.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DatabaseAccessSem1.Repository;
using EksamenSemEt.DatabaseAccess.Repository;
using DatabaseAccessSem1;
using Timer = System.Windows.Forms.Timer;
namespace EksamenSemEt.UI
{
    public partial class InstructorSelector : UserControl
    {

        private InstructorRepository instructorRepo;
        private InstructorGroupRepository instructorGroupRepo;
        private HashSet<int> selectedInstructors = new HashSet<int>();
        private HashSet<int> cachedAssignedIDs = new HashSet<int>();

        private HashSet<int> selectedInstructorIDs = new HashSet<int>();
        private int? currentSessionType = null;
        private int? editingSessionID = null;

        private bool isBinding = false;

        private bool initialLoadDone = false;
        private readonly Timer fullLoadTimer = new Timer { Interval = 200};



        public InstructorSelector()
        {
            InitializeComponent();
            InstructorSearchFieldTextBox.TextChanged += (s, e) => LoadInstructors();
            InstructorsCheckedList.ItemCheck += InstructorsCheckedList_ItemCheck;

            fullLoadTimer.Tick += (s, e) => LoadFullInstructorList();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (fullLoadTimer != null)
                {
                    fullLoadTimer.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        public void Configure(InstructorRepository instructorRepo, InstructorGroupRepository instructorGroupRepo)
        {
            this.instructorRepo = instructorRepo;
            this.instructorGroupRepo = instructorGroupRepo;
            LoadInstructors();
        }

        public void BindToSession(int sessionID, int sessionTypeID)
        {
            editingSessionID = sessionID;
            currentSessionType = sessionTypeID;
            LoadInstructors();

            cachedAssignedIDs = instructorGroupRepo.GetInstructorID(sessionID).ToHashSet();

            isBinding = true;
            for (int i= 0; i < InstructorsCheckedList.Items.Count; i++)
            {
                if (InstructorsCheckedList.Items[i] is Instructor inst)
                {
                    int id = inst.InstructorID ?? -1;
                    InstructorsCheckedList.SetItemChecked(i, cachedAssignedIDs.Contains(id));
                }
            }
            isBinding = false;
        }

        public void ClearSessionBinding()
        {
            editingSessionID = null;
            currentSessionType = null;
            cachedAssignedIDs.Clear();
            fullLoadTimer.Stop();
            initialLoadDone = false;
            ClearSelection();
        }

        public void SetSessionType(int? sessionTypeId)
        {
            if (!editingSessionID.HasValue)
            {
                currentSessionType = sessionTypeId;
                LoadInstructors();
            }
        }

        public List<int> GetSelectedID()
        {
            return selectedInstructors.ToList();
        }

        public void ClearSelection()
        {
            selectedInstructors.Clear();
            LoadInstructors();
        }

        private void LoadInstructors()
        {
            if (instructorRepo == null) return;

            fullLoadTimer.Stop();
            PerformInitialLoad();

            if (!initialLoadDone || !string.IsNullOrWhiteSpace(InstructorSearchFieldTextBox.Text.Trim()))
            {
                initialLoadDone=true;
                fullLoadTimer.Start();
            }

        }

        private void PerformInitialLoad()
        {
            isBinding = true;

            var instructors = instructorRepo.broadSearch(InstructorSearchFieldTextBox.Text.Trim(), currentSessionType, limit: 100 ).ToList();

            UpdateCheckedListUI(instructors);

            isBinding = false;


        }

        private void LoadFullInstructorList()
        {
            fullLoadTimer.Stop();
            isBinding = true;

            var instructors = instructorRepo.broadSearch(InstructorSearchFieldTextBox.Text.Trim(), currentSessionType, limit: 99999).ToList();

            UpdateCheckedListUI(instructors);
            isBinding = false;
        }

        private void UpdateCheckedListUI(List<Instructor> instructors)
        {
            InstructorsCheckedList.DataSource = null;
            InstructorsCheckedList.DataSource = instructors;
            InstructorsCheckedList.DisplayMember = "FullName";
            InstructorsCheckedList.ValueMember = "InstructorID";

            for (int i = 0; i < InstructorsCheckedList.Items.Count; i++)
            {
                if (InstructorsCheckedList.Items[i] is Instructor inst)
                {
                    int id = inst.InstructorID ?? -1;

                    bool isAssigned = editingSessionID.HasValue
                        ? cachedAssignedIDs.Contains(id) : selectedInstructors.Contains(id);

                    if (isAssigned)
                    {
                        InstructorsCheckedList.SetItemChecked(i, true);
                    }
                }
            }
        }

        private void InstructorsCheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (isBinding) return;

            var list = sender as CheckedListBox;
            if (list.Items[e.Index] is Instructor inst)
            {
                int id = inst.InstructorID ?? -1;
                bool isChecked = e.NewValue == CheckState.Checked;

                if (editingSessionID.HasValue)
                {
                    if (isChecked)
                    {
                        instructorGroupRepo.Create(new InstructorGroup
                        {
                            SessionID = editingSessionID.Value,
                            InstructorID = id
                        });
                        cachedAssignedIDs.Add(id);
                    }
                    else
                    {
                        instructorGroupRepo.DeleteGroup(editingSessionID.Value, id);
                        cachedAssignedIDs.Remove(id);
                    }
                }
                else
                {
                    if (isChecked)
                    {
                        selectedInstructors.Add(id);
                    }
                    else
                    {
                        selectedInstructors.Remove(id);

                    }
                }
            }
        }
    }
}
