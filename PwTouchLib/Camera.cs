using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AForge.Video;
using AForge.Video.DirectShow;

namespace PwTouchLib
{
    public static class Camera
    {
        public static VideoCaptureDevice GetCamera(int index)
        {
            FilterInfoCollection devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (devices.Count == 0 || index < 0 || index > devices.Count)
                return null;
            
            return new VideoCaptureDevice(devices[index].MonikerString);
        }
    }
}
