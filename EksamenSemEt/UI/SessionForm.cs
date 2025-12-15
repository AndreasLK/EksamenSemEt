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

namespace EksamenSemEt.UI
{
    public partial class SessionForm : UserControl
    {
        private readonly SessionRepository sessionRepo;
        private readonly InstructorRepository instructorRepo;
        private readonly CertificationRepository certRepo;
        private readonly LocationRepository locationRepo;

        public SessionForm(CertificationRepository certificationRepository, LocationRepository locationRepository)
        {
            this.locationRepo = locationRepository;
            this.certRepo = certificationRepository;
            InitializeComponent();
            InitializeSessionType();
            InitializeLocation();



        }

        private void InitializeSessionType() {
            
            var comboBoxes = new List<ComboBox> { SessionTypeComboBox, SessionTypeSearchComboBox };

            foreach (var comboBox in comboBoxes)
            {
                comboBox.DataSource = certRepo.GetAll().ToList();
                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "CertificationID";
            }

        }

        private void InitializeLocation()
        {
            var comboBoxes = new List<ComboBox> { LocationComboBox, LocationSearchComboBox };
            foreach (var comboBox in comboBoxes) {
                comboBox.DataSource = locationRepo.GetAll().ToList();
                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "LocationID";
            }
        
        }


        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);

            this.BeginInvoke(new Action(() =>
            {
                this.ActiveControl = null;

                label1.Focus();
            }));
        }
    }

}
