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
    public partial class MemberSearch : UserControl
    {
        private MemberRepository memberRepo;
        private MemberTypeRepository memberTypeRepo;
        private DataGridView dgv;
        private BindingSource source = new BindingSource();
        private TextBox searchField;

        public event EventHandler<int> MemberDoubleClicked;

        private MemberGroupRepository memberGroupRepo;
        private int? editingSessionID;
        private HashSet<int> cachedSessionMemberIDs;


        private bool isLoading = false;
        private DateTimePicker dtp;
        private Rectangle rectangle;

        private bool initialLoadDone = false;
        private readonly Timer fullLoadTimer = new Timer { Interval = 50 };
        private readonly Timer debounceTimer = new Timer { Interval = 120 };

        public event EventHandler SelectedMemberChanged;
        public int? SelectedMemberID = null;
        public bool isReadOnly = false;
        private bool excludeInactive = false;

        private const string IsSessionMemberColoumnName = "IsBookedOnSession";

        public MemberSearch()
        {
            InitializeComponent();
            dgv = MemberListView;
            dgv.AutoGenerateColumns = false;

            searchField = SearchField;

            fullLoadTimer.Tick += (s, e) => LoadFullMemberData();
            debounceTimer.Tick += (s, e) =>
            {
                debounceTimer.Stop();
                LoadMemberData(isDebounced: true);
            };

  

            dgv.DataSource = source;
            dgv.CellValueChanged += MemberListView_CellValueChanged;
            dgv.CellClick += MemberListView_CellClick;
            dgv.UserDeletingRow += MemberListView_UserDeletingRow;
            dgv.SelectionChanged += Dgv_SelectionChanged;
            dgv.CellContentClick += Dgv_CellContentClick;
            dgv.Scroll += dtp_Scroll;
            dgv.DataError += Dgv_DataError;

            dgv.CellDoubleClick += Dgv_CellDoubleClick;
            searchField.TextChanged += Debounced_SearchFieldText_TextChanged;


        }

        private void Dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var member = dgv.Rows[e.RowIndex].DataBoundItem as MemberViewModel;

            if (member != null && member.MemberID.HasValue)
            {
                MemberDoubleClicked?.Invoke(this, member.MemberID.Value);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                fullLoadTimer.Dispose();
                debounceTimer.Dispose();
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }


        public int? GetSelectedMemberID()
        {
            return SelectedMemberID;
        }

        public bool IsReadOnly()
        {
            return isReadOnly;
        }

        public void SetIsReadOnly(bool value)
        {
            isReadOnly = value;

            if (dgv != null)
            {
                dgv.ReadOnly = false;
                InitializeDGVSetup();

                LoadMemberData();
            }
        }

        public void Configure(
            MemberRepository memberRepo,
            MemberTypeRepository memberTypeRepo,
            MemberGroupRepository? memberGroupRepo = null,
            int? editingSessionID = null,
            bool isReadOnly = false,
            bool excludeInactive = false
            )
        {
            this.memberRepo = memberRepo;
            this.memberTypeRepo = memberTypeRepo;
            this.memberGroupRepo = memberGroupRepo;
            this.editingSessionID = editingSessionID;

            this.isReadOnly = isReadOnly;
            this.excludeInactive = excludeInactive;

            if (this.editingSessionID.HasValue) { 
                cachedSessionMemberIDs = this.memberGroupRepo.GetMembersIDs(this.editingSessionID.Value).ToHashSet();

            } else
            {
                cachedSessionMemberIDs = new HashSet<int>();
            }
            InitializeDGVSetup();

            LoadMemberData();
        }

        private void InitializeDGVSetup()
        {

            dgv.AutoGenerateColumns = false;
            dgv.ReadOnly = isReadOnly;
            dgv.Dock = DockStyle.Fill;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            
            if (dtp == null)
            {
                dtp = new DateTimePicker(); //Til Fødselsdage
                dgv.Controls.Add(dtp);
                dtp.Visible = false;
                dtp.Format = DateTimePickerFormat.Short;
                dtp.ValueChanged += dtp_ValueChanged_Handler;
            }
            dgv.Columns.Clear();

            if (editingSessionID.HasValue) {
                SetupCheckBoxColumn();
            }



            AddTextColumn("MemberID", "ID", "MemberID", readOnly: true);
            AddTextColumn("FirstName", "Fornavn", "FirstName");
            AddTextColumn("LastName", "Efternavn", "LastName");
            AddTextColumn("DateOfBirth", "Fødselsdag", "DateOfBirth", visible: !isReadOnly);
            AddTextColumn("Email", "Email", "Email", visible: !isReadOnly);
            AddTextColumn("PhoneNumber", "Tlf. Nr", "PhoneNumber");

            SetupMemberTypeColumn();

            AddBoolColumn("Active", "Aktiv", "Active", visible: !isReadOnly);

            ApplyColumnStyles();


        }

        private void AddTextColumn(string dataProp, string header, string name, bool readOnly = false, bool visible = true)
        {
            var col = new DataGridViewTextBoxColumn();
            col.DataPropertyName = dataProp;
            col.HeaderText = header;
            col.Name = name;
            col.ReadOnly = isReadOnly || readOnly;
            col.Visible = visible;
            dgv.Columns.Add(col);
        }

        private void AddBoolColumn(string dataProp, string header, string name, bool visible = true)
        {
            var col = new DataGridViewCheckBoxColumn();
            col.DataPropertyName = dataProp;
            col.HeaderText = header;
            col.Name = name;
            col.ReadOnly = isReadOnly;
            col.Visible = visible;
            dgv.Columns.Add(col);
        }


        private void SetupCheckBoxColumn()
        {
            if (dgv.Columns.Contains(IsSessionMemberColoumnName)) return; // dipper hvis den allerede er sat

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.HeaderText = "Meldt På Hold";
            checkColumn.Name = IsSessionMemberColoumnName;
            checkColumn.DataPropertyName = IsSessionMemberColoumnName;
            checkColumn.ReadOnly = false;
            checkColumn.Width = 50; //Evt. fjern senere

            dgv.Columns.Insert(0, checkColumn);
        }

        private void SetupMemberTypeColumn()
        {
            if (memberTypeRepo == null) return;


            try
            {
                var types = memberTypeRepo.GetAll().ToList();


                if (dgv.Columns.Contains("MemberType"))
                {
                    var col = (DataGridViewComboBoxColumn)dgv.Columns["MemberType"];
                    col.DataSource = types;
                    return;
                }



                DataGridViewComboBoxColumn memberTypeColumn = new DataGridViewComboBoxColumn();
                memberTypeColumn.HeaderText = "Medlemstype";
                memberTypeColumn.Name = "MemberType";
                memberTypeColumn.DataSource = types;
                memberTypeColumn.DisplayMember = "Name";
                memberTypeColumn.ValueMember = "MemberTypeID";
                memberTypeColumn.DataPropertyName = "MemberType";
                dgv.Columns.Add(memberTypeColumn);

                if (isReadOnly)
                {
                    memberTypeColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    memberTypeColumn.FlatStyle = FlatStyle.Flat;
                }
                else
                {
                    memberTypeColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
                    memberTypeColumn.DisplayStyleForCurrentCellOnly = true;
                    memberTypeColumn.FlatStyle = FlatStyle.Standard;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kunne ikke indlæse medlemstyper: {ex.Message}");
            }
        }

        private void ApplyColumnStyles()
        {
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Name == "MemberID")
                {
                    col.DefaultCellStyle.BackColor = Color.LightGray;
                }
                else if (col.ReadOnly)
                {
                    col.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    col.DefaultCellStyle.ForeColor = Color.Gray;
                }
                else
                {
                    col.DefaultCellStyle.BackColor = Color.White;
                    col.DefaultCellStyle.ForeColor = Color.Black;
                }
            }
        }

        private void Debounced_SearchFieldText_TextChanged(object? sender, EventArgs e)
        {
            fullLoadTimer.Stop();
            debounceTimer.Stop();
            debounceTimer.Start();
        }

        public void LoadMemberData(bool isDebounced = false)
        {
            fullLoadTimer.Stop();
            PerfomInitialLoad();

            if (!initialLoadDone || isDebounced)
            {
                initialLoadDone = true;
                fullLoadTimer.Start();
            }
        }

        private async void PerfomInitialLoad()
        {
            isLoading = true;
            try
            {
                var members = await System.Threading.Tasks.Task.Run(() =>
                    memberRepo.broadSearch(searchString: searchField.Text.Trim(), limit: 100, excludeInactive: excludeInactive).ToList()
                );
                UpdateDGVData(members);
            } catch(Exception ex)
            {
                MessageBox.Show($"Fejl ved indlæsning af data: {ex.Message}");
            }
            finally { isLoading = false; }
        }

        private async void LoadFullMemberData()
        {
            fullLoadTimer.Stop();

            if (dgv.IsCurrentCellInEditMode)
            {
                fullLoadTimer.Start();
                return;
            }

            isLoading = true;
            try
            {
                int currentCount = source.Count;

                var newMembers = await System.Threading.Tasks.Task.Run(() =>
                    memberRepo.broadSearch(searchString: searchField.Text.Trim(), limit: 99999, offset: currentCount, excludeInactive: excludeInactive).ToList()
                );
                
                if (newMembers.Count > 0)
                {
                    AppendDGVData(newMembers);
                }

            } catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved fuld indlæsning: {ex.Message}");
            } finally
            {
                isLoading = false;
            }
        }


        private void AppendDGVData(List<Member> members)
        {
            var newData = members.Select(m => new MemberViewModel
            {
                MemberID = m.MemberID,
                FirstName = m.FirstName,
                LastName = m.LastName,
                DateOfBirth = m.DateOfBirth,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber,
                MemberType = m.MemberType,
                Active = m.Active,
                IsBookedOnSession = editingSessionID.HasValue && cachedSessionMemberIDs.Contains(m.MemberID ?? -1)
            }).ToList();


            if (source.DataSource is IList<MemberViewModel> currentList)
            {
                foreach (var item in newData)
                {
                    currentList.Add(item);
                }

                source.ResetBindings(false);
            }
            else if (source.DataSource != null) //Fallback i tilfælde af fejl til konvertering til IList
            {
                var list = source.DataSource as dynamic;
                foreach (var item in newData)
                {
                    list.Add(item);
                }
            }
        }



        private void UpdateDGVData(List<Member> members)
        {
            var data = members.Select(m => new MemberViewModel
            {
                MemberID = m.MemberID,
                FirstName = m.FirstName,
                LastName = m.LastName,
                DateOfBirth = m.DateOfBirth,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber,
                MemberType = m.MemberType,
                Active = m.Active,

                IsBookedOnSession = editingSessionID.HasValue && cachedSessionMemberIDs.Contains(m.MemberID ?? -1)
            }).ToList();

            DataGridHelper.LoadData(dgv, ref source, data);

            ApplyColumnStyles();

        }



        private void Dgv_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (isLoading || e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgv.Columns[e.ColumnIndex].Name == IsSessionMemberColoumnName && isReadOnly && editingSessionID.HasValue)
            {
                try
                {
                    dgv.CommitEdit(DataGridViewDataErrorContexts.Commit); // tvinger opdatering
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    bool isChecked = Convert.ToBoolean(cell.Value);

                    if (dgv.Rows[e.RowIndex].Cells["MemberID"].Value is int memberID)
                    {
                        int sessionID = editingSessionID.Value;

                        if (isChecked)
                        {
                            memberGroupRepo.Create(new MemberGroup { SessionID = sessionID, MemberID = memberID });
                            cachedSessionMemberIDs.Add(memberID);
                        }
                        else
                        {
                            memberGroupRepo.DeleteGroup(memberID, sessionID);
                            cachedSessionMemberIDs.Remove(memberID);
                        }
                    }

                } catch (Exception ex)
                {
                    MessageBox.Show($"Fejl ved opdatering af holdtilmelding: {ex.Message}");
                    LoadMemberData();
                }
            }
        }


        private void MemberListView_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (isLoading || e.RowIndex < 0 || isReadOnly) return;
            if (dgv.Columns[e.ColumnIndex].Name == IsSessionMemberColoumnName) return;

            SaveRow(e.RowIndex);
        }

        private void SaveRow(int rowIndex)
        {
            if (isReadOnly) return;

            var row = dgv.Rows[rowIndex];
            if (row.IsNewRow) return;

            try
            {
                if (row.DataBoundItem is MemberViewModel vm)
                {
                    var member = new Member
                    {
                        MemberID = vm.MemberID,
                        FirstName = vm.FirstName ?? "",
                        LastName = vm.LastName ?? "",
                        DateOfBirth = vm.DateOfBirth,
                        Email = vm.Email,
                        PhoneNumber = vm.PhoneNumber,
                        MemberType = vm.MemberType,
                        Active = vm.Active
                    };

                    memberRepo.Update(member);
                }
            } catch(Exception ex)
            {
                MessageBox.Show($"Fejl ved opdatering: {ex.Message}");
                LoadMemberData();
            }
        }

        private void MemberListView_UserDeletingRow(object? sender, DataGridViewRowCancelEventArgs e)
        {
            if (isReadOnly)
            {
                e.Cancel = true;
                return;
            }

            DialogResult result = MessageBox.Show("Er du sikker på at slette dette medlem Permanent?", "Slet Medlem", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            try
            {
                int id = Convert.ToInt32(e.Row.Cells["MemberID"].Value);
                memberRepo.Delete(id);
            } catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved sletning: {ex.Message}");
                e.Cancel = true;
            }
        }

        private void Dgv_SelectionChanged(object? sender, EventArgs e)
        {
            if (isLoading || dgv.CurrentRow == null) return;

            if (dgv.CurrentRow.Cells["MemberID"].Value is int memberID)
            {
                SelectedMemberID = memberID;
            }
            else
            {
                SelectedMemberID = null;
            }
            SelectedMemberChanged?.Invoke(this, EventArgs.Empty);
        }

        private void MemberListView_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || isReadOnly)
            {
                dtp.Visible = false;
                return;
            }

            if (dgv.Columns[e.ColumnIndex].Name == "DateOfBirth")
            {
                rectangle = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                dtp.Size = new Size(rectangle.Width, rectangle.Height);
                dtp.Location = new Point(rectangle.X, rectangle.Y);
                dtp.Visible = true;

                object cellValue = dgv.CurrentCell.Value;
                dtp.Value = (cellValue == DBNull.Value || cellValue == null) ? DateTime.Today : Convert.ToDateTime(cellValue);
            }
            else
            {
                dtp.Visible = false;
            }
        }

        private void dtp_ValueChanged_Handler(object? sender, EventArgs e)
        {
            dgv.CurrentCell.Value = dtp.Value.Date;
            dtp.Visible = false;
        }

        private void dtp_Scroll(object? sender, EventArgs e)
        {
            dtp.Visible = false;
        }

        private void Dgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show($"DataError i kolonne {e.ColumnIndex}, række {e.RowIndex}: {e.Exception.Message}");
            e.Cancel = false;
        }

    }
}
