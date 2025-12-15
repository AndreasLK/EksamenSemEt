using DatabaseAccessSem1.Repository;
using DatabaseAccessSem1;
using System.Text.RegularExpressions;
using Sem1BackupForms.Forms;
using EksamenSemEt.UI;
using EksamenSemEt.DatabaseAccess.Repository;
namespace Sem1BackupForms
{
    public partial class MemberForm : UserControl
    {
        private MemberRepository memberRepo;
        private MemberTypeRepository memberTypeRepo;
        private DataGridView dgv;
        private BindingSource bindingSource;

        private bool isLoading = false;

        private DateTimePicker dtp;
        private Rectangle _rectangle;
        public MemberForm(MemberRepository memberRepo, MemberTypeRepository memberTypeRepo)
        {
            this.memberRepo = memberRepo;
            this.memberTypeRepo = memberTypeRepo;
            InitializeComponent();
            InitializeDataGridView();
            InitializeMemberTypes();
        }


        private void InitializeDataGridView()
        {

            dtp = new DateTimePicker();


            dgv = memberListView;
            dgv.Dock = DockStyle.Fill;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;
            dgv.ReadOnly = false;

            tableLayoutPanel3.Controls.Add(dgv);
            DataGridHelper.LoadData(dgv, ref bindingSource, memberRepo.broadSearch(SearchFieldText.Text));

            var types = memberTypeRepo.GetAll().ToList();

            DataGridViewComboBoxColumn memberTypeColumn = new DataGridViewComboBoxColumn();
            memberTypeColumn.HeaderText = "Medlemstype";
            memberTypeColumn.Name = "MemberType";
            memberTypeColumn.DataSource = types;
            memberTypeColumn.DisplayMember = "Name";
            memberTypeColumn.ValueMember = "MemberTypeID";

            memberTypeColumn.DataPropertyName = "MemberType";

            if (dgv.Columns.Contains("MemberType"))
            {
                dgv.Columns.Remove("MemberType");
            }

            dgv.Columns.Add(memberTypeColumn);


            var idColumn = dgv.Columns["MemberID"];
            idColumn.ReadOnly = true;
            idColumn.DefaultCellStyle.BackColor = Color.LightGray;
            idColumn.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            idColumn.DefaultCellStyle.SelectionForeColor = Color.Black;


            dgv.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Short;

            dtp.ValueChanged += new EventHandler(dtp_ValueChanged_Handler);

            dgv.CellValueChanged += memberListView_CellValueChanged;
            dgv.Scroll += new ScrollEventHandler(dtp_Scroll);

        }

        private void memberListView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (isLoading || e.RowIndex < 0) return;

            var row = memberListView.Rows[e.RowIndex];

            string newFirstName = row.Cells["FirstName"].Value?.ToString() ?? "";
            string newLastName = row.Cells["LastName"].Value?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(newFirstName) || string.IsNullOrWhiteSpace(newLastName))
            {
                DataGridHelper.LoadData(dgv, ref bindingSource, memberRepo.broadSearch(SearchFieldText.Text));
                return;
            }

