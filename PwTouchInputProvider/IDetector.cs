using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

//http://www.aforgenet.com/framework/features/

namespace PwTouchInputProvider
{
    public interface IDetector
    {
        void ProcessFrame(ref Bitmap frame);
        Rectangle[] GetBlobRectangles();
    }
}
