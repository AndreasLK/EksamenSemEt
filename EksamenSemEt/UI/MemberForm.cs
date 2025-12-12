using DatabaseAccessSem1.Repository;
using DatabaseAccessSem1;
using System.Text.RegularExpressions;
using Sem1BackupForms.Forms;
using EksamenSemEt.UI;
namespace Sem1BackupForms
{
    public partial class MemberForm : UserControl
    {
        private MemberRepository repo;
        private DataGridView dgv;
        private BindingSource bindingSource;

        private DateTimePicker dtp;
        private Rectangle _rectangle;
        public MemberForm(MemberRepository memberRepo)
        {
            repo = memberRepo;
            InitializeComponent();
            InitializeDataGridView();

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

            LoadData(repo.GetAll().ToList());

            var idColumn = dgv.Columns["MemberID"];
            idColumn.ReadOnly = true;
            idColumn.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            idColumn.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.LightGray;
            idColumn.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;


            dgv.Controls.Add(dtp);
            dtp.Visible = false;
            dtp.Format = DateTimePickerFormat.Short;

            dtp.TextChanged += new EventHandler(dtp_TextChange);
        }

        private void LoadData(List<Member> members)
        {
            var sortableList = new SortableBindingList<Member>(members);

            bindingSource = new BindingSource();
            bindingSource.DataSource = sortableList;
            dgv.DataSource = bindingSource;


        }

        private void MainForm_Load(object sender, EventArgs e)
        {

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

                repo.Delete(id);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved sletning af medlem: " + ex.Message, "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;

            }
        }

        private void memberListView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var row = memberListView.Rows[e.RowIndex];

            if (row.IsNewRow) return;

            try
            {
                int id = Convert.ToInt32(row.Cells["MemberID"].Value);


                var updateMember = new Member
                {
                    MemberID = id,
                    FirstName = row.Cells["FirstName"].Value?.ToString() ?? "", //hvis null så tom string
                    LastName = row.Cells["LastName"].Value?.ToString() ?? "",
                    DateOfBirth = row.Cells["DateOfBirth"].Value as DateTime?,
                    Email = row.Cells["Email"].Value?.ToString(),
                    PhoneNumber = row.Cells["PhoneNumber"].Value != null ? Convert.ToInt32(row.Cells["PhoneNumber"].Value) : null,
                    MemberType = Convert.ToInt32(row.Cells["MemberType"].Value),
                    Active = Convert.ToBoolean(row.Cells["Active"].Value)
                };

                repo.Update(updateMember);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved opdatering af medlem: " + ex.Message, "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData(repo.GetAll().ToList());
            }
        }

        private void memberListView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && memberListView.Columns[e.ColumnIndex].Name == "DateOfBirth")
            {
                _rectangle = memberListView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                dtp.Size = new Size(_rectangle.Width, _rectangle.Height);
                dtp.Location = new Point(_rectangle.X, _rectangle.Y);
                dtp.Visible = true;
                if (memberListView.CurrentCell.Value != DBNull.Value && memberListView.CurrentCell.Value != DBNull.Value)
                {
                    dtp.Value = Convert.ToDateTime(memberListView.CurrentCell.Value);
                }
                else
                {
                    dtp.Value = DateTime.Today;
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
                    FirstName = row.Cells["FirstName"].Value?.ToString() ?? "", //hvis null så tom string
                    LastName = row.Cells["LastName"].Value?.ToString() ?? "",
                    DateOfBirth = row.Cells["DateOfBirth"].Value as DateTime?,
                    Email = row.Cells["Email"].Value?.ToString(),
                    PhoneNumber = row.Cells["PhoneNumber"].Value != null ? Convert.ToInt32(row.Cells["PhoneNumber"].Value) : null,
                    MemberType = Convert.ToInt32(row.Cells["MemberType"].Value),
                    Active = Convert.ToBoolean(row.Cells["Active"].Value)
                };

                repo.Update(member);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fejl ved opdatering af medlem: " + ex.Message, "Fejl", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData(repo.GetAll().ToList());
            }
        }


        private void dtp_ValueChanged(object? sender, EventArgs e)
        {
            SaveRow(memberListView.CurrentCell.RowIndex);

            dtp.Visible = false;
        }


        private void dtp_TextChange(object? sender, EventArgs e)
        {
            memberListView.CurrentCell.Value = dtp.Value.Date;
        }

        private void dtp_Scroll(object? sender, EventArgs e)
        {
            dtp.Visible = false;
        }

        private void AddMemberButton_Click(object sender, EventArgs e)
        {

        }
    }
}
