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

        public MainForm()
        {
            inputProvider = new InputProvider(false);
            inputProvider.Initialize();
            if (inputProvider.Camera == null)
                return;

            inputProvider.Start();

            InitializeComponent();

            SetUpCameras();
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

            inputProvider.Stop();

            this.inputProvider.OnCameraFrame -= OnCameraFrame;
            this.inputProvider.OnProcessedFrame -= OnProcessedFrame;
            this.inputProvider.OnProcessedCameraFrame -= OnProcessedCameraFrame;
        }

        //Load
        void SetUpCameras()
        {
            foreach (FilterInfo fi in CameraManager.GetCameras())
            {
                cbCamera.Items.Add(fi.Name);
            }

            cbCamera.SelectedIndex = Global.AppSettings.Camera;

            nudSkipFrames.Value = Global.AppSettings.SkipFrames;

            chkbBlobFilter.Checked = Global.AppSettings.FilterBlobs;
            tbMinBlobSize.Text = Global.AppSettings.MinBlobSize.ToString();
            tbMaxBlobSize.Text = Global.AppSettings.MaxBlobSize.ToString();
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
            Image old = pbNewCameraFrame.Image;

            pbNewCameraFrame.Image = image;

            if (old != null)
                old.Dispose();
        }
        public void OnProcessedFrame(Bitmap image)
        {
            Image old = pbProcessedFrame.Image;

            pbProcessedFrame.Image = image;

            if (old != null)
                old.Dispose();
        }
        public void OnProcessedCameraFrame(Bitmap image)
        {
            Image old = pbProcessedCameraFrame.Image; 
            
            pbProcessedCameraFrame.Image = image;

            if (old != null)
                old.Dispose();
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
            Calibration c = new Calibration(inputProvider);
            c.ShowDialog();
        }
        void cbCamera_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Global.AppSettings.Camera != cbCamera.SelectedIndex)
            {
                Global.AppSettings.CameraMode = 0;
                Global.AppSettings.Camera = cbCamera.SelectedIndex;

                inputProvider.Stop();
                inputProvider.Initialize(false);
                inputProvider.Start();
            }

            cbCameraMode.Items.Clear();

            foreach (VideoCapabilities vc in inputProvider.Camera.VideoCapabilities)
            {
                cbCameraMode.Items.Add(vc.FrameSize.Width + " x " + vc.FrameSize.Height + " @ " + vc.MaxFrameRate + "Hz");
            }

            if (Global.AppSettings.CameraMode > cbCameraMode.Items.Count)
                Global.AppSettings.CameraMode = 0;

            cbCameraMode.SelectedIndex = Global.AppSettings.CameraMode;
        }
        void cbCameraMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Global.AppSettings.CameraMode != cbCameraMode.SelectedIndex)
            {
                Global.AppSettings.CameraMode = cbCameraMode.SelectedIndex;

                nudSkipFrames.Maximum = inputProvider.Camera.VideoCapabilities[cbCameraMode.SelectedIndex].MaxFrameRate;

                inputProvider.Stop();
                inputProvider.Initialize(false);
                inputProvider.Start();
            }
        }

        void nudSkipFrames_ValueChanged(object sender, EventArgs e)
        {
            Global.AppSettings.SkipFrames = (int)nudSkipFrames.Value;
        }

        void btnSave_Click(object sender, EventArgs e)
        {
            if (cbDetector.Text == "" || cbDetector.Text == "Nieuwe detector...")
            {
                MessageBox.Show("Niet opgeslagen, geef deze detector een naam.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            string s = rtbScript.Text;

            try
            {
                DetectorManager.SaveScriptText(cbDetector.Text, s);
            }
            catch (IOException exc)
            {
                if (MessageBox.Show("Kon detector niet opslaan. Error:\r\n\r\n" + exc.ToString(), "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                    btnSave_Click(sender, e);

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
        void btnDelete_Click(object sender, EventArgs e)
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

            rtbScript.Text = "";
        }
        void btnAforgeHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = Application.StartupPath + @"\AForge.NET.chm";
            p.Start();
        }
        void cbDetector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDetector.Text == "Nieuwe detector...")
            {
                rtbScript.Text = DetectorManager.GetDefaultScript();
                return;
            }

            rtbScript.Text = DetectorManager.GetScript(cbDetector.Text);

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

        void tbBlobSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            if(e.KeyChar != '\b')
                e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }
        void tbMinBlobSize_TextChanged(object sender, EventArgs e)
        {
            int a;
            int.TryParse(tbMinBlobSize.Text, out a);
            Global.AppSettings.MinBlobSize = a;

            if (inputProvider != null && inputProvider.Detector != null)
                inputProvider.Detector.BlobCounter.MinWidth =
                inputProvider.Detector.BlobCounter.MinHeight = Global.AppSettings.MaxBlobSize;
        }
        void tbMaxBlobSize_TextChanged(object sender, EventArgs e)
        {
            int a;
            int.TryParse(tbMaxBlobSize.Text, out a);
            Global.AppSettings.MaxBlobSize = a;

            if (inputProvider != null && inputProvider.Detector != null)
                inputProvider.Detector.BlobCounter.MaxWidth =
                inputProvider.Detector.BlobCounter.MaxHeight = Global.AppSettings.MaxBlobSize;
        }
        void chkbBlobFilter_CheckedChanged(object sender, EventArgs e)
        {
            Global.AppSettings.FilterBlobs = chkbBlobFilter.Checked;

            if(inputProvider != null && inputProvider.Detector != null)
                inputProvider.Detector.BlobCounter.FilterBlobs = Global.AppSettings.FilterBlobs;
        }
    }
}
