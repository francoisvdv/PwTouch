using System;
using System.Collections.Generic;
using System.Text;

namespace PwTouchInputProvider
{
    public static class Global
    {
        public const bool DeveloperMode = true;

        public static readonly string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\PwTouchAppInputProvider\";

        public static readonly AppSettings AppSettings = new AppSettings(AppDataFolder + @"\Settings.xml");
    }
}
