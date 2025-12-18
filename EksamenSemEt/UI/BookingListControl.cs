using DatabaseAccessSem1;
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
    public partial class BookingListControl : UserControl
    {
        private BookingRepository bookingRepo;
        private DataGridView dgv;
        private BindingSource source = new BindingSource();

        private TextBox memberSearchField;
        private TextBox instructorSearchField;
        private ComboBox typeCombo;
        private ComboBox locCombo;
        private DateTimePicker startPicker;
        private DateTimePicker endPicker;



        private bool isLoading = false;
        private bool initialLoadDone = false;

        private readonly Timer fullLoadTimer = new Timer { Interval = 200 };
        private readonly Timer debounceTimer = new Timer { Interval = 300 };

        public event EventHandler BookingSelectionChanged;

        private const int INIT_LIMIT = 100;

        public BookingListControl()
        {
            InitializeComponent();
            dgv = BookingListView;
            dgv.AutoGenerateColumns = false;

            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgv.SelectionChanged += (s, e) => BookingSelectionChanged?.Invoke(this, EventArgs.Empty);

            fullLoadTimer.Tick += (s, e) => FetchData(limit: 99999);

            debounceTimer.Tick += (s, e) =>
            {
                debounceTimer.Stop();
                FetchData(limit: INIT_LIMIT);
            };


            SetupColumns();


        }

        public void Configure(
            BookingRepository bookingRepository,
            TextBox memberSearchBox,
            TextBox instructorSearchBox,
            ComboBox typeCombo = null,
            ComboBox locCombo = null,
            DateTimePicker startPicker = null,
            DateTimePicker endPicker = null
            ){
            this.bookingRepo = bookingRepository;
            this.memberSearchField = memberSearchBox;
            this.instructorSearchField = instructorSearchBox;

            this.typeCombo = typeCombo;
            this.locCombo = locCombo;
            this.startPicker = startPicker;
            this.endPicker = endPicker;




            if (memberSearchField != null) memberSearchField.TextChanged += SearchInputChanged;
            if (instructorSearchField != null) instructorSearchField.TextChanged += SearchInputChanged;

            if (typeCombo != null) typeCombo.SelectedIndexChanged += SearchInputChanged;
            if (locCombo != null) locCombo.SelectedIndexChanged += SearchInputChanged;
            if (startPicker != null) startPicker.ValueChanged += SearchInputChanged;
            if (endPicker != null) endPicker.ValueChanged += SearchInputChanged;
            FetchData(limit: INIT_LIMIT);
        }

        private void SearchInputChanged(object? sender, EventArgs e)
        {
            fullLoadTimer.Stop();
            debounceTimer.Stop();
            debounceTimer.Start();
        }

        public BookingViewModel? GetSelectedBooking()
        {
            if(dgv.CurrentRow?.DataBoundItem is BookingViewModel vm) return vm;
            return null;
        }

        private async void FetchData(int limit, int offset = 0, bool silentError = false)
        {
            fullLoadTimer.Stop();
            if (bookingRepo == null) return;

            isLoading = true;

            try
            {
                string memText = memberSearchField?.Text.Trim() ?? "";
                string insText = instructorSearchField?.Text.Trim() ?? "";

                int? typeID = (typeCombo.SelectedValue is int t && t > 0) ? t : null;
                int? locID = (locCombo.SelectedValue is int l && l > 0) ? l : null;
                DateTime? start = startPicker?.Value;
                DateTime? end = startPicker.Value;

                var bookings = await Task.Run(() =>
                    bookingRepo.AdvancedSearch(
                        memberSearch: memText,
                        instructorSearch: insText,

                        sessionTypeID: typeID,
                        locationID: locID,
                        startDate: start,
                        endDate: end,
                        limit: limit
                    )
                );

                UpdateDGVData(bookings);

                if (limit <= INIT_LIMIT) fullLoadTimer.Start();

            }
            catch (Exception ex)
            {
                if (!silentError)
                {
                    MessageBox.Show($"Fejl ved datahentning: {ex.Message}");
                }
            }
            finally
            {
                isLoading = false;
            }
        }

        public void RefreshData()
        {
            FetchData(limit: INIT_LIMIT);
        }

        private void UpdateDGVData(IEnumerable<BookingViewModel> data)
        {
            int? selectedID = GetSelectedBooking()?.SessionID;
            DataGridHelper.LoadData(dgv, ref source, data);

            if (selectedID.HasValue && dgv.Rows.Count > 0)
            {
                dgv.ClearSelection();
                foreach(DataGridViewRow row in dgv.Rows)
                {
                    if (row.DataBoundItem is BookingViewModel vm && vm.SessionID == selectedID)
                    {
                        row.Selected = true;
                        dgv.FirstDisplayedScrollingRowIndex = row.Index;
                        break;
                    }
                }
            }
        }

        private void SetupColumns()
        {
            dgv.Columns.Clear();

            AddTextColumn("MemberID", "ID");
            AddTextColumn("MemberFirstName", "Fornavn");
            AddTextColumn("MemberLastName", "Efternavn");
            AddTextColumn("MemberPhone", "Tlf");

            AddTextColumn("SessionType", "Hold");
            AddTextColumn("Location", "Lokation");

            var dateCol = AddTextColumn("Date", "Dato");
            dateCol.DefaultCellStyle.Format = "dd/MM/yyyy";

            AddTextColumn("StartTime", "Tid");
            AddTextColumn("Availability", "Pladser");
        }

        private DataGridViewColumn AddTextColumn(string dataProp, string header)
        {
            var col = new DataGridViewTextBoxColumn
            {
                DataPropertyName = dataProp,
                HeaderText = header,
                Name = dataProp,
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            dgv.Columns.Add(col);
            return col;
        }


    }
}
