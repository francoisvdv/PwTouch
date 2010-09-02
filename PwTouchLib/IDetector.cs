using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

//http://www.aforgenet.com/framework/features/

namespace PwTouchLib
{
    public interface IDetector
    {
        void ProcessFrame(ref Bitmap frame);
    }
}
