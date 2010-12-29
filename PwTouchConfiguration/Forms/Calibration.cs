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
        InputProvider inputProvider;

        List<CalibrationPoint> calibrationPoints;

        public Calibration(InputProvider ip)
        {
            inputProvider = ip;

            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            Resized();
        }

        void Calibration_Load(object sender, EventArgs e)
        {
            inputProvider.OnProcessedCameraFrame += OnProcessedCameraFrame;
            inputProvider.OnBlobsTracked += OnBlobsTracked;
        }
        void Calibration_FormClosed(object sender, FormClosedEventArgs e)
        {
            inputProvider.OnProcessedCameraFrame -= OnProcessedCameraFrame;
            inputProvider.OnBlobsTracked -= OnBlobsTracked;

            Global.AppSettings.CalibrationPoints = calibrationPoints;
        }
        public void OnProcessedCameraFrame(Bitmap image)
        {
            if (pbProcessedCameraFrame.Image != null)
                pbProcessedCameraFrame.Image.Dispose();

            pbProcessedCameraFrame.Image = image;
        }

        void Calibration_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.G)
                Console.WriteLine("Start calibration!");
            else if (e.KeyCode == Keys.B)
                inputProvider.RestartDetector();
            else if (e.KeyCode == Keys.Escape)
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
        void Calibration_Resize(object sender, EventArgs e)
        {
            Resized();
        }

        void Resized()
        {
            calibrationPoints = GetNewCalibrationPoints();
            panel.Location = new Point((Size.Width - panel.Size.Width) / 2, (Size.Height - panel.Size.Height) / 2);
        }

        List<CalibrationPoint> GetNewCalibrationPoints()
        {
            const int margin = 50;

            return new List<CalibrationPoint>()
            {
                new CalibrationPoint(margin, margin), //top-left
                new CalibrationPoint(Size.Width - margin, margin), //top-right
                new CalibrationPoint(margin, Size.Height - margin), //bottom-left
                new CalibrationPoint(Size.Width - margin, Size.Height - margin) //bottom-right
            };
        }

        void OnBlobsTracked(List<Blob> blobs)
        {
            //find closest blobs to calibrationpoints, check lifetime > n
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            for (int i = 0; i < calibrationPoints.Count; i++)
            {
                Pen pen;
                if (calibrationPoints[i].IsSet)
                    pen = Pens.Green;
                else
                    pen = Pens.LightBlue;

                g.DrawLine(pen, calibrationPoints[i].ImageX - 20, calibrationPoints[i].ImageY, calibrationPoints[i].ImageX + 20, calibrationPoints[i].ImageY);
                g.DrawLine(pen, calibrationPoints[i].ImageX, calibrationPoints[i].ImageY - 20, calibrationPoints[i].ImageX, calibrationPoints[i].ImageY + 20);
            }
        }
    }
}
