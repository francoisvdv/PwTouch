using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows;
using Multitouch.Contracts;
using System.Windows.Forms;
using System.Timers;

namespace PwTouch
{
	[AddIn("PwTouch", Publisher = "Francois van der Ven", Description = "Provides input from PwTouch table.", Version = VERSION)]
	[Export(typeof(IProvider))]
	public class InputProvider : IProvider
	{
		internal const string VERSION = "1.0.0.0";
        List<Contact> contacts = new List<Contact>();
        System.Timers.Timer timer;

        public InputProvider()
        {
            timer = new System.Timers.Timer(1000 / 60d);
			timer.Elapsed += timer_Elapsed;

            contacts.Add(new Contact(0, ContactState.New, new System.Windows.Point(10, 10), 1680, 1050));
        }

		public void Start()
		{
			timer.Start();
		}

		public void Stop()
		{
			timer.Stop();
		}

        public bool IsRunning
        {
            get { return true; }
        }

        public bool HasConfiguration
        {
            get { return true; }
        }

        void timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			EventHandler<NewFrameEventArgs> eventHandler = NewFrame;
			if (eventHandler != null)
				eventHandler(this, new NewFrameEventArgs(Stopwatch.GetTimestamp(), contacts, null));

            contacts.Clear();
            contacts.Add(new Contact(0, ContactState.New, new System.Windows.Point(10, 10), SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height));
		}

        public System.Windows.UIElement GetConfiguration()
        {
            return null;
        }

        public bool SendImageType(ImageType imageType, bool value)
        {
            return false;
        }

        public bool SendEmptyFrames
        {
            get
            {
                return true;
            }
            set
            {

            }
        }

        public event EventHandler<NewFrameEventArgs> NewFrame;
    }
}