using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PwTouchInputProvider.Forms;
using System.Threading;
using PwTouchInputProvider;

namespace PwTouchConfiguration
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();

            if (!PipeClient.SendMessage(PipeClient.Command.Stop))
                Log.Write("Couldn't stop driver, it probably isn't running.");

            try
            {
                Application.Run(new MainForm());
            }
            catch (Exception exc)
            {
#if DEBUG
                throw exc;
#else
                MessageBox.Show("PwTouchApp configuratiescherm is vastgelopen. Zie het volgende bestand voor meer informatie:\r\n" + Log.FilePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log.Write(exc.ToString(), true);
#endif
            }

            if (!PipeClient.SendMessage(PipeClient.Command.Start))
                Log.Write("Couldn't restart driver, it probably isn't running.");
        }

    }
}
