using System;
using CefSharp;
using CefSharp.Wpf;

namespace WebRecorder
{
	public class NavigateAction : UserAction
	{
		public NavigateAction()
			: base(UserActionTypes.Navigate)
		{ }

	    public string Href
	    {
            get { return GetParameter(UserActionParameters.Href); }
            set { AddParameter(UserActionParameters.Href, value);}
	    }

        public string Target
        {
            get { return GetParameter(UserActionParameters.Target); }
            set { AddParameter(UserActionParameters.Target, value); }
        }

        public override void Execute(WebView browser)
        {
            Subscribe(browser);
            try
            {
                browser.Load(Href);
            }
            catch (Exception)
            {
                Unsubscribe(browser);                
                throw;
            }
        }

        private void Subscribe(WebView browser)
        {
            browser.LoadCompleted += BrowserLoadCompleted;
        }

        private void Unsubscribe(WebView browser)
        {
            browser.LoadCompleted -= BrowserLoadCompleted;
        }

        private void BrowserLoadCompleted(object sender, LoadCompletedEventArgs e)
        {
            var browser = (WebView) sender;
            if (e.Url == Href)
            {
                //TODO: Need to clean up the event subscription if the LoadCompleted event is never raised
                Unsubscribe(browser);   
                base.Execute(browser);
            }
        }

        public override string ToString()
        {
            return string.Format("href='{0}', target='{1}'", GetParameter(UserActionParameters.Href), GetParameter(UserActionParameters.Target));
        }
	}
}
