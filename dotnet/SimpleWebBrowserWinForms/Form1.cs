using System;
using System.Configuration;
using System.Windows.Forms;

namespace SimpleWebBrowserWinForms
{
    public partial class Form1 : Form
    {
        private Uri WmUri { get; set; }

        public Form1()
        {
            InitializeComponent();
            Configure();
        }

        protected override void OnLoad(EventArgs e)
        {
            webBrowser1.Navigate(WmUri);
        }

        private void Configure()
        {
            string wmUrl = ConfigurationManager.AppSettings["WMUrl"];
            if (wmUrl != null)
                WmUri = new Uri(wmUrl);
        }
    }
}
