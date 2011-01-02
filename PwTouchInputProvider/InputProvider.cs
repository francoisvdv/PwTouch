using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using Multitouch.Contracts;
using PwTouchInputProvider;
using System.Reflection;
using System.IO;

using Image = AForge.Imaging.Image;

namespace PwTouchInputProvider
{
	[AddIn("PwTouch", Publisher = "Francois van der Ven", Description = "Provides input from PwTouch table.", Version = VERSION)]
	[Export(typeof(IProvider))]
	public class InputProvider : IProvider
    {
        #region IProvider
        public event EventHandler<NewFrameEventArgs> NewFrame;

        public bool HasConfiguration { get { return false; } }
        public bool IsRunning { get { return true; } }
        public bool SendEmptyFrames { get { return false; } set { } }
		internal const string VERSION = "1.0.0.0";

        public System.Windows.UIElement GetConfiguration()
        {
            return null;
        }
        public bool SendImageType(ImageType imageType, bool value)
        {
            return false;
        }
        #endregion

        //=================== EVENTS
        public delegate void FrameDelegate(Bitmap frame);
        /// <summary>Returns a copy of the new camera frame. Don't forget to dispose!</summary>
        public FrameDelegate OnCameraFrame;
        /// <summary>Returns a copy of the new processed frame. Don't forget to dispose!</summary>
        public FrameDelegate OnProcessedFrame;
        /// <summary>Returns a copy of the resulting frame. Don't forget to dispose!</summary>
        public FrameDelegate OnProcessedCameraFrame;

        public delegate void BlobsTrackedDelegate(List<Blob> blobs);
        public BlobsTrackedDelegate OnBlobsTracked;


        //=================== FIELDS
        Queue<Contact> contacts = new Queue<Contact>();

        VideoCaptureDevice camera;
        DetectorBase detector;
        TrackerBase tracker;
        DetectorBase restartDetector;
        int skipFrames;
        System.Timers.Timer timer;



        //=================== PROPERTIES
        public VideoCaptureDevice Camera { get { return camera; } set { camera = value; } }
        public DetectorBase Detector { get { return detector; } set { detector = value; } }
        public TrackerBase Tracker { get { return tracker; } set { tracker = value; } }



        public InputProvider()
            : this(true)
        {

        }

        public InputProvider(bool startPipeServer)
        {
            timer = new System.Timers.Timer(2000);
            timer.Elapsed += delegate
            {
                foreach (Process p in Process.GetProcesses())
                {
                    if (p.ProcessName.Contains("PwTouchConfiguration"))
                        return;
                }

                Log.Write("Timer elapsed and configuration process is terminated - InputProvider starting.");
                Initialize();
                Start();
                Log.Write("Started");

                timer.Stop();
            };

            if (startPipeServer)
            {
                PipeServer.OnReceived += delegate(PipeClient.Command cmd)
                {
                    if (cmd == PipeClient.Command.Stop)
                    {
                        Stop();

                        PipeServer.SendConfirmation();
                        Log.Write("Stopped, confirmation sent.");

                        timer.Start();
                    }
                    else if (cmd == PipeClient.Command.Start)
                    {
                        Initialize();
                        Start();

                        PipeServer.SendConfirmation();
                        Log.Write("Started, confirmation sent.");

                        timer.Stop();
                    }
                };
                PipeServer.Start();
            }

            Initialize();
        }

