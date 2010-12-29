using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PwTouchInputProvider
{
    public class CalibrationPoint
    {
        public static implicit operator Point(CalibrationPoint p)
        {
            return new Point(p.ImageX, p.ImageY);
        }
        public static implicit operator CalibrationPoint(Point d)
        {
            return new CalibrationPoint(d.X, d.Y);
        }

        public int ImageX { get; set; }
        public int ImageY { get; set; }

        public int ScreenX { get; set; }
        public int ScreenY { get; set; }

        public bool IsSet { get { return (ScreenX != -1 && ScreenY != -1); } }

        public CalibrationPoint(int imageX, int imageY, int screenX = -1, int screenY = -1)
        {
            ImageX = imageX;
            ImageY = imageY;

            ScreenX = -1;
            ScreenY = -1;
        }
    }
}
