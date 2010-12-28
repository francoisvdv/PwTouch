using System.Drawing;
using AForge.Imaging.Filters;

namespace PwTouchInputProvider
{
    /// <summary>
    /// The default detector. If a user-defined detector doesn't work, we revert to this one.
    /// </summary>
    public class Detector1 : DetectorBase
    {
        public override void Initialize(Bitmap backgroundImage)
        {
            base.Initialize(backgroundImage);

            backgroundImage = Grayscale.CommonAlgorithms.RMY.Apply(backgroundImage);

            fSequence.Add(Grayscale.CommonAlgorithms.RMY);
            fSequence.Add(new Difference(backgroundImage));
            fSequence.Add(new Threshold(100));
        }

        public override void ProcessFrame(ref Bitmap frame)
        {
            frame = fSequence.Apply(frame);

            blobCounter.ProcessImage(frame);
        }
    }
}