        public void Initialize(bool loadAppSettings = true)
        {
            if (camera != null && camera.IsRunning)
                Stop();

            if(loadAppSettings)
                Global.AppSettings.Load();

            camera = CameraManager.GetCamera(Global.AppSettings.Camera);
            if (camera != null)
            {
                if (Global.AppSettings.CameraMode < camera.VideoCapabilities.Length)
                {
                    camera.DesiredFrameSize = camera.VideoCapabilities[Global.AppSettings.CameraMode].FrameSize;
                    camera.DesiredFrameRate = camera.VideoCapabilities[Global.AppSettings.CameraMode].MaxFrameRate;
                }
            }
            if (camera == null)
            {
                camera = CameraManager.GetCamera(0);
                Global.AppSettings.Camera = 0;
            }
            if (camera == null)
            {
                MessageBox.Show("Er is geen (standaard) camera gevonden. Driver wordt nu afgesloten.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Application.Exit();
            }

            detector = null;
            restartDetector = null;
            tracker = null;

            skipFrames = Global.AppSettings.SkipFrames;
        }

		public void Start()
		{
            if (camera == null)
                return;

            camera.NewFrame += VideoSource_NewFrame;

            if (!camera.IsRunning)
                camera.Start();

            RestartDetector();
		}
		public void Stop()
		{
            if (camera == null || !camera.IsRunning)
                return;

            camera.NewFrame -= VideoSource_NewFrame;

            camera.SignalToStop();
            camera.WaitForStop();
		}

        public void SetDetector(DetectorBase detector)
        {
            restartDetector = detector;
        }
        /// <summary>Detector will be restarted on next frame.</summary>
        public void RestartDetector()
        {
            restartDetector = detector;
        }

        int FindY(CalibrationPoint cp1, CalibrationPoint cp2, int x)
        {
            //y = ax + b
            //b = y - (ax)

            float a = (cp2.WebcamY - cp1.WebcamY) / (cp2.WebcamX - cp1.WebcamX);
            float b = cp1.WebcamY - a * cp1.WebcamX;

            return (int)(a * x + b);
        }
        int FindX(CalibrationPoint cp1, CalibrationPoint cp2, int y)
        {
            //y = ax + b
            //b = y - (ax)

            //x = (y - b) / a

            float a = (cp2.WebcamY - cp1.WebcamY) / (cp2.WebcamX - cp1.WebcamX);
            float b = cp1.WebcamY - a * cp1.WebcamX;

            return (int)((y - b) / a);
        }

        Point WebcamToScreen(Blob b)
        {
            List<CalibrationPoint> calibrationPoints = Global.AppSettings.CalibrationPoints;

            if (calibrationPoints.Count == 0)
                return b.Center;

            float margin = calibrationPoints[0].ScreenX; //0.1f

            float offsetX = 0;
            offsetX -= margin + (calibrationPoints[0].ScreenX - calibrationPoints[0].WebcamX); //Move left
            offsetX += margin + (calibrationPoints[1].ScreenX - calibrationPoints[1].WebcamX); //Move right

            offsetX -= margin + (calibrationPoints[3].ScreenX - calibrationPoints[3].WebcamX); //Move left
            offsetX += margin + (calibrationPoints[2].ScreenX - calibrationPoints[2].WebcamX); //Move right

            float offsetY = 0;
            offsetY -= margin + (calibrationPoints[0].ScreenY - calibrationPoints[0].WebcamY); //Move up
            offsetY += margin + (calibrationPoints[1].ScreenY - calibrationPoints[1].WebcamY); //Move down

            offsetY -= margin + (calibrationPoints[3].ScreenY - calibrationPoints[3].WebcamY); //Move up
            offsetY += margin + (calibrationPoints[2].ScreenY - calibrationPoints[2].WebcamY); //Move down

            //float aspectX = 1 + (float)Screen.PrimaryScreen.Bounds.Width % (float)Camera.DesiredFrameSize.Width / (float)Camera.DesiredFrameSize.Width;
            //float aspectY = 1 + (float)Screen.PrimaryScreen.Bounds.Height % (float)Camera.DesiredFrameSize.Height / (float)Camera.DesiredFrameSize.Height;

            //offsetX *= aspectX;

            //Move left
            //b.Rect = new Rectangle(

            PointF relativeCenter = new PointF((float)b.Center.X / (float)Camera.DesiredFrameSize.Width, (float)b.Center.Y / (float)Camera.DesiredFrameSize.Height);

            return new Point((int)(Screen.PrimaryScreen.Bounds.Width * (relativeCenter.X - offsetX)),
                (int)(Screen.PrimaryScreen.Bounds.Height * (relativeCenter.Y - offsetY)));
        }

        Bitmap frame;
        void VideoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            if(OnCameraFrame != null)
                OnCameraFrame(Image.Clone(eventArgs.Frame));

            if (skipFrames < Global.AppSettings.SkipFrames)
            {
                skipFrames++;
                return;
            }
            else
                skipFrames = 0;

            frame = (Bitmap)eventArgs.Frame.Clone();

            if (detector == null || tracker == null || restartDetector != null)
            {
                if (restartDetector != null)
                    detector = restartDetector;
                else
                    detector = DetectorManager.LoadDetectorWithoutExceptions(DetectorManager.DetectorDirectory + "\\" + Global.AppSettings.DetectorName + ".dll");

                if (detector == null)
                    detector = new Detector1();

                detector.Initialize(Image.Clone(frame));

                tracker = new Tracker1();
                restartDetector = null;
            }

            try
            {
                detector.ProcessFrame(ref frame);
            }
            catch(Exception exc)
            {
                if (detector.GetType() != typeof(Detector1))
                    SetDetector(new Detector1());
                else
                    throw exc;

                return;
            }

            if (OnProcessedFrame != null)
                OnProcessedFrame(Image.Clone(frame));

            List<Blob> trackedBlobs = tracker.ProcessBlobs(detector.GetBlobRectangles());

            if (OnBlobsTracked != null)
                OnBlobsTracked(trackedBlobs);

            Bitmap processedCameraFrame;
            if (OnProcessedFrame != null)
            {
                processedCameraFrame = frame.Clone(new Rectangle(0, 0, frame.Size.Width, frame.Size.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(processedCameraFrame);

                Font idFont = new Font("Arial", processedCameraFrame.Width / 20);
                
                //Draw rectangles to original camera frame
                foreach (Blob blob in trackedBlobs)
                {
                    if (!blob.Active)
                        continue;
                    
                    g.DrawRectangle(Pens.Yellow, blob.Rect);

                    g.FillRectangle(new SolidBrush(Color.FromArgb(150, Color.DarkRed)), 
                        new Rectangle(blob.Rect.X, blob.Rect.Y, (int)g.MeasureString(blob.Id.ToString(), idFont).Width, (int)idFont.GetHeight()));
                    g.DrawString(blob.Id.ToString(), idFont, Brushes.White, new PointF(blob.Rect.X, blob.Rect.Y));
                }

                if (OnProcessedCameraFrame != null) //this is a different thread, so it might have changed in the meantime (race condition)
                    OnProcessedCameraFrame(processedCameraFrame);
            }
#if !DEBUG
            if (NewFrame != null)
#endif
            {
                List<Contact> contacts = new List<Contact>();
                foreach (Blob blob in trackedBlobs)
                {
                    Point w2s = WebcamToScreen(blob);
                    Console.WriteLine(w2s.ToString());
                    Contact c = new Contact(blob.Id, blob.ContactState, new System.Windows.Point(w2s.X, w2s.Y), blob.Rect.Width, blob.Rect.Height);
                    contacts.Add(c);
                }
                
#if !DEBUG
                NewFrame(this, new NewFrameEventArgs(Stopwatch.GetTimestamp(), contacts, null));
#endif
                contacts.Clear();
            }

            frame.Dispose();
        }
    }
}