using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.VideoSurveillance;
using PwTouchApp.Detection;

namespace PwTouchApp.Forms
{
    public partial class Form1 : Form
    {
        Detector1 detector;

        bool processing = false;

        public Form1()
        {
            InitializeComponent();

            OpenWebcam();
            StartProcessing();
        }

        void OpenWebcam()
        {
            //try to create the capture
            if (Program.Capture == null)
            {
                try
                {
                    Program.Capture = new Capture();
                }
                catch (NullReferenceException excpt)
                {   //show errors if there is any
                    MessageBox.Show("Openen van webcamstream mislukt.\r\n\r\n" + excpt.Message);
                }
            }
        }
        void StartProcessing()
        {
            if (processing == true)
                return;

            if (Program.Capture != null) //if camera capture has been successfully created
            {
                detector = new Detector1();
                processingTimer.Start();
            }

            btnToggleProcessing.Text = "Stop Processing";

            processing = true;
        }
        void StopProcessing()
        {
            if (processing == false)
                return;
            
            processingTimer.Stop();
            detector = new Detector1();

            btnToggleProcessing.Text = "Start Processing";

            processing = false;
        }

        private void processingTimer_Tick(object sender, EventArgs e)
        {
            //Image<Bgr, Byte> frame = Program.Capture.QueryFrame();
            Image<Bgr, Byte> frame = new Image<Bgr, byte>("test.png");

            detector.DetectBlobs(frame);

            label3.Text = "Gevonden: ";

            foreach (MCvBlob blob in detector.Blobs)
            {
                frame.Draw(Rectangle.Round(blob), new Bgr(255.0, 255.0, 255.0), 2);

                label3.Text += " | (" + Math.Round(blob.Center.X).ToString() + ", " + Math.Round(blob.Center.Y).ToString() + ")";
            }

            capturedImageBox.Image = frame;
            foregroundImageBox.Image = detector.BgFgDetector.ForgroundMask;
        }

        private void btnToggleProcessing_Click(object sender, EventArgs e)
        {
            if (processing == true)
            {
                StopProcessing();
            }
            else
            {
                StartProcessing();
            }
        }
    }
}
