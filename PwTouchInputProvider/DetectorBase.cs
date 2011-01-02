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
        public BlobCounter BlobCounter = new BlobCounter();

        public bool Initialized { get; private set; }

        public virtual void Initialize(Bitmap backgroundImage)
        {
            fSequence = new FiltersSequence();
            BlobCounter = new BlobCounter();

            BlobCounter.MinWidth = BlobCounter.MinHeight = Global.AppSettings.MinBlobSize;
            BlobCounter.MaxWidth = BlobCounter.MaxWidth = Global.AppSettings.MaxBlobSize;
            BlobCounter.FilterBlobs = Global.AppSettings.FilterBlobs;
            BlobCounter.ObjectsOrder = ObjectsOrder.None;
            
            Initialized = true;
        }

        public virtual Rectangle[] GetBlobRectangles() { return BlobCounter.GetObjectsRectangles(); }

        public abstract void ProcessFrame(ref Bitmap frame);
    }
}
