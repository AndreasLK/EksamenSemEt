using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EksamenSemEt.UI
{
    public partial class sqlConnectionInput : UserControl
    {
        public string NewConnectionString => textBox1.Text;
        public sqlConnectionInput(string currentString, string errorMessage)
        {
            InitializeComponent();
            textBox1.Text = currentString;

            label1.Text = $"Kunne ikke tilslutte database!\nFejl: {errorMessage}\n\nOpdater ConnectionString";
            label1.ForeColor = Color.Red;

            button1.Click += (s, e) =>
            {
                if (this.ParentForm != null)
                {
                    this.ParentForm.DialogResult = DialogResult.OK;
                    this.ParentForm.Close();
                }
            };
        }
    }
}
