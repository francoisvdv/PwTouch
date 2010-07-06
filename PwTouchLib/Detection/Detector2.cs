using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.VideoSurveillance;

namespace PwTouchLib.Detection
{
    public class Detector2 : Detector, IDisposable
    {
        IBGFGDetector<Gray> bgfgDetector;
        BlobDetector blobDetector;
        BlobTracker blobTracker;

        BlobSeq oldList;

        public IBGFGDetector<Gray> BgFgDetector
        {
            get { return bgfgDetector; }
        }

        public override IEnumerable<MCvBlob> Blobs
        {
            get { return blobTracker; }
        }

        public Detector2()
        {
            bgfgDetector = new FGDetector<Gray>(FORGROUND_DETECTOR_TYPE.FGD_SIMPLE);
            blobDetector = new BlobDetector(BLOB_DETECTOR_TYPE.Simple);
            blobTracker = new BlobTracker(BLOBTRACKER_TYPE.MS);
        }

        public override void DetectBlobs(Image<Bgr, Byte> frame)
        {
            Image<Gray, Byte> img = frame.Convert<Gray, Byte>();

            //Find foreground mask
            bgfgDetector.Update(img);

            Image<Gray, Byte> foregroundMask = img;
            //Image<Gray, Byte> foregroundMask = bgfgDetector.ForgroundMask;
            //Image<Gray, Byte> foregroundMask = new Image<Gray, byte>("foreground.png");

            blobTracker.Add(new MCvBlob(), null, null);
            foreach (MCvBlob blob in blobTracker)
            {
            }

            BlobSeq newList = new BlobSeq();
            while (blobDetector.DetectNewBlob(frame, foregroundMask, newList, oldList) != 0)
            {
                oldList = newList;
            }

            foreach (MCvBlob blob in oldList)
            {
                blobTracker.Add(blob, frame, foregroundMask);
            }
        }

        public void Dispose()
        {
            blobTracker.Dispose();
        }
    }
}
