using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EksamenSemEt.UI
{
    public partial class SessionTimeEditor : UserControl
    {

        public event EventHandler EditingDone;
        public SessionTimeEditor()
        {
            InitializeComponent();
            SessionDatePicker.KeyDown += Input_KeyDown;
            SessionStartTimePicker.KeyDown += Input_KeyDown;
            SessionEndTimePicker.KeyDown += Input_KeyDown;

        }

        public void SetValues(DateTime startDateTime, int durationSeconds)
        {
            if (startDateTime < SessionDatePicker.MinDate || startDateTime > SessionDatePicker.MaxDate)
            {
                MessageBox.Show(
            $"Ugyldig dato modtaget fra databasen: {startDateTime:dd-MM-yyyy}\n\n" +
            "Datoen er uden for det tilladte interval. Editoren nulstilles til dags dato.",
            "Data Fejl",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
                startDateTime = DateTime.Now;
            }

            SessionDatePicker.Value = startDateTime.Date;
            SessionStartTimePicker.Value = startDateTime;


            DateTime endDateTime;
            try
            {
                endDateTime = startDateTime.AddSeconds(durationSeconds);
            }
            catch
            {
                endDateTime = startDateTime.AddMinutes(45);
            }

            if (endDateTime < SessionEndTimePicker.MinDate || endDateTime > SessionEndTimePicker.MaxDate)
            {
                endDateTime = startDateTime.AddMinutes(45);
            }

            SessionEndTimePicker.Value = endDateTime;
        }

        public int GetNewDuration()
        {
            TimeSpan duration = SessionEndTimePicker.Value.TimeOfDay - SessionStartTimePicker.Value.TimeOfDay;
            if (duration.TotalSeconds <= 0) return 60 * 45; // default til 45 min hvis noget er galt

            return (int)duration.TotalSeconds;
        }

        public DateTime GetNewStart()
        { 
            return SessionDatePicker.Value.Date + SessionStartTimePicker.Value.TimeOfDay;
        }

        
        private void Input_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // slipper man for ding lyd som windows elsker
                EditingDone?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
