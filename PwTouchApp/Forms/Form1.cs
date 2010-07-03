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

namespace PwTouchApp.Forms
{
    public partial class Form1 : Form
    {
        private Capture _capture;

        BlobDetector blobDetector = new BlobDetector(BLOB_DETECTOR_TYPE.CC);

        public Form1()
        {
            InitializeComponent();

            //try to create the capture
            if (_capture == null)
            {
                try
                {
                    _capture = new Capture();
                }
                catch (NullReferenceException excpt)
                {   //show errors if there is any
                    MessageBox.Show(excpt.Message);
                }
            }

            if (_capture != null) //if camera capture has been successfully created
            {
                Application.Idle += ProcessFrame;
            }
        }

        void ProcessFrame(object sender, EventArgs e)
        {
            DetectBlobs4();
        }

        void UpdateText(String text)
        {
            label3.Text = text;
        }

        void DetectBlobs(Image<Gray, Byte> frame)
        {
            //BlobDetector blobDetector = new BlobDetector(Emgu.CV.CvEnum.BLOB_DETECTOR_TYPE.CC);

            using (MemStorage stor = new MemStorage())
            {
                Contour<Point> contours = frame.FindContours(
                Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_SIMPLE,
                Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_TREE,
                stor);
                
                while(contours != null)
                {
                    UpdateText(contours.BoundingRectangle.ToString());

                    contours = contours.HNext;
                }
            }
        }
        
        private static MCvFont _font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_SIMPLEX, 1.0, 1.0);
        private static BlobTrackerAuto<Bgr> _tracker;
        private static IBGFGDetector<Bgr> _detector;
        void DetectBlobs3()
        {
            if (_detector == null && _tracker == null)
            {
                _detector = new FGDetector<Bgr>(FORGROUND_DETECTOR_TYPE.FGD);

                _tracker = new BlobTrackerAuto<Bgr>();
            }

            Image<Bgr, Byte> frame = _capture.QueryFrame();
            frame._SmoothGaussian(3); //filter out noises

            #region use the background code book model to find the forground mask
            _detector.Update(frame);

            Image<Gray, Byte> foregroundMask = _detector.ForgroundMask;
            #endregion

            _tracker.Process(frame, foregroundMask);

            foreach (MCvBlob blob in _tracker)
            {
                frame.Draw(Rectangle.Round(blob), new Bgr(255.0, 255.0, 255.0), 2);
                frame.Draw(blob.ID.ToString(), ref _font, Point.Round(blob.Center), new Bgr(255.0, 255.0, 255.0));
            }

            capturedImageBox.Image = frame;
            foregroundImageBox.Image = foregroundMask;
        }

        private static BlobDetector _blobDetector;
        private static BlobSeq oldList = new BlobSeq();
        private static BlobSeq newList = new BlobSeq();
        bool initial;
        void DetectBlobs4()
        {
            if (_detector == null && _tracker == null)
            {
                _detector = new FGDetector<Bgr>(FORGROUND_DETECTOR_TYPE.FGD);
                _tracker = new BlobTrackerAuto<Bgr>();
                _blobDetector = new BlobDetector(BLOB_DETECTOR_TYPE.CC);
            }

            Image<Bgr, Byte> frame = new Image<Bgr, byte>("test.png"); //_capture.QueryFrame();
            frame._SmoothGaussian(3); //filter out noises

            #region use the background code book model to find the forground mask
            _detector.Update(frame);

            Image<Gray, Byte> foregroundMask = frame.Convert<Gray, Byte>(); //_detector.ForgroundMask; //new Image<Gray, byte>("foreground.png"); 
            Image<Gray, Byte> backgroundMask = _detector.BackgroundMask;
            #endregion

            label3.Text = "Figuren: ";

            _blobDetector.DetectNewBlob(frame, foregroundMask, newList, oldList);

            foreach (MCvBlob blob in newList)
            {
                frame.Draw(Rectangle.Round(blob), new Bgr(255.0, 255.0, 255.0), 2);
                frame.Draw(blob.ID.ToString(), ref _font, Point.Round(blob.Center), new Bgr(255.0, 255.0, 255.0));

                //Console.WriteLine(blob.Center.ToString() + " | " + blob.Size.ToString());

                label3.Text += " | (" + Math.Round(blob.Center.X).ToString() + ", " + Math.Round(blob.Center.Y).ToString() + ")";
            }

            oldList = newList;

            capturedImageBox.Image = frame;
            foregroundImageBox.Image = foregroundMask;
        }

        void DrawMotion(Image<Bgr, Byte> image, Rectangle motionRegion, double angle, Bgr color)
        {
            float circleRadius = (motionRegion.Width + motionRegion.Height) >> 2;
            Point center = new Point(motionRegion.X + motionRegion.Width >> 1, motionRegion.Y + motionRegion.Height >> 1);

            CircleF circle = new CircleF(
            center,
            circleRadius);

            int xDirection = (int)(Math.Cos(angle * (Math.PI / 180.0)) * circleRadius);
            int yDirection = (int)(Math.Sin(angle * (Math.PI / 180.0)) * circleRadius);
            Point pointOnCircle = new Point(
            center.X + xDirection,
            center.Y - yDirection);
            LineSegment2D line = new LineSegment2D(center, pointOnCircle);
  
            image.Draw(circle, color, 1);
            image.Draw(line, color, 2);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {

            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
