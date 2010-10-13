using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using AForge.Imaging;
using AForge.Imaging.Filters;

namespace PwTouchInputProvider
{
    /// <summary>
    /// This detector compares the current frame to the first frame of the image sequence.
    /// </summary>
    public class Detector1 : IDetector
    {
        public bool DrawBlobMarkers { get; set; }

        FiltersSequence     fSequence = new FiltersSequence();
        BlobCounter         blobCounter = new BlobCounter();

        public Detector1(Bitmap backgroundImage)
        {
            backgroundImage = Grayscale.CommonAlgorithms.RMY.Apply(backgroundImage);

            fSequence.Add(Grayscale.CommonAlgorithms.RMY);
            fSequence.Add(new Difference(backgroundImage));
            fSequence.Add(new Blur());
            fSequence.Add(new Threshold(100));
            fSequence.Add(new Erosion());

            blobCounter.ObjectsOrder = ObjectsOrder.None;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ProcessFrame(ref Bitmap frame)
        {
            Bitmap processed = (Bitmap)frame.Clone();
            processed = fSequence.Apply(frame);

            blobCounter.ProcessImage(processed);

            if (DrawBlobMarkers)
            {
                foreach (Rectangle r in blobCounter.GetObjectsRectangles())
                {
                    Graphics g = Graphics.FromImage(frame);
                    g.DrawRectangle(Pens.Yellow, r);
                }
            }

            processed.Dispose();
        }
    }
}
