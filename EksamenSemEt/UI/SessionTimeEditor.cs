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
            SessionDatePicker.Value = startDateTime.Date;
            SessionStartTimePicker.Value = startDateTime.Date;
            SessionEndTimePicker.Value = startDateTime.AddSeconds(durationSeconds);
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
