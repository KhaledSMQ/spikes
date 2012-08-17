using System;
using System.Windows;
using System.Windows.Controls;
using mshtml;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Guid SID_SWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
            IServiceProvider serviceProvider = (IServiceProvider)myWebBrowser.Document; 

            Guid serviceGuid = SID_SWebBrowserApp;

            Guid iid = typeof(SHDocVw.IWebBrowser2).GUID;

            SHDocVw.IWebBrowser2 myWebBrowser2 = (SHDocVw.IWebBrowser2) serviceProvider.QueryService(ref serviceGuid, ref iid);
            SHDocVw.DWebBrowserEvents_Event wbEvents = (SHDocVw.DWebBrowserEvents_Event)myWebBrowser2;
            SHDocVw.DWebBrowserEvents2_Event wbEvents2 = (SHDocVw.DWebBrowserEvents2_Event)myWebBrowser2;
            wbEvents.NewWindow += new SHDocVw.DWebBrowserEvents_NewWindowEventHandler(wbEvents_NewWindow);
            wbEvents2.NewWindow2 += new SHDocVw.DWebBrowserEvents2_NewWindow2EventHandler(wbEvents2_NewWindow2);
            wbEvents2.NavigateError += new SHDocVw.DWebBrowserEvents2_NavigateErrorEventHandler(wbEvents2_NavigateError);
            wbEvents2.RedirectXDomainBlocked += new SHDocVw.DWebBrowserEvents2_RedirectXDomainBlockedEventHandler(wbEvents2_RedirectXDomainBlocked);
            wbEvents2.ThirdPartyUrlBlocked += new SHDocVw.DWebBrowserEvents2_ThirdPartyUrlBlockedEventHandler(wbEvents2_ThirdPartyUrlBlocked);
        }

        void wbEvents2_ThirdPartyUrlBlocked(ref object URL, uint dwCount)
        {
        }

        void wbEvents2_RedirectXDomainBlocked(object pDisp, ref object StartURL, ref object RedirectURL, ref object Frame, ref object StatusCode)
        {
        }

        void wbEvents2_NavigateError(object pDisp, ref object URL, ref object Frame, ref object StatusCode, ref bool Cancel)
        {
        }

        void wbEvents2_NewWindow2(ref object ppDisp, ref bool Cancel)
        {
            Window1 wnd = new Window1();
            wnd.Show();
            wnd.myWebBrowser.Navigate(new Uri("about:blank"));
            //wnd.myWebBrowser.Navigating += new System.Windows.Navigation.NavigatingCancelEventHandler(myWebBrowser_Navigating);

            Guid SID_SWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
            IServiceProvider serviceProvider = (IServiceProvider)wnd.myWebBrowser.Document;

            Guid serviceGuid = SID_SWebBrowserApp;

            Guid iid = typeof(SHDocVw.IWebBrowser2).GUID;

            SHDocVw.IWebBrowser2 myWebBrowser2 = (SHDocVw.IWebBrowser2)serviceProvider.QueryService(ref serviceGuid, ref iid);
            
            ppDisp = myWebBrowser2.Application;

            SHDocVw.DWebBrowserEvents2_Event wbEvents2 = (SHDocVw.DWebBrowserEvents2_Event)myWebBrowser2;
            wbEvents2.NewWindow2 += new SHDocVw.DWebBrowserEvents2_NewWindow2EventHandler(wbEvents2_NewWindow2);
            wbEvents2.NavigateError += new SHDocVw.DWebBrowserEvents2_NavigateErrorEventHandler(wbEvents2_NavigateError);
            wbEvents2.RedirectXDomainBlocked += new SHDocVw.DWebBrowserEvents2_RedirectXDomainBlockedEventHandler(wbEvents2_RedirectXDomainBlocked);
            wbEvents2.ThirdPartyUrlBlocked += new SHDocVw.DWebBrowserEvents2_ThirdPartyUrlBlockedEventHandler(wbEvents2_ThirdPartyUrlBlocked);        
        }

        void myWebBrowser_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //MessageBox.Show(e.Uri.ToString());
            myWebBrowser.Navigate(e.Uri);
        }

        void wbEvents_NewWindow(string URL, int Flags, string TargetFrameName, ref object PostData, string Headers, ref bool Processed)
        {
            MessageBox.Show(URL);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            myWebBrowser.Navigate(new Uri("http://www.google.com")); 
        }
    }

    public static class WebBrowserScriptingExtensions
    {
        private const string Type = "text/javascript";
        private const string Script = "script";
        private const string Head = "head";

        public static void AddScript(this WebBrowser browser, string scriptText)
        {
            var htmlDocument = browser.Document as HTMLDocumentClass;
            var htmlDocument2 = browser.Document as IHTMLDocument2;

            var script = (IHTMLScriptElement)htmlDocument2.createElement(Script);
            script.type = Type;
            script.text = scriptText;
            AddScriptToHead(htmlDocument, script);
        }

        private static void AddScriptToHead(HTMLDocumentClass htmlDocument, IHTMLScriptElement script)
        {
            var headElementCollection = htmlDocument.getElementsByTagName(Head);
            foreach (IHTMLElement elem in headElementCollection)
            {
                var head = (HTMLHeadElementClass)elem;
                head.appendChild((IHTMLDOMNode)script);
            }
        }
    }
}
