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



        public InstructorSelector()
        {
            InitializeComponent();
            InstructorSearchFieldTextBox.TextChanged += (s, e) => LoadInstructors();
            InstructorsCheckedList.ItemCheck += InstructorsCheckedList_ItemCheck;
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
            selectedInstructors.Clear();
            ClearSelection();
        }

        public void SetSessionType(int? sessionTypeId)
        {
            currentSessionType = sessionTypeId;
            LoadInstructors();
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

            isBinding = true;
            var instructors = instructorRepo.broadSearch(InstructorSearchFieldTextBox.Text.Trim(), currentSessionType).ToList();

            InstructorsCheckedList.DataSource = null;
            InstructorsCheckedList.DataSource = instructors;
            InstructorsCheckedList.DisplayMember = "FullName";
            InstructorsCheckedList.ValueMember = "InstructorID";

            for (int i = 0; i < InstructorsCheckedList.Items.Count; i++)
            {
                if (InstructorsCheckedList.Items[i] is Instructor inst)
                {
                    int id = inst.InstructorID ?? -1;
                    if (!editingSessionID.HasValue)
                    {
                        if (selectedInstructors.Contains(id))
                        {
                            InstructorsCheckedList.SetItemChecked(i, true);
                        }
                    } else //Live edit
                    {
                        if (cachedAssignedIDs.Contains(id))
                        {
                            InstructorsCheckedList.SetItemChecked(i, true);
                        }
                    }
                }
            }

            isBinding = false;

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
                    }
                    else
                    {
                        instructorGroupRepo.DeleteInstructorFromSession(editingSessionID.Value, id);
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
