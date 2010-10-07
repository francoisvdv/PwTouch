using System;
using System.Collections.Generic;
using System.Text;

namespace PwTouchLib
{
    public static class Global
    {
        public static readonly AppSettings AppSettings = new AppSettings(System.Windows.Forms.Application.StartupPath + "//Settings.xml");
    }
}
