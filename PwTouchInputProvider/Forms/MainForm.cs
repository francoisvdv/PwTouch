﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using System.Collections;
using System.IO;
using System.Text;
using System.CodeDom.Compiler;

namespace PwTouchInputProvider.Forms
{
    public partial class MainForm : Form
    {
        TextEditorControl rtbScript;

        InputProvider inputProvider;

        //Filters
        List<Type> filters = new List<Type>();

        Bitmap cameraFrame;
        Bitmap processedFrame;
        Bitmap processedCameraFrame;

        public MainForm(InputProvider provider)
        {
            InitializeComponent();

            this.inputProvider = provider;

            SetUpCameras();
            SetUpDetectors();
            SetUpScriptEditor();
        }

        void MainForm_Load(object sender, EventArgs e)
        {
            this.inputProvider.OnCameraFrame += OnCameraFrame;
            this.inputProvider.OnProcessedFrame += OnProcessedFrame;
            this.inputProvider.OnProcessedCameraFrame += OnProcessedCameraFrame;
        }
        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Global.AppSettings.Save();

            this.inputProvider.OnCameraFrame -= OnCameraFrame;
            this.inputProvider.OnProcessedFrame -= OnProcessedFrame;
            this.inputProvider.OnProcessedCameraFrame -= OnProcessedCameraFrame;
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
        void SetUpScriptEditor()
        {
            rtbScript = new TextEditorControl();
            rtbScript.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighter("C#");
            rtbScript.Dock = DockStyle.Fill;
            panel1.Controls.Add(rtbScript);

            cbDetector.Items.Add("Nieuwe detector...");

            if (Directory.Exists(DetectorManager.DetectorDirectory))
            {
                foreach (string file in Directory.GetFiles(DetectorManager.DetectorDirectory))
                {
                    if (!cbDetector.Items.Contains(Path.GetFileNameWithoutExtension(file)))
                        cbDetector.Items.Add(Path.GetFileNameWithoutExtension(file));
                }

                if (Global.AppSettings.DetectorName != "[default]" && cbDetector.Items.Contains(Global.AppSettings.DetectorName))
                    cbDetector.SelectedIndex = cbDetector.Items.IndexOf(Global.AppSettings.DetectorName);
            }
        }

        public void OnCameraFrame(Bitmap image)
        {
            if (cameraFrame != null)
                cameraFrame.Dispose();

            cameraFrame = image;

            pbNewCameraFrame.Image = image;
        }
        public void OnProcessedFrame(Bitmap image)
        {
            if (processedFrame != null)
                processedFrame.Dispose();

            processedFrame = image;

            pbProcessedFrame.Image = image;
        }
        public void OnProcessedCameraFrame(Bitmap image)
        {
            if (processedCameraFrame != null)
                processedCameraFrame.Dispose();

            processedCameraFrame = image;

            pbProcessedCameraFrame.Image = image;
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

        private void cbSave_Click(object sender, EventArgs e)
        {
            if (cbDetector.Text == "" || cbDetector.Text == "Nieuwe detector...")
            {
                MessageBox.Show("Niet opgeslagen, geef deze detector een naam.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string s = rtbScript.Document.TextContent;

            try
            {
                DetectorManager.SaveScriptText(cbDetector.Text, s);
            }
            catch (IOException exc)
            {
                if (MessageBox.Show("Kon detector niet opslaan. Error:\r\n\r\n" + exc.ToString(), "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                    cbSave_Click(sender, e);

                return;
            }
            
            System.CodeDom.Compiler.CompilerResults cresults = DetectorManager.SaveScriptAssembly(cbDetector.Text, s);
            if (cresults.Errors.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Script opgeslagen, maar er waren compilefouten:");
                foreach (CompilerError error in cresults.Errors)
                {
                    sb.Append("Line: ");
                    sb.Append(error.Line);
                    sb.AppendLine();
                    sb.Append("Description: ");
                    sb.Append(error.ErrorText);
                    sb.AppendLine();
                    sb.AppendLine();
                }
                MessageBox.Show(sb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!cbDetector.Items.Contains(cbDetector.Text))
                cbDetector.Items.Add(cbDetector.Text);

            cbDetector_SelectedIndexChanged(null, null);
        }
        private void cbDelete_Click(object sender, EventArgs e)
        {
            if (cbDetector.Text == "" || cbDetector.Text == "Nieuwe detector...")
                return;
            
            string textFile = DetectorManager.DetectorDirectory + "\\" + cbDetector.Text + ".txt";
            string dllFile = DetectorManager.DetectorDirectory + "\\" + cbDetector.Text + ".dll";

            try
            {
                if (File.Exists(textFile))
                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(textFile, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
                if (File.Exists(dllFile))
                    Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(dllFile, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            }
            catch(Exception exc)
            {
                MessageBox.Show("Kon Detector niet verwijderen.\r\n\r\n" + exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(cbDetector.Items.Contains(cbDetector.Text))
                cbDetector.Items.RemoveAt(cbDetector.SelectedIndex);

            rtbScript.Document.TextContent = "";
        }
        private void cbDetector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDetector.Text == "Nieuwe detector...")
            {
                rtbScript.Document.TextContent = DetectorManager.GetDefaultScript();
                return;
            }

            rtbScript.Document.TextContent = DetectorManager.GetScript(cbDetector.Text);

            DetectorBase detector = null;
            try
            {
                detector = DetectorManager.LoadDetector(DetectorManager.DetectorDirectory + "\\" + cbDetector.Text + ".dll");
            }
            catch (FileNotFoundException exc)
            {
                MessageBox.Show(exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (FileLoadException exc)
            {
                MessageBox.Show(exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (TypeLoadException exc)
            {
                MessageBox.Show(exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (detector == null)
                return;

            inputProvider.SetDetector(detector);

            Global.AppSettings.DetectorName = cbDetector.Text;
        }
    }
}
