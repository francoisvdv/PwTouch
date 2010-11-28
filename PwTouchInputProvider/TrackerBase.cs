using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PwTouchInputProvider
{
    public abstract class TrackerBase
    {
        public abstract List<Rectangle> ProcessBlobs(IEnumerable<Rectangle> blobs);
    }
}
