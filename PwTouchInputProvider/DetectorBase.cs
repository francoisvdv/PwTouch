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

        public virtual Rectangle[] GetBlobRectangles() { return blobCounter.GetObjectsRectangles(); }

        public abstract void ProcessFrame(ref Bitmap frame);
        
    }
}
