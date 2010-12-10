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
using PwTouchInputProvider.Forms;

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

        public delegate void ProcessFrameDelegate(ref Bitmap frame);
        public ProcessFrameDelegate OnProcessed;

        Queue<Contact> contacts = new Queue<Contact>();
        System.Timers.Timer timer;

        VideoCaptureDevice camera;
        DetectorBase detector;
        TrackerBase tracker;
        bool restartDetector;

        public bool DrawBlobMarkers { get; set; }
        public VideoCaptureDevice Camera { get { return camera; } set { camera = value; } }
        public DetectorBase Detector { get { return detector; } set { detector = value; } }
        public TrackerBase Tracker { get { return tracker; } set { tracker = value; } }

        public InputProvider()
        {
            if (camera == null)
            {
                camera = CameraManager.GetCamera(Global.AppSettings.Camera);
            }
            if (camera == null)
            {
                camera = CameraManager.GetCamera(0);
                Global.AppSettings.Camera = 0;
            }
            if (camera == null)
            {
                MessageBox.Show("Er is geen (standaard) camera gevonden. Dit configuratiescherm sluit nu.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                return;
            }

            timer = new System.Timers.Timer(1000 / 60d);
			timer.Elapsed += timer_Elapsed;
        }

		public void Start()
		{
            StartCamera();
            timer.Start();
		}
		public void Stop()
		{
            StopCamera();
			timer.Stop();
		}

        void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			EventHandler<NewFrameEventArgs> eventHandler = NewFrame;
			if (eventHandler != null)
				eventHandler(this, new NewFrameEventArgs(Stopwatch.GetTimestamp(), contacts, null));

            contacts.Clear();
		}

        //Camera
        public void StartCamera()
        {
            if (camera == null)
                return;

            camera.NewFrame += VideoSource_NewFrame;

            if (!camera.IsRunning && Global.DeveloperMode)
                camera.Start();

            RestartDetector();
        }
        public void StopCamera()
        {
            if (camera == null || !camera.IsRunning)
                return;

            camera.NewFrame -= VideoSource_NewFrame;

            if (Global.DeveloperMode)
                camera.SignalToStop();
        }
        /// <summary>Detector will be restarted on next frame.</summary>
        public void RestartDetector()
        {
            restartDetector = true;
        }

        Bitmap frame;
        void VideoSource_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            frame = (Bitmap)eventArgs.Frame.Clone();
            
            if (detector == null || tracker == null || restartDetector)
            {
                detector = new Detector1((Bitmap)frame.Clone());
                tracker = new Tracker1();
                restartDetector = false;
                return;
            }

            detector.ProcessFrame(ref frame);

            IEnumerable<Rectangle> blobs = detector.GetBlobRectangles();

            List<Blob> trackedBlobs = tracker.ProcessBlobs(blobs);

            foreach (Blob blob in trackedBlobs)
            {
                if (DrawBlobMarkers)
                {
                    Graphics g = Graphics.FromImage(frame);
                    g.DrawRectangle(Pens.Yellow, blob.Rect);
                }

                contacts.Enqueue(new Contact(0, ContactState.New, new System.Windows.Point(blob.Rect.X, blob.Rect.Y), blob.Rect.Width, blob.Rect.Height));
            }

            if(OnProcessed != null)
                OnProcessed(ref frame);
        }
    }
}