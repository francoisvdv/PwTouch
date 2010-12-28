using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PwTouchConfiguration
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
#else
            try
            {
                PwTouchInputProvider.PipeClient.SendMessage("OpenConfiguration");
            }
            catch
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                MessageBox.Show("Kon PwTouchApp configuratie niet openen. Staat de driver aan?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif
        }
    }
}
