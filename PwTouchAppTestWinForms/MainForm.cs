using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PwTouchLib.Forms;

namespace PwTouchAppTestWinForms
{
    public partial class MainForm : Form
    {
        MainControls c;

        public MainForm()
        {
            InitializeComponent();

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            c = new MainControls();
            c.Dock = DockStyle.Fill;
            Controls.Add(c);

            c.Start();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            c.Stop();
        }

    }
}
