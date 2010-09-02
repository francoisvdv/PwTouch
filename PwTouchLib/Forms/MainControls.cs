using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using System.Reflection;

namespace PwTouchLib.Forms
{
    public partial class MainControls : UserControl
    {
        VideoCaptureDevice camera;
        IDetector detector;

        //Filters
        List<Type> filters = new List<Type>();

        public MainControls()
        {
            InitializeComponent();
        }

        void MainControls_Load(object sender, EventArgs e)
        {
            StartCamera();

            SetUpFilters();
        }
        void MainControls_ParentChanged(object sender, EventArgs e)
        {
            StopCamera();
        }
        
        void StartCamera()
        {
            camera = Camera.GetCamera(0);
            camera.NewFrame += new AForge.Video.NewFrameEventHandler(VideoSource_NewFrame);
            camera.Start();
        }
        void StopCamera()
        {
            if (camera == null || !camera.IsRunning)
                return;

            camera.SignalToStop();
            camera = null;
        }

        void SetUpFilters()
        {
            foreach (Type t in Assembly.GetAssembly(typeof(AForge.Imaging.Filters.AdaptiveSmoothing)).GetTypes())
            {
                if ((t.Namespace != null && !t.Namespace.StartsWith("AForge.Imaging.Filters")) ||
                    !t.IsClass || !t.IsPublic || t.IsNested || t.IsAbstract || t.IsCOMObject )
                    continue;

                filters.Add(t);
            }

            foreach (Type t in filters)
            {
                lbFilters.Items.Add(t.ToString());
            }
        }

        void VideoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap frame = (Bitmap)eventArgs.Frame.Clone();

            if (detector == null)
                detector = new Detector1((Bitmap)frame.Clone());

            detector.ProcessFrame(ref frame);
            pictureBox1.Image = frame;
        }

        void btnCameraSettings_Click(object sender, EventArgs e)
        {
            if (camera == null)
                return;

            camera.DisplayPropertyPage(this.Handle);
        }

        private void lbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}