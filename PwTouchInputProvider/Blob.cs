using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PwTouchInputProvider
{
    public class Blob
    {
        /// <summary>The amount of frames this blob has been alive.</summary>
        public int LifeTime { get; set; }
        public int Id { get; set; }
        public Rectangle Rect { get; set; }
        public Point Center { get { return new Point(Rect.X + (Rect.Width / 2), Rect.Y + (Rect.Height / 2)); } }
        public bool Active { get; set; }
        public Multitouch.Contracts.ContactState ContactState
        {
            get
            {
                if (!Active || LifeTime == 0)
                    return Multitouch.Contracts.ContactState.Removed;
                else if (LifeTime == 1)
                    return Multitouch.Contracts.ContactState.New;
                else
                    return Multitouch.Contracts.ContactState.Moved;
            }
        }

        public Multitouch.Contracts.Contact GetContact()
        {
            return new Multitouch.Contracts.Contact(Id, ContactState, new System.Windows.Point(Rect.X + (Rect.Width / 2), Rect.Y + (Rect.Height / 2)), System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
        }
    }
}
