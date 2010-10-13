using System;
using System.Collections.Generic;
using System.Text;

using AForge.Video;
using AForge.Video.DirectShow;

namespace PwTouchInputProvider
{
    public static class CameraManager
    {
        public static FilterInfoCollection GetCameras()
        {
            return new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        public static VideoCaptureDevice GetCamera(int index)
        {
            FilterInfoCollection devices = GetCameras();
            if (devices.Count == 0 || index < 0 || index > devices.Count)
                return null;
            
            return new VideoCaptureDevice(devices[index].MonikerString);
        }
    }
}
