using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PwTouchInputProvider.Forms
{
    public partial class Calibration : Form
    {
        Point currentPoint;

        public Calibration()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;


        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            
        }

        private void Calibration_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