            try
            {
                int id = Convert.ToInt32(row.Cells["MemberID"].Value);

                var updateMember = new Member
                {
                    MemberID = id,
                    FirstName = newFirstName,
                    LastName = newLastName,
                    DateOfBirth = row.Cells["DateOfBirth"].Value as DateTime?,
                    Email = row.Cells["Email"].Value?.ToString(),
                    PhoneNumber = row.Cells["PhoneNumber"].Value?.ToString(),

                    MemberType = Convert.ToInt32(row.Cells["MemberType"].Value),
                    Active = Convert.ToBoolean(row.Cells["Active"].Value)
                };

                memberRepo.Update(updateMember);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved opdatering: " + ex.Message);
                DataGridHelper.LoadData(dgv, ref bindingSource, memberRepo.broadSearch(SearchFieldText.Text));
            }
        }


        private void dtp_ValueChanged_Handler(object? sender, EventArgs e)
        {
            memberListView.CurrentCell.Value = dtp.Value.Date;

            dtp.Visible = false;
        }
        private void memberListView_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("Er du sikker på at slette dette medlem Permenent? \n NO TAKESIES BACKSIES \n \nEr du i tvivl er svaret nej", "Slet Medlem", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


            if (result == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            //Hvis der er trykket ja
            try
            {
                int id = Convert.ToInt32(e.Row.Cells["MemberID"].Value);

                memberRepo.Delete(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved sletning af medlem: " + ex.Message, "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
        }

        private void memberListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                dtp.Visible = false;
                return;
            }

            if (memberListView.Columns[e.ColumnIndex].Name == "DateOfBirth")
            {
                _rectangle = memberListView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                dtp.Size = new Size(_rectangle.Width, _rectangle.Height);
                dtp.Location = new Point(_rectangle.X, _rectangle.Y);
                dtp.Visible = true;

                object cellValue = memberListView.CurrentCell.Value;

                if (cellValue == DBNull.Value || cellValue == null)
                {
                    dtp.Value = DateTime.Today;
                }
                else
                {
                    dtp.Value = Convert.ToDateTime(cellValue);
                }
            }
            else
            {
                dtp.Visible = false;
            }
        }

        private void SaveRow(int rowIndex)
        {
            var row = memberListView.Rows[rowIndex];
            if (row.IsNewRow) return;
            try
            {
                int id = Convert.ToInt32(row.Cells["MemberID"].Value);
                var member = new Member
                {
                    MemberID = id,
                    FirstName = row.Cells["FirstName"].Value?.ToString(), //hvis null så tom string
                    LastName = row.Cells["LastName"].Value?.ToString() ?? "",
                    DateOfBirth = row.Cells["DateOfBirth"].Value as DateTime?,
                    Email = row.Cells["Email"].Value?.ToString(),
                    PhoneNumber = row.Cells["PhoneNumber"].Value?.ToString() ?? "",
                    MemberType = Convert.ToInt32(row.Cells["MemberType"].Value),
                    Active = Convert.ToBoolean(row.Cells["Active"].Value)
                };

                memberRepo.Update(member);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved opdatering af medlem: " + ex.Message, "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DataGridHelper.LoadData(dgv, ref bindingSource, memberRepo.broadSearch(SearchFieldText.Text).ToList());
            }
        }


        private void dtp_ValueChanged(object? sender, EventArgs e)
        {
            SaveRow(memberListView.CurrentCell.RowIndex);

            dtp.Visible = false;
        }


        private void dtp_Scroll(object? sender, EventArgs e)
        {
            dtp.Visible = false;
        }

        private void AddMemberButton_Click(object sender, EventArgs e)
        {
            try
            {
                string firstName = FirstNameTextBox.Text.Trim();
                string lastName = LastNameTextBox.Text.Trim();
                DateTime dateOfBirth = BirthdaySelector.Value.Date;
                string email = EmailTextBox.Text.Trim();
                string phoneNumberText = PhoneNumberTextBox.Text.Trim();
                int memberType = (int)MemberTypeComboBox.SelectedValue; //Compile som om det var en int
                bool active = ActivityCheckBox.Checked;

                //Check values

                if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                {
                    MessageBox.Show("Fornavn og Efternavn er påkrævet.", "Obligatoriske Felter Mangler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var member = new Member
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth,
                    Email = email,
                    PhoneNumber = phoneNumberText,
                    MemberType = memberType,
                    Active = active

                };

                memberRepo.Create(member);

                DataGridHelper.LoadData(dgv, ref bindingSource, memberRepo.broadSearch(SearchFieldText.Text));

                DataGridHelper.ShowSuccess("Medlem tilføjet succesfuldt!");
                FirstNameTextBox.Clear();
                LastNameTextBox.Clear();
                EmailTextBox.Clear();
                PhoneNumberTextBox.Clear();
                EmailTextBox.Clear();
                ActivityCheckBox.Checked = false;
                BirthdaySelector.Value = DateTime.Today;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved tilføjelse af medlem: " + ex.Message, "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitializeMemberTypes()
        {
            var types = memberTypeRepo.GetAll().ToList();

            MemberTypeComboBox.DataSource = null;
            MemberTypeComboBox.Items.Clear();

            MemberTypeComboBox.DataSource = types;
            MemberTypeComboBox.DisplayMember = "Name";
            MemberTypeComboBox.ValueMember = "MemberTypeID";

            if (types.Count > 0)
            {
                MemberTypeComboBox.SelectedIndex = 0;
            }
        }

        private void SearchFieldText_TextChanged(object sender, EventArgs e)
        {
            DataGridHelper.LoadData(dgv, ref bindingSource, memberRepo.broadSearch(SearchFieldText.Text));
        }
    }
}
