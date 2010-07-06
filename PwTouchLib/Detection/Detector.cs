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
    public abstract class Detector
    {
        public abstract IEnumerable<MCvBlob> Blobs { get; }

        public abstract void DetectBlobs(Image<Bgr, Byte> frame);
        
        
    }
}
