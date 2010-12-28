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
            
            VideoCaptureDevice vcd = new VideoCaptureDevice(devices[index].MonikerString);

            //It takes a moment to fill the VideoCapabilities property (see AForge.NET VideoCaptureDevice.VideoCapabilities docs)
            //Wait for a maximum of 5000 millisecs.
            int i = 0;
            while (vcd.VideoCapabilities == null && i < 5000)
            {
                System.Threading.Thread.Sleep(1);

                i++;
            }

            return vcd;
        }
    }
}
