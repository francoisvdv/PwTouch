using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.Collections;

namespace PwTouchInputProvider.Forms
{
    public partial class MainForm : Form
    {
        InputProvider inputProvider;

        //Filters
        List<Type> filters = new List<Type>();

        public MainForm(InputProvider provider)
        {
            InitializeComponent();
            AddScriptEditor();

            this.inputProvider = provider;

            SetUpCameras();
            SetUpDetectors();
        }
        void AddScriptEditor()
        {
            TextEditorControl t = new TextEditorControl();
            t.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("C#");
            t.Dock = DockStyle.Fill;
            panel1.Controls.Add(t);
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            this.inputProvider.DrawBlobMarkers = true;
            this.inputProvider.OnProcessed += SetFrame;
        }
        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.AppSettings.Save();

            this.inputProvider.DrawBlobMarkers = false;
            this.inputProvider.OnProcessed -= SetFrame;
        }

        //Load controls
        void SetUpCameras()
        {
            foreach (FilterInfo fi in CameraManager.GetCameras())
            {
                cbCamera.Items.Add(fi.Name);
            }

            cbCamera.SelectedIndex = Global.AppSettings.Camera;

            nudSkipFrames.Value = Global.AppSettings.SkipFrames;
        }
        void SetUpDetectors()
        {

        }

        public void SetFrame(ref Bitmap image)
        {
            pictureBox1.Image = image;
        }

        //Configuration controls
        void btnCameraSettings_Click(object sender, EventArgs e)
        {
            if (inputProvider.Camera == null)
                return;

            inputProvider.Camera.DisplayPropertyPage(this.Handle);
        }
        void btnRestartDetector_Click(object sender, EventArgs e)
        {
            inputProvider.RestartDetector();
        }
        void btnCalibrate_Click(object sender, EventArgs e)
        {
            Calibration c = new Calibration();
            c.ShowDialog();
        }
        void cbCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputProvider.StopCamera();
            inputProvider.Camera = CameraManager.GetCamera(cbCamera.SelectedIndex);

            Global.AppSettings.Camera = cbCamera.SelectedIndex;

            cbCameraMode.Items.Clear();

            foreach (VideoCapabilities vc in inputProvider.Camera.VideoCapabilities)
            {
                cbCameraMode.Items.Add(vc.FrameSize.Width + " x " + vc.FrameSize.Height + " @ " + vc.MaxFrameRate + "Hz");
            }

            cbCameraMode.SelectedIndex = Global.AppSettings.CameraMode;

            inputProvider.StartCamera();
        }
        void cbCameraMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputProvider.Camera.Stop();
            inputProvider.Camera.DesiredFrameSize = inputProvider.Camera.VideoCapabilities[cbCameraMode.SelectedIndex].FrameSize;
            inputProvider.Camera.DesiredFrameRate = inputProvider.Camera.VideoCapabilities[cbCameraMode.SelectedIndex].MaxFrameRate;
            inputProvider.Camera.Start();

            Global.AppSettings.CameraMode = cbCameraMode.SelectedIndex;

            nudSkipFrames.Maximum = inputProvider.Camera.VideoCapabilities[cbCameraMode.SelectedIndex].MaxFrameRate;

            inputProvider.RestartDetector();
        }

        private void nudSkipFrames_ValueChanged(object sender, EventArgs e)
        {
            Global.AppSettings.SkipFrames = (int)nudSkipFrames.Value;
        }
    }
}
