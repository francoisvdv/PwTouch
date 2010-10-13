using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PwTouchInputProvider.Forms;

namespace PwTouchInputProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            InputProvider ip = new InputProvider();
            if (ip.Camera == null)
                return;

            ip.StartCamera();

            Application.Run(new MainForm(ip));

            ip.StopCamera();
        }
    }
}
