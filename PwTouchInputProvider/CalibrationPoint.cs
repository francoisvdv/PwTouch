using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PwTouchInputProvider
{
    /// <summary>
    /// All coordinates are relative. E.g. ScreenX = 0.2f, if actual X is 200 and total screen width is 1000.
    /// </summary>
    public class CalibrationPoint
    {
        public static implicit operator PointF(CalibrationPoint p)
        {
            return new PointF(p.ScreenX, p.ScreenY);
        }
        public static implicit operator CalibrationPoint(Point d)
        {
            return new CalibrationPoint(d.X, d.Y);
        }

        public float ScreenX { get; set; }
        public float ScreenY { get; set; }

        public float WebcamX { get; set; }
        public float WebcamY { get; set; }

        public bool IsSet { get { return (WebcamX != -1 && WebcamY != -1); } }

        public CalibrationPoint(float screenX, float screenY, float webcamX = -1, float webcamY = -1)
        {
            ScreenX = screenX;
            ScreenY = screenY;

            WebcamX = webcamX;
            WebcamY = webcamY;
        }
    }
}
