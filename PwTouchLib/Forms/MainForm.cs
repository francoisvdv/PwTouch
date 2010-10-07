using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using System.Reflection;

namespace PwTouchLib.Forms
{
    public partial class MainForm : Form
    {
        VideoCaptureDevice camera;
        IDetector detector;

        //Filters
        List<Type> filters = new List<Type>();

        bool restartDetector;

        public MainForm()
            : this(null)
        {
        }
        public MainForm(VideoCaptureDevice cam)
        {
            InitializeComponent();

            SetUpFilters();

            if (cam == null)
                this.camera = Camera.GetCamera(Global.AppSettings.CameraID);
            else
                this.camera = cam;

            //camera.DesiredFrameSize = new Size(640, 480);
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            StartCamera();
        }
        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopCamera();
        }

        void SetUpFilters()
        {
            foreach (Type t in Assembly.GetAssembly(typeof(AForge.Imaging.Filters.AdaptiveSmoothing)).GetTypes())
            {
                if ((t.Namespace != null && !t.Namespace.StartsWith("AForge.Imaging.Filters")) ||
                    !t.IsClass || !t.IsPublic || t.IsNested || t.IsAbstract || t.IsCOMObject)
                    continue;

                filters.Add(t);
            }

            foreach (Type t in filters)
            {
                lbFilters.Items.Add(t.ToString());
            }
        }

        void StartCamera()
        {
            if (camera == null)
                return;

            camera.NewFrame += VideoSource_NewFrame;

            if(!camera.IsRunning && Global.DeveloperMode)
                camera.Start();
        }
        void StopCamera()
        {
            if (camera == null || !camera.IsRunning)
                return;

            camera.NewFrame -= VideoSource_NewFrame;

            if(Global.DeveloperMode)
                camera.SignalToStop();
        }

        void VideoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

            if (detector == null || restartDetector)
            {
                detector = new Detector1((Bitmap)frame.Clone());
                restartDetector = false;
            }

            detector.ProcessFrame(ref frame);
            pictureBox1.Image = frame;
        }

        void btnCameraSettings_Click(object sender, EventArgs e)
        {
            if (camera == null)
                return;

            camera.DisplayPropertyPage(this.Handle);
        }
        void btnRestartDetector_Click(object sender, EventArgs e)
        {
            restartDetector = true;
        }
        void btnCalibrate_Click(object sender, EventArgs e)
        {
            Calibration c = new Calibration();
            c.ShowDialog();
        }
    }
}
