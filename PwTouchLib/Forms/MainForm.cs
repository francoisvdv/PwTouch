using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AForge.Video.DirectShow;

namespace PwTouchLib.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            MainControls c = new MainControls();
            c.Dock = DockStyle.Fill;
            Controls.Add(c);
        }
    }
}
