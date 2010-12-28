using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PwTouchInputProvider
{
    public static class Log
    {
        public static readonly string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"/PwTouchAppInputProvider/Log.txt";

        public static void Write(string msg, bool writeToFile = false)
        {
            Console.WriteLine(msg);

            if (!Directory.Exists(Path.GetDirectoryName(FilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));

            if (writeToFile)
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(DateTime.Now.ToString() + " : " + msg);
                    }
                }
            }
        }
    }
}
