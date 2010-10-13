using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

//http://www.aforgenet.com/framework/features/

namespace PwTouchInputProvider
{
    public interface IDetector
    {
        bool DrawBlobMarkers { get; set; }
        void ProcessFrame(ref Bitmap frame);
    }
}
