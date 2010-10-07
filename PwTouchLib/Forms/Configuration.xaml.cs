using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PwTouchLib.Forms
{
    /// <summary>
    /// Adds MainControls to its WindowsFormsHost.
    /// </summary>
    public partial class Configuration : UserControl
    {
        MainControls m;

        public Configuration()
        {
            DataContext = this;

            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            m = new MainControls();
            this.Width = m.Size.Width;
            this.Height = m.Size.Height;
            this.windowsFormsHost1.Child = m;

            m.Start();
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.windowsFormsHost1.Child = null;
            m.Stop();
            m.Dispose();
        }

    }
}
