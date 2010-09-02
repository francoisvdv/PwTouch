using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using AForge.Imaging;
using AForge.Imaging.Filters;

namespace PwTouchLib
{
    /// <summary>
    /// This detector compares the current frame to the first frame of the image sequence.
    /// </summary>
    public class Detector1 : IDetector
    {
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

        public void ProcessFrame(ref Bitmap frame)
        {
            Bitmap processed = (Bitmap)frame.Clone();
            processed = fSequence.Apply(frame);

            blobCounter.ProcessImage(processed);

            foreach (Rectangle r in blobCounter.GetObjectsRectangles())
            {
                Graphics g = Graphics.FromImage(frame);
                g.DrawRectangle(Pens.Yellow, r);
            }

            processed.Dispose();
        }
    }
}
