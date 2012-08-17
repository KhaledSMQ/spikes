using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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

namespace SimpleWebBrowserWpf
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Uri WmUri { get; set; }

        public Window1()
        {
            InitializeComponent();
            Configure();
            WebBrowserControl.Navigate(WmUri);
        }

        private void Configure()
        {
            string wmUrl = ConfigurationManager.AppSettings["WMUrl"];
            if (wmUrl != null)
                WmUri = new Uri(wmUrl);
        }
    }
}
