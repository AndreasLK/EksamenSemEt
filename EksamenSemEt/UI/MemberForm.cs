using DatabaseAccessSem1.Repository;
using DatabaseAccessSem1;
using System.Text.RegularExpressions;
using Sem1BackupForms.Forms;
using EksamenSemEt.UI;
using EksamenSemEt.DatabaseAccess.Repository;
namespace Sem1BackupForms
{
    public partial class MemberForm : UserControl //Lavet af Rasmus (Design og opsætning) og Andreas
    {
        private MemberRepository memberRepo;
        private MemberTypeRepository memberTypeRepo;
        public event EventHandler<Member> OnHistoryRequested;
        public MemberForm(MemberRepository memberRepo, MemberTypeRepository memberTypeRepo)
        {
            this.memberRepo = memberRepo;
            this.memberTypeRepo = memberTypeRepo;
            InitializeComponent();

            InitializeMemberTypes();

            if (memberSearch1 != null)
            {
                memberSearch1.Configure(memberRepo, memberTypeRepo, isReadOnly: false);

                memberSearch1.MemberDoubleClicked += MemberSearch1_MemberDoubleClicked;
            }

            
        }

        private void MemberSearch1_MemberDoubleClicked(object sender, int memberId)
        {
            var member = memberRepo.GetByID(memberId);

            if (member != null)
            {
                OnHistoryRequested?.Invoke(this, member);
            }
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

                DataGridHelper.ShowSuccess("Medlem tilføjet succesfuldt!");

                if (memberSearch1 != null) memberSearch1.LoadMemberData();

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
    }
}
