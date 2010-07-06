using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Emgu.CV;

namespace PwTouchLib
{
    //http://michaelsync.net/2010/04/06/step-by-step-tutorial-installing-multi-touch-simulator-for-silverlight-phone-7

   static class Program
   {
       public static Capture Capture { get; set; }

      /// <summary>
      /// The main entry point for the application.
      /// </summary>
      [STAThread]
      static void Main()
      {
         Application.EnableVisualStyles();
         Application.SetCompatibleTextRenderingDefault(false);
         Application.Run(new Forms.Form1());
      }
   }
}