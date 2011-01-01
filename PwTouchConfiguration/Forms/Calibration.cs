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
        const int SecondsToHoldCalibrationPoint = 5;

        InputProvider inputProvider;

        List<CalibrationPoint> calibrationPoints;

        bool calibrating;
        int currentCalibrationPointIndex;

        Blob    closestBlob;
        float   closestX;
        int     closestXDivider;
        float   closestY;
        int     closestYDivider;



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
            if (!calibrating)
            {
                if (e.KeyCode == Keys.G)
                    StartCalibration();
            }

            if (e.KeyCode == Keys.Escape)
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            else if (e.KeyCode == Keys.B)
                inputProvider.RestartDetector();
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


        CalibrationPoint GetCurrentCalibrationPoint()
        {
            return calibrationPoints[currentCalibrationPointIndex];
        }
        List<CalibrationPoint> GetNewCalibrationPoints()
        {
            const float margin = 0.1f;

            return new List<CalibrationPoint>()
            {
                new CalibrationPoint(margin, margin), //top-left
                new CalibrationPoint(1 - margin, margin), //top-right
                new CalibrationPoint(margin, 1 - margin), //bottom-left
                new CalibrationPoint(1 - margin, 1 - margin) //bottom-right
            };
        }


        /// <summary> Get blob center relative to webcam image width/height.</summary>
        PointF GetRelativeBlobPosition(Blob b)
        {
            return GetRelativeBlobPosition(b.Center.X, b.Center.Y);
        }
        /// <summary> Get coords relative to webcam image width/height. </summary>
        PointF GetRelativeBlobPosition(int x, int y)
        {
            return new PointF((float)x / (float)inputProvider.Camera.DesiredFrameSize.Width, (float)y / (float)inputProvider.Camera.DesiredFrameSize.Height);
        }

        PointF GetAbsoluteCalibrationPointScreen(CalibrationPoint cp)
        {
            return new PointF(cp.ScreenX * Size.Width, cp.ScreenY * Size.Height);
        }
        PointF GetAbsoluteCalibrationPointWebcam(CalibrationPoint cp)
        {
            return new PointF(cp.WebcamX * inputProvider.Camera.DesiredFrameSize.Width, cp.WebcamY * inputProvider.Camera.DesiredFrameSize.Height);
        }

        /// <summary> Get distance from blob center to calibrationpoint. </summary>
        PointF GetBlobDistance(Blob b, CalibrationPoint cp)
        {
            if (b == null || cp == null)
                throw new ArgumentNullException();

            //Since screen and webcam resolution don't match, use relative distances
            PointF relWebcam = GetRelativeBlobPosition(b);
            
            return new PointF(Math.Abs(cp.ScreenX - relWebcam.X), Math.Abs(cp.ScreenY - relWebcam.Y));
        }
        /// <summary> Get the total relative distance (sum of X and Y). This may be a good indication of how close the blob is to the cp.</summary>
        float GetBlobDistanceIndicator(Blob b, CalibrationPoint cp)
        {
            if (b == null || cp == null)
                throw new ArgumentNullException();

            return GetBlobDistance(b, cp).X + GetBlobDistance(b, cp).Y;
        }


        void StartCalibration()
        {
            calibrating = true;

            Invalidate();
        }
        void StopCalibration()
        {
            calibrating = false;

            Invalidate();
        }
        void CalibrateNext()
        {
            currentCalibrationPointIndex++;

            closestBlob = null;
            closestX = 0;
            closestXDivider = 1;
            closestY = 0;
            closestYDivider = 1;

            if (currentCalibrationPointIndex >= calibrationPoints.Count)
                StopCalibration();
        }

        void OnBlobsTracked(List<Blob> blobs)
        {
            if (!calibrating)
                return;

            CalibrationPoint cp = GetCurrentCalibrationPoint();

            Blob closest = closestBlob;
            foreach (Blob b in blobs)
            {
                if (!b.Active)
                {
                    if (b == closest)
                        closest = null;

                    continue;
                }

                if (closest == null)
                {
                    closest = b;
                }
                else
                {
                    if (GetBlobDistanceIndicator(b, cp) < GetBlobDistanceIndicator(closest, cp))
                        closest = b;
                }

                //If distance from blob to calibration point is greater than 20% of camera frame width/height, 
                //this blob isn't a good calibration point.
                PointF blobDistance = GetBlobDistance(closest, cp);
                if (blobDistance.X > 0.20f || blobDistance.Y > 0.20f)
                {
                    closest = null;
                }
            }

            if (closest == null)
            {
                closestBlob = null;

                Invalidate();

                return;
            }

            if (closest != closestBlob)
            {
#if DEBUG
                closest.LifeTime = inputProvider.Camera.DesiredFrameRate * SecondsToHoldCalibrationPoint / 2; // 'reset' the LifeTime so we can check if it has lived long enough to be a good cp
#else
                closest.LifeTime = 2;
#endif

                closestX = closest.Center.X;
                closestXDivider = 1;
                closestY = closest.Center.Y;
                closestYDivider = 1;

                closestBlob = closest;
            }
            else
            {
                closest.LifeTime++;

                closestX += closest.Center.X;
                closestXDivider++;
                closestY += closest.Center.Y;
                closestYDivider++;
            }

            //Holding calibration blob for 5 seconds makes it pass, we can move to the next cp
            if (closest.LifeTime / inputProvider.Camera.DesiredFrameRate >= SecondsToHoldCalibrationPoint) //LifeTime: 90, FPS: 30, => 3 seconds
            {
                PointF relative = GetRelativeBlobPosition((int)(closestX / closestXDivider), (int)(closestY / closestYDivider));

                cp.WebcamX = relative.X;
                cp.WebcamY = relative.Y;

                CalibrateNext();
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!calibrating)
                return;

            Graphics g = e.Graphics;
            for (int i = 0; i < calibrationPoints.Count; i++)
            {
                if (!calibrationPoints[i].IsSet && i != currentCalibrationPointIndex)
                    continue;

                Pen pen;
                if (calibrationPoints[i].IsSet)
                    pen = Pens.Green;
                else
                    pen = Pens.LightBlue;

                PointF screen = GetAbsoluteCalibrationPointScreen(calibrationPoints[i]);

                g.DrawLine(pen, screen.X - 20, screen.Y, screen.X + 20, screen.Y);
                g.DrawLine(pen, screen.X, screen.Y - 20, screen.X, screen.Y + 20);

                if (i != currentCalibrationPointIndex || closestBlob == null)
                    continue;

                //Draw indicator...
                float progress = ((float)closestBlob.LifeTime / (float)inputProvider.Camera.DesiredFrameRate) / (float)SecondsToHoldCalibrationPoint;

                //Looking at PhotoShop Color picker, making a color go from white to green means H: 120, S: 0% to 100%, B: 100%.
                //In RGB this is R: 255 to 0, G: 255, B: 255 to 0
                Brush brush = new SolidBrush(Color.FromArgb((int)((1 - progress) * 255), 255, (int)((1 - progress) * 255)));

                g.FillRectangle(Brushes.DarkGray, screen.X - 20, screen.Y + 40, 40, 10);
                g.FillRectangle(brush, screen.X - 20, screen.Y + 40, (int)(progress * 40), 10);
            }
        }
    }
}
