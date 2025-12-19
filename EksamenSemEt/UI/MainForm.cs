using DatabaseAccessSem1;
using DatabaseAccessSem1.Repository;
using EksamenSemEt.DatabaseAccess.Repository;
using EksamenSemEt.Services;
using Microsoft.Data.SqlClient;
using Sem1BackupForms;
using Sem1BackupForms.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EksamenSemEt.UI
{
    public partial class MainForm : Form
    {
        private readonly MemberRepository memberRepo;
        private readonly MemberGroupRepository memberGroupRepo;
        private readonly SessionRepository sessionRepo;
        private readonly InstructorRepository instructorRepo;
        private readonly InstructorGroupRepository instructorGroupRepo;
        private readonly MemberTypeRepository memberTypeRepo;
        private readonly CertificationRepository certificateRepo;
        private readonly LocationRepository locationRepo;
        private readonly BookingRepository bookingRepo;
        private readonly ReportingStatistics reportingStatistics;

        private readonly IDbConnectionFactory dbFactory;
        private string sqlConnString = "Server=LAPTOP-DVIHMMRN;Database=EksamenDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public MainForm()
        {
            InitializeComponent();

            /*
            //INIT DB CONNECTION
            string _runningPath = AppDomain.CurrentDomain.BaseDirectory;
            string _projectPath = Path.GetFullPath(Path.Combine(_runningPath, @"..\..\..\"));
            string _dbPath = Path.Combine(_projectPath, "DatabaseAccess", "Data", "EksamenSem1.db"); //Fulde path doneret af Gemini
            string sqliteConnString = $"Data Source={_dbPath}"; //Alt dette er for at sikre der ændres i den rigtige database. Slipper vi for med MSSQL serveren
            */
            ValidateAndConnect();

            dbFactory = new SqlServerConnectionFactory(sqlConnString); 
            
            

            memberRepo = new MemberRepository(dbFactory);
            sessionRepo = new SessionRepository(dbFactory);
            instructorRepo = new InstructorRepository(dbFactory);
            memberGroupRepo = new MemberGroupRepository(dbFactory);
            instructorGroupRepo = new InstructorGroupRepository(dbFactory);
            memberTypeRepo = new MemberTypeRepository(dbFactory);
            certificateRepo = new CertificationRepository(dbFactory);
            locationRepo = new LocationRepository(dbFactory);
            bookingRepo = new BookingRepository(dbFactory);
            reportingStatistics = new ReportingStatistics(memberRepo, memberGroupRepo);


            //INIT SIDEBAR
            SideBar sideBar = new SideBar();
            sideBar.Dock = DockStyle.Fill;
            sideBar.Margin = new Padding(0);

            tableLayoutPanel1.Controls.Add(sideBar, 0, 0);

            sideBar.MemberClicked += (s, e) =>
            {
                var form = new MemberForm(memberRepo, memberTypeRepo);

                form.OnHistoryRequested += (sender, member) =>
                {
                    LoadHistoryForMember(member, form);
                };

                LoadView(form);
            };

            sideBar.InstructorClicked += (s, e) =>
            {
                var form = new InstructorForm(instructorRepo, certificateRepo);

                form.OnHistoryRequested += (sender, instructor) =>
                {
                    LoadHistoryForInstructor(instructor, form);
                };

                LoadView(form);
            };

            sideBar.BookingClicked += (s, e) => LoadView(new BookingForm(memberRepo, memberTypeRepo, sessionRepo, memberGroupRepo, locationRepo, certificateRepo, bookingRepo));
            sideBar.SessionClicked += (s, e) => LoadView(new SessionForm(certificateRepo, locationRepo, instructorRepo, instructorGroupRepo, sessionRepo));
            sideBar.CertificateClicked += (s, e) => LoadView(new CertificateForm(certificateRepo, sessionRepo));
            sideBar.LocationClicked += (s, e) => LoadView(new LocationForm(locationRepo));
            sideBar.ReportClicked += (s, e) => reportingStatistics.GenerateReport();
            LoadView(new BookingForm(memberRepo, memberTypeRepo, sessionRepo, memberGroupRepo, locationRepo, certificateRepo, bookingRepo));
        }

        private void LoadView(UserControl view){
            ContentPanel.Controls.Clear();
            view.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(view);
        }

        private void LoadHistoryForMember(Member member, UserControl returnToView)
        {
            var history = new HistoryControl(sessionRepo, memberRepo, instructorRepo, memberGroupRepo, instructorGroupRepo);
            history.LoadHistory(member);
            history.CloseRequested += (s, e) => LoadView(returnToView);

            LoadView(history);
        }
        private void LoadHistoryForInstructor(Instructor instructor, UserControl returnToView)
        {
            var history = new HistoryControl(sessionRepo, memberRepo, instructorRepo, memberGroupRepo, instructorGroupRepo);
            history.LoadHistory(instructor);
            history.CloseRequested += (s, e) => LoadView(returnToView);

            LoadView(history);
        }

        private void ValidateAndConnect()
        {
            bool isConnected = false;

            while (!isConnected)
            {
                try
                {
                    using (var connection = new SqlConnection(sqlConnString))
                    {
                        connection.Open();
                        isConnected = true;
                    }
                } catch (Exception ex)
                {
                    using (Form dialog = new Form())
                    {
                        var inputControl = new sqlConnectionInput(sqlConnString, ex.Message);
                        inputControl.Dock = DockStyle.Fill;

                        dialog.Text = "Kunne ikke tilslutte database";
                        dialog.Size = new Size(1200, 350);
                        dialog.StartPosition = FormStartPosition.CenterScreen;
                        dialog.FormBorderStyle = FormBorderStyle.FixedDialog;
                        dialog.MaximizeBox = false;
                        dialog.Controls.Add(inputControl);

                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            sqlConnString = inputControl.NewConnectionString.Trim();
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                    }
                }
            }
        }

    }
}
