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

        public bool Initialized { get; private set; }

        public virtual void Initialize(Bitmap backgroundImage)
        {
            fSequence = new FiltersSequence();
            blobCounter = new BlobCounter();

            blobCounter.MinWidth = 10;
            blobCounter.MinHeight = 10;
            blobCounter.FilterBlobs = false;
            blobCounter.ObjectsOrder = ObjectsOrder.None;

            Initialized = true;
        }

        public virtual Rectangle[] GetBlobRectangles() { return blobCounter.GetObjectsRectangles(); }

        public abstract void ProcessFrame(ref Bitmap frame);
    }
}
