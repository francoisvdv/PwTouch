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
    public class Detector1 : DetectorBase
    {
        public Detector1(Bitmap backgroundImage)
        {
            backgroundImage = Grayscale.CommonAlgorithms.RMY.Apply(backgroundImage);

            fSequence.Add(Grayscale.CommonAlgorithms.RMY);
            fSequence.Add(new Difference(backgroundImage));
            fSequence.Add(new Threshold(100));
            
            blobCounter.MinWidth = 10;
            blobCounter.MinHeight = 10;
            blobCounter.FilterBlobs = true;
            blobCounter.ObjectsOrder = ObjectsOrder.None;
        }

        public override void ProcessFrame(Bitmap frame, out Bitmap processed)
        {
            processed = (Bitmap)frame.Clone();
            processed = fSequence.Apply(frame);

            blobCounter.ProcessImage(processed);
        }
    }
}
