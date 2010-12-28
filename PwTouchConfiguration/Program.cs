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

#if !DEBUG
            if (!PipeClient.SendMessage(PipeClient.Command.Stop))
            {
                MessageBox.Show("Kon driver niet pauzeren. Staat de driver aan?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
#endif
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

#if !DEBUG
            if (!PipeClient.SendMessage(PipeClient.Command.Start))
            {
                MessageBox.Show("Kon driver niet hervatten. Probeer de driver handmatig te starten.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
#endif
        }

    }
}
