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

        public MemberForm(MemberRepository memberRepo)
        {
            repo = memberRepo;
            InitializeComponent();
            InitializeDataGridView();

        }


        private void InitializeDataGridView()
        {



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


            if (result == DialogResult.No) { e.Cancel = true;}
        }


    }
}
