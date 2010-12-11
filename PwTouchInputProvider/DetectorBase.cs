using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using AForge.Imaging;
using AForge.Imaging.Filters;

//http://www.aforgenet.com/framework/features/

namespace PwTouchInputProvider
{
    public abstract class DetectorBase
    {
        protected FiltersSequence fSequence = new FiltersSequence();
        protected BlobCounter blobCounter = new BlobCounter();

        public int SkipFrames { get; set; }

        public virtual Rectangle[] GetBlobRectangles() { return blobCounter.GetObjectsRectangles(); }

        Bitmap processed;
        public void ProcessFrame(Bitmap frame)
        {
            if (processed != null)
                processed.Dispose();

            processed = null;

            ProcessFrame(frame, out processed);
        }
        public abstract void ProcessFrame(Bitmap frame, out Bitmap processed);
    }
}
