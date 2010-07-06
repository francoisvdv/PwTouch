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
    public class Detector1 : Detector, IDisposable
    {
        IBGFGDetector<Bgr> bgfgDetector;
        BlobDetector blobDetector;

        BlobSeq blobs = new BlobSeq();

        public IBGFGDetector<Bgr> BgFgDetector
        {
            get { return bgfgDetector; }
        }

        public override IEnumerable<MCvBlob> Blobs
        {
            get { return blobs; }
        }

        public Detector1()
        {
            bgfgDetector = new FGDetector<Bgr>(FORGROUND_DETECTOR_TYPE.FGD_SIMPLE);
            blobDetector = new BlobDetector(BLOB_DETECTOR_TYPE.Simple);
        }

        public override void DetectBlobs(Image<Bgr, Byte> frame)
        {
            //Find foreground mask
            bgfgDetector.Update(frame);

            Image<Gray, Byte> foregroundMask = frame.Convert<Gray, Byte>();
            //Image<Gray, Byte> foregroundMask = bgfgDetector.ForgroundMask;
            //Image<Gray, Byte> foregroundMask = new Image<Gray, byte>("foreground.png");

            BlobSeq newList = new BlobSeq();
            while (blobDetector.DetectNewBlob(frame, foregroundMask, newList, blobs) != 0)
            {
                blobs = newList;
            }
        }

        public void Dispose()
        {
            blobDetector.Dispose();
        }
    }
}
