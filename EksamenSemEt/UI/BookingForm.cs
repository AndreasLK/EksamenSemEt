using DatabaseAccessSem1;
using DatabaseAccessSem1.Repository;
using EksamenSemEt.DatabaseAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EksamenSemEt.UI
{
    public partial class BookingForm : UserControl
    {
        private readonly MemberRepository memberRepo;
        private readonly MemberTypeRepository memberTypeRepo;
        private readonly SessionRepository sessionRepo;
        private readonly MemberGroupRepository memberGroupRepo;
        private readonly LocationRepository locationRepo;
        private readonly CertificationRepository certificationRepo;

        private BindingSource sessionBindingSource = new BindingSource();
        private BindingSource bookingBindingSource = new BindingSource();
        private bool isInitializing = true;

        public BookingForm(
            MemberRepository memberRepository,
            MemberTypeRepository memberTypeRepository,
            SessionRepository sessionRepository,
            MemberGroupRepository memberGroupRepository,
            LocationRepository locationRepository,
            CertificationRepository certificationRepository
            )
        {
            this.memberRepo = memberRepository;
            this.memberTypeRepo = memberTypeRepository;
            this.sessionRepo = sessionRepository;
            this.memberGroupRepo = memberGroupRepository;
            this.locationRepo = locationRepository;
            this.certificationRepo = certificationRepository;

            InitializeComponent();

            if (memberSearch1 != null)
            {
                memberSearch1.Configure(memberRepo, memberTypeRepo, isReadOnly: true);
            }

            SetupSessionGrid();
            InitializeSearchFilters();
            WireUpSearchEvents();

            isInitializing = false;
            LoadSearchData();
            CreateBookingButton.Click += CreateBooking_Click;
        }

        private void SetupSessionGrid()
        {
            SessionListView.ReadOnly = true;
            SessionListView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            SessionListView.MultiSelect = false;
            SessionListView.AllowUserToAddRows = false;
            SessionListView.AllowUserToDeleteRows = false;
            SessionListView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void InitializeSearchFilters()
        {
            SessionTypeComboBox.DisplayMember = "Name";
            SessionTypeComboBox.ValueMember = "CertificationID";
            SessionTypeComboBox.DataSource = certificationRepo.GetAll().OrderBy(c => c.Name).ToList();
            SessionTypeComboBox.SelectedIndex = -1;

            SessionLocationComboBox.DisplayMember = "Name";
            SessionLocationComboBox.ValueMember = "LocationID";
            SessionLocationComboBox.DataSource = locationRepo.GetAll().OrderBy(l => l.Name).ToList();
            SessionLocationComboBox.SelectedIndex = -1;

            SessionMinDatePicker.Value = DateTime.Today;
            SessionMaxDatePicker.Value = DateTime.Today.AddDays(14);
        }

        private void WireUpSearchEvents()
        {
            SessionTypeComboBox.SelectedIndexChanged += OnSearchCriteriaChanged;
            //add more


        }

        private void OnSearchCriteriaChanged(object? sender, EventArgs e)
        {
            if (isInitializing)
            {
                LoadSearchData();
            }
        }

        private void LoadSearchData()
        {
            try
            {
                var sessions = sessionRepo.Search(
                    sessionType: (SessionTypeComboBox.SelectedValue is int typeID && typeID > 0) ? typeID : null,
                    dateTimeStart: SessionMinDatePicker.Value.Date,
                    dateTimeEnd: SessionMaxDatePicker.Value.Date,
                    maxMembers: MaxMembersUpDown.Value > 0 ? (int)MaxMembersUpDown.Value : null,
                    minMembers: MinMembersUpDown.Value > 0 ? (int)MinMembersUpDown.Value : null,
                    locationID: (SessionLocationComboBox.SelectedValue is int locID && locID > 0) ? locID : null,
                    minSlots: AvailableSlotsUpDown.Value > 0 ? (int)AvailableSlotsUpDown.Value : null
                );

                var locationDict = locationRepo.GetAll().ToDictionary(l => l.LocationID, l => l.Name);
                var typeDict = certificationRepo.GetAll().ToDictionary(c => c.CertificationID, c => c.Name);

                var displayList = sessions.Select(s =>
                {
                    int id = s.SessionID.GetValueOrDefault();
                    int currentCount = sessionRepo.GetMemberCount(id);

                    // Find Name in List
                    string locName = "Ukendt";
                    if (s.LocationID.HasValue && locationDict.ContainsKey(s.LocationID.Value))
                    {
                        locName = locationDict[s.LocationID.Value];
                    }
                    string typeName = "Ukendt";
                    if (typeDict.ContainsKey(s.SessionType))
                    {
                        typeName = typeDict[s.SessionType];
                    }

                    return new SessionViewModel
                    {
                        SessionID = id,
                        MaxMembers = s.MaxMembers,
                        BookedCount = currentCount,

                        Date = s.DateTime.ToShortDateString(),
                        StartTime = s.DateTime.ToString("HH:mm"),
                        Duration = (s.SessionDuration / 60) + " min",

                        Type = typeName,
                        Location = locName,

                        Availability = $"{currentCount} / {s.MaxMembers}"
                    };
                });

                DataGridHelper.LoadData(SessionListView, ref sessionBindingSource, displayList);
                if (SessionListView.Columns["SessionID"] != null) SessionListView.Columns["SessionID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Fejl ved søgning: {ex.Message}");
            }
        }


        private void CreateBooking_Click(object? sender, EventArgs e)
        {
            if (SessionListView.CurrentRow == null)
            {
                MessageBox.Show("Vælg venligst et hold.");
                return;
            }

            var selectedView = SessionListView.CurrentRow.DataBoundItem as SessionViewModel;

            if (selectedView.BookedCount >= selectedView.MaxMembers)
            {
                MessageBox.Show($"Holdet er fyldt! ({selectedView.Availability})");
                return;
            }

            int? memberID = memberSearch1.GetSelectedMemberID();
            if (memberID == null)
            {
                MessageBox.Show("Vælg venligst et medlem.");
                return;
            }


            try
            {
                List<int> existingMembers = memberGroupRepo.GetMembersIDs(selectedView.SessionID).ToList();

                if (existingMembers.Contains(memberID.Value))
                {
                    MessageBox.Show("Dette medlem er allerede på holdet.");
                    return;
                }

                memberGroupRepo.Create(new MemberGroup
                {
                    SessionID = selectedView.SessionID,
                    MemberID = memberID.Value
                });

                DataGridHelper.ShowSuccess("Booking oprettet");
                
                int newCount = sessionRepo.GetMemberCount(selectedView.SessionID);
                selectedView.BookedCount = newCount;
                selectedView.Availability = $"{newCount} / {selectedView.MaxMembers}";

                sessionBindingSource.ResetCurrentItem();


            }
            catch (Exception ex) {
                MessageBox.Show($"Fejl ved booking: {ex.Message}");
            }

        }
    }
}
