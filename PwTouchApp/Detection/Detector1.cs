using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.VideoSurveillance;

namespace PwTouchApp.Detection
{
    public class Detector1 : IDisposable
    {
        IBGFGDetector<Bgr> bgfgDetector;
        BlobDetector blobDetector;
        BlobSeq oldList = new BlobSeq();
        BlobSeq newList = new BlobSeq();

        public IBGFGDetector<Bgr> BgFgDetector
        {
            get { return bgfgDetector; }
        }
        public BlobSeq Blobs
        {
            get { return oldList; }
        }

        public Detector1()
        {
            bgfgDetector = new FGDetector<Bgr>(FORGROUND_DETECTOR_TYPE.FGD_SIMPLE);
            blobDetector = new BlobDetector(BLOB_DETECTOR_TYPE.Simple);
        }

        public void DetectBlobs(Image<Bgr, Byte> frame)
        {
            frame._SmoothGaussian(3); //filter out noises

            //Find foreground mask
            bgfgDetector.Update(frame);

            Image<Gray, Byte> foregroundMask = frame.Convert<Gray, Byte>(); //_detector.ForgroundMask; //new Image<Gray, byte>("foreground.png"); 
            
            while (blobDetector.DetectNewBlob(frame, foregroundMask, newList, oldList) != 0)
            {
                oldList = newList;
            }
        }

        public void Dispose()
        {
            blobDetector.Dispose();

            oldList.Dispose();
            newList.Dispose();
        }
    }
}
