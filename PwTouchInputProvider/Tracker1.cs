using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PwTouchInputProvider
{
    public class Tracker1 : TrackerBase
    {
        public override List<Rectangle> ProcessBlobs(IEnumerable<Rectangle> blobs)
        {
            return new List<Rectangle>(blobs);
        }
    }
}
