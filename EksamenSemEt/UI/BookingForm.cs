using DatabaseAccessSem1;
using DatabaseAccessSem1.Repository;
using DatabaseAccessSem1.Services;
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
        private readonly BookingRepository bookingRepo;

        private BookingListControl bookingListControl;

        private readonly BookingService bookingService;

        private BindingSource sessionBindingSource = new BindingSource();
        private BindingSource bookingBindingSource = new BindingSource();
        private bool isInitializing = true;

        public BookingForm(
            MemberRepository memberRepository,
            MemberTypeRepository memberTypeRepository,
            SessionRepository sessionRepository,
            MemberGroupRepository memberGroupRepository,
            LocationRepository locationRepository,
            CertificationRepository certificationRepository,
            BookingRepository bookingRepository
            )
        {
            this.memberRepo = memberRepository;
            this.memberTypeRepo = memberTypeRepository;
            this.sessionRepo = sessionRepository;
            this.memberGroupRepo = memberGroupRepository;
            this.locationRepo = locationRepository;
            this.certificationRepo = certificationRepository;
            this.bookingRepo = bookingRepository;
            this.bookingService = new BookingService(sessionRepo, memberGroupRepo, memberRepo, memberTypeRepo);

            InitializeComponent();

            if (memberSearch1 != null)
            {
                memberSearch1.Configure(memberRepo, memberTypeRepo, isReadOnly: true, excludeInactive: true);
            }

            InitializeSessionTypes();
            InitializeLocations();
            InitializeDatePickers();

            SetupSessionGrid();
            WireUpSearchEvents();

            if (bookingListControl1 != null)
            {
                bookingListControl1.Configure(
                    bookingRepo,
                    memberGroupRepo,
                    MemberSearch,
                    InstructorSearch,
                    BookingSessionTypeComboBox,
                    BookingSessionLocationComboBox,
                    dateTimePicker1,
                    dateTimePicker2,
                    BookingMinMemberUpDown,
                    BookingMaxMemberUpDown,
                    BookingAvailableSlotsUpDown
                    );
            }


            isInitializing = false;
            LoadSearchData();
            CreateBookingButton.Click += CreateBooking_Click;

            if (ResetBookingSearchButton != null)
            {
                ResetBookingSearchButton.Click += ResetBookingSearchButton_Click;
            }

            if (ResetSearchButton != null)
            {
                ResetSearchButton.Click += ResetSearchButton_Click;
            }
        }

        private void InitializeSessionTypes()
        {
            
            var comboBoxes = new List<ComboBox> {
                SessionTypeComboBox,        
                BookingSessionTypeComboBox  
            };

            var types = certificationRepo.GetAll().OrderBy(c => c.Name).ToList();

            foreach (var comboBox in comboBoxes)
            {
                if (comboBox == null) continue; // Safety check

                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "CertificationID";
                comboBox.DataSource = new List<Certificate>(types); 
                comboBox.SelectedIndex = -1;
            }
        }

        private void InitializeLocations()
        {
            var comboBoxes = new List<ComboBox> {
                SessionLocationComboBox,       
                BookingSessionLocationComboBox  
            };

            var locations = locationRepo.GetAll().OrderBy(l => l.Name).ToList();

            foreach (var comboBox in comboBoxes)
            {
                if (comboBox == null) continue;

                comboBox.DisplayMember = "Name";
                comboBox.ValueMember = "LocationID";
                comboBox.DataSource = new List<Location>(locations);
                comboBox.SelectedIndex = -1;
            }
        }

        private void InitializeDatePickers()
        {
            SessionMinDatePicker.Value = DateTime.Today;
            SessionMaxDatePicker.Value = DateTime.Today.AddDays(14);
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

        private void WireUpSearchEvents()
        {
            SessionTypeComboBox.SelectedIndexChanged += OnSearchCriteriaChanged;
            SessionLocationComboBox.SelectedIndexChanged += OnSearchCriteriaChanged;
            SessionMinDatePicker.ValueChanged += OnSearchCriteriaChanged;
            SessionMaxDatePicker.ValueChanged += OnSearchCriteriaChanged;
            MinMembersUpDown.ValueChanged += OnSearchCriteriaChanged;
            MaxMembersUpDown.ValueChanged += OnSearchCriteriaChanged;
            AvailableSlotsUpDown.ValueChanged += OnSearchCriteriaChanged;


        }

        private void OnSearchCriteriaChanged(object? sender, EventArgs e)
        {
            if (isInitializing) return;

            LoadSearchData();
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

                    string locName = (s.LocationID.HasValue && locationDict.ContainsKey(s.LocationID.Value))
                        ? locationDict[s.LocationID.Value] : "Ukendt";

                    string typeName = typeDict.ContainsKey(s.SessionType)
                        ? typeDict[s.SessionType] : "Ukendt";

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
            if (SessionListView.CurrentRow == null) { MessageBox.Show("Vælg venligst et hold."); return; }
            var selectedView = SessionListView.CurrentRow.DataBoundItem as SessionViewModel;

            int? memberID = memberSearch1.GetSelectedMemberID();
            if (memberID == null) { MessageBox.Show("Vælg venligst et medlem."); return; }

            bool success = bookingService.TryBookSession(memberID.Value, selectedView.SessionID);

            if (success)
            {
                int newCount = sessionRepo.GetMemberCount(selectedView.SessionID);
                selectedView.BookedCount = newCount;
                selectedView.Availability = $"{newCount} / {selectedView.MaxMembers}";
                sessionBindingSource.ResetCurrentItem();

                bookingListControl1?.RefreshData();
            }
        }

        private void ResetBookingSearchButton_Click(object sender, EventArgs e)
        {
            MemberSearch.Text = string.Empty;
            InstructorSearch.Text = string.Empty;

            BookingSessionTypeComboBox.SelectedIndex = -1;
            BookingSessionLocationComboBox.SelectedIndex = -1;

            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today.AddDays(14);

            BookingMinMemberUpDown.Value = 0;
            BookingMaxMemberUpDown.Value = 0;
            BookingAvailableSlotsUpDown.Value = 0;
        }

        private void ResetSearchButton_Click(object? sender, EventArgs e)
        {
            isInitializing = true; //For ikke at pinge databasen 

            try
            {
                SessionTypeComboBox.SelectedIndex = -1;
                SessionLocationComboBox.SelectedIndex = -1;

 
                SessionMinDatePicker.Value = DateTime.Today;
                SessionMaxDatePicker.Value = DateTime.Today.AddDays(14);

                MinMembersUpDown.Value = 0;
                MaxMembersUpDown.Value = 0;
                AvailableSlotsUpDown.Value = 0;
            }
            finally
            {
                isInitializing = false;

                LoadSearchData();
            }
        }

    }
}
