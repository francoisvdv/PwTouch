using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PwTouchInputProvider
{
    public class Blob
    {
        public long Id { get; set; }
        public Rectangle Rect { get; set; }

        public bool Active { get; set; }
    }
}
