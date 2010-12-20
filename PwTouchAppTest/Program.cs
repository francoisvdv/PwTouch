using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PwTouchInputProvider.Forms;
using System.Threading;

namespace PwTouchInputProvider
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            InputProvider ip = new InputProvider();
            if (ip.Camera == null)
                return;

            ip.Camera.VideoSourceError += new AForge.Video.VideoSourceErrorEventHandler(Camera_VideoSourceError);

            ip.StartCamera();

            //Weird: give camera half a second to really start.
            Thread.Sleep(500);
            
            Application.Run(new MainForm(ip));

            ip.StopCamera();
        }

        static void Camera_VideoSourceError(object sender, AForge.Video.VideoSourceErrorEventArgs eventArgs)
        {
            MessageBox.Show(eventArgs.Description);
        }
    }
}
