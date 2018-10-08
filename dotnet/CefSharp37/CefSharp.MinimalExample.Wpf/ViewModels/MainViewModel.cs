using System;
using System.ComponentModel;
using System.Windows;
using CefSharp.MinimalExample.Wpf.Mvvm;
using CefSharp.Wpf;

namespace CefSharp.MinimalExample.Wpf.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IWpfWebBrowser _webBrowser;
		private string _title;
		private string _outputText;
		
		public IWpfWebBrowser WebBrowser
        {
            get { return _webBrowser; }
	        set
	        {
				SubscribeToEvents(value);
		        PropertyChanged.ChangeAndNotify(ref _webBrowser, value, () => WebBrowser);
	        }
        }

        public string Title
        {
            get { return _title; }
            set { PropertyChanged.ChangeAndNotify(ref _title, value, () => Title); }
        }

		public string OutputText
	    {
			get { return _outputText; }
			set
			{
				_outputText = value;
				OnPropertyChanged("OutputText");
			}
	    }

        public MainViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Title")
            {
                Application.Current.MainWindow.Title = "CefSharp.MinimalExample.Wpf - " + Title;
            }
        }

		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

	    private void SubscribeToEvents(IWpfWebBrowser browser)
	    {
		    if (browser != null)
		    {
			    browser.ConsoleMessage += browser_ConsoleMessage;
			    browser.FrameLoadStart += browser_FrameLoadStart;
			    browser.FrameLoadEnd += browser_FrameLoadEnd;
			    browser.LoadError += browser_LoadError;
			    browser.NavStateChanged += browser_NavStateChanged;
			    browser.StatusMessage += browser_StatusMessage;
		    }
	    }

	    private void Write(string text)
	    {
		    OutputText += text + Environment.NewLine;
	    }

		void browser_StatusMessage(object sender, StatusMessageEventArgs e)
		{
			Write(string.Format("StatusMessage: {0}", e.Value));
		}

		void browser_NavStateChanged(object sender, NavStateChangedEventArgs e)
		{
			Write("NavStateChanged.");
		}

		void browser_LoadError(object sender, LoadErrorEventArgs e)
		{
			Write(string.Format("LoadError: error code {0}, error text {1}, failed url {2}", e.ErrorCode, e.ErrorText, e.FailedUrl));
		}

		void browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
		{
			Write(string.Format("FrameLoadEnd: http code {0}, is main frame {1}, url {2}", e.HttpStatusCode, e.IsMainFrame, e.Url));
		}

		void browser_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
		{
			Write(string.Format("FrameLoadStart: is main frame {0}, url {1}", e.IsMainFrame, e.Url));
		}

		void browser_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
		{
			Write(string.Format("ConsoleMessage: {0}, source {1}, line {2}", e.Message, e.Source, e.Line));
		}
    }
}
