using System;
using System.Collections.Generic;
using System.Text;

namespace PwTouchInputProvider
{
    public static class Global
    {
        public const bool DeveloperMode = true;

        public static readonly AppSettings AppSettings = new AppSettings(System.Windows.Forms.Application.StartupPath + "//Settings.xml");

        static Global()
        {
            AppSettings.Load();
        }
    }
}
