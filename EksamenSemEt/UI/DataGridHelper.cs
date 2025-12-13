using Sem1BackupForms.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace EksamenSemEt.UI
{
    public static class DataGridHelper
    {
        //ingen init fordi det er en static klasse
        public static void LoadData<T>(DataGridView dgv, ref BindingSource bindingSource,IEnumerable<T> data)
        {
            // 1. Sorterbar liste fra data
            var sortableList = new SortableBindingList<T>(data.ToList());

            if (bindingSource == null)
            {
                bindingSource = new BindingSource();
            }

            // 3. Assign data
            bindingSource.DataSource = sortableList;
            dgv.DataSource = bindingSource;
        }
        public static void ShowSuccess(string message)
        {

            Form toast = new Form();
            toast.FormBorderStyle = FormBorderStyle.None;
            toast.StartPosition = FormStartPosition.CenterScreen;
            toast.Size = new Size(300, 60);
            toast.BackColor = Color.SeaGreen; 
            toast.TopMost = true; // Altid øverst
            toast.ShowInTaskbar = false; // Ikke synlig i taskbar

  
            Label lbl = new Label();
            lbl.Text = message;
            lbl.ForeColor = Color.White;
            lbl.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lbl.Dock = DockStyle.Fill;
            lbl.TextAlign = ContentAlignment.MiddleCenter;
            toast.Controls.Add(lbl);

            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1100; // 1.1 sekunder
            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                toast.Close();
                toast.Dispose();
                timer.Dispose();
            };

            timer.Start();
            toast.Show();
        }
    }
}
