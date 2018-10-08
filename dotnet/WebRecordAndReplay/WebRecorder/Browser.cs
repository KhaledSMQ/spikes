using System;
using System.Collections.Generic;
using System.Linq;
using CefSharp;
using CefSharp.Wpf;

namespace WebRecorder
{
    public class Browser
    {
		public event EventHandler<UserActionEventArgs> UserActionPlaying;
		public event EventHandler<UserActionEventArgs> UserActionPlayed;
		public event EventHandler<UserActionEventArgs> UserActionRecording;
		public event EventHandler<UserActionEventArgs> UserActionRecorded;
		public event EventHandler FinishedPlaying;
		public event EventHandler<ConsoleMessageEventArgs> ConsoleMessageReceived;
		public event EventHandler<LogMessageEventArgs> LogMessageReceived;

        public WebView WebView { get; private set; }

        private UserActionRecorder Recorder { get; set; }
        private bool IsRecording { get; set; }
        private bool IsPlaying { get; set; }

		public IEnumerable<UserAction> RecordedUserActions
		{
			get { return Recorder.Actions.ToList(); }
		}

        public Browser()
        {
            WebView = new WebView();
            IsRecording = false;
		    IsPlaying = false;
			Initialize();
			SubscribeToEvents();
        }

        public void Shutdown()
        {
            WebView.Dispose();
            System.Threading.Thread.Sleep(1000);
            CEF.Shutdown();
        }

        public void Navigate(string address)
        {
            Log("Navigating to " + address);
            WebView.Load(address);
        }

		public void ClearRecordedActions()
		{
			Recorder.Clear();
		}

		public void LoadActions(IEnumerable<UserAction> userActions)
		{
			Recorder.Load(userActions);
		}

        public void StartRecording()
        {
            if (!IsRecording)
            {
                IsRecording = true;
                Recorder.Clear();
            }            
        }

        public void StopRecording()
        {
            if (IsRecording)
            {
                IsRecording = false;
            }
        }

        public void StartPlaying()
        {
            if (!IsPlaying)
            {
                IsPlaying = true;
            }

            Recorder.PlayAll();
        }

        public void StopPlaying()
        {
            if (IsPlaying)
            {
                IsPlaying = false;
            }
        }

		private void Initialize()
		{
			CEF.Initialize(new Settings() { LogFile = @".\debug.log", LogSeverity = LogSeverity.Verbose });
			RegisterAutomationObject();
			WebView.RequestHandler = new RequestHandler();
            Recorder = new UserActionRecorder { Browser = WebView };
			Recorder.UserActionPlaying += RecorderUserActionPlaying;
			Recorder.UserActionPlayed += RecorderUserActionPlayed;
			Recorder.FinishedPlaying += RecorderFinishedPlaying;
        }

		private void RegisterAutomationObject()
		{
			var automation = new AutomationObject();
			automation.UserActionReceived += UserActionReceived;
			CEF.RegisterJsObject("automation", automation);
		}

		private void SubscribeToEvents()
		{
            WebView.PropertyChanged += WebViewPropertyChanged;
            WebView.LoadCompleted += WebViewLoadCompleted;
            WebView.ConsoleMessage += WebViewConsoleMessage;
		}

		private void Record(UserAction userAction)
		{
			Log("Recording", userAction);
			OnUserActionRecording(userAction);
            Recorder.Record(userAction);
			Log("Recorded", userAction);
			OnUserActionRecorded(userAction);
		}

		private void Log(string action, UserAction userAction)
		{
			var message = string.Format("{0} {1}: {2}", action, userAction.Type, userAction);
			Log(message);
		}

        private void Log(string message)
        {
            OnLogMessageReceived(message);
        }

		private void InjectJQuery()
		{
			var jq = GetJQuery();
            WebView.EvaluateScript(jq);
		}

		private void InjectHandlers()
		{
			var automation = GetAutomation();
            WebView.EvaluateScript(automation);
		}

		private static string GetJQuery()
		{
			return Properties.Resources.jquery_1_9_1_min;
		}

		private static string GetAutomation()
		{
			return Properties.Resources.automation;
		}

		private void WebViewLoadCompleted(object sender, LoadCompletedEventArgs e)
		{
		    var addressLoaded = e.Url;
			Log("Loaded page " + addressLoaded);

		    //var request = WebView.GetRequest();
            
			if (IsMainPage(addressLoaded))
			{
                Log("Navigated to " + addressLoaded);
                if (IsRecording)
                    Record(new NavigateAction { Href = addressLoaded });
                InjectJQuery();
				InjectHandlers();
                PerformScriptSubscriptions();
			    OverrideWindowOpen();
			}
			else
			{
			    var userAction = WebView.GetLastAction();
			    if (userAction != null)
			    {
			        var target = userAction.GetParameter(UserActionParameters.Target);
			        if (!string.IsNullOrWhiteSpace(target))
			        {
                        InjectJQuery();
                        InjectHandlers();
                        PerformScriptSubscriptions(target);
                        OverrideWindowOpen(target);
                    }
			    }
			}
		}

        private bool IsMainPage(string address)
        {
            return WebView.Address == address;
        }

        private string GetContent()
        {
            var content = WebView.EvaluateScript(@"document.getElementsByTagName('html')[0].innerHTML").ToString();
            return content;
        }

        private void PerformScriptSubscriptions(string frameTarget = null)
        {
            var script = @"subscribeToEvents()";
            if (!string.IsNullOrWhiteSpace(frameTarget))
                script = string.Format("subscribeToEventsInFrame('{0}')", frameTarget);
            WebView.EvaluateScript(script);
        }

        private void OverrideWindowOpen(string frameTarget = null)
        {
            var script = @"overrideWindowOpen()";
            if (!string.IsNullOrWhiteSpace(frameTarget))
                script = string.Format("overrideWindowOpenInFrame('{0}')", frameTarget);
            WebView.EvaluateScript(script);
        }

		private void WebViewConsoleMessage(object sender, CefSharp.ConsoleMessageEventArgs e)
		{
            OnConsoleMessageReceived(e.Message);
		}

		private void WebViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var propertyName = e.PropertyName;

			if (propertyName == "IsBrowserInitialized")
			{
                if (WebView.IsBrowserInitialized)
				{
                    WebView.Load("about:blank");
				}
			}
		}

		private void RecorderUserActionPlaying(object sender, UserActionEventArgs e)
		{
			var userAction = e.UserAction;
			Log("Playing", userAction);
			OnUserActionPlaying(userAction);
		}

        private void RecorderUserActionPlayed(object sender, UserActionEventArgs e)
        {
            var userAction = e.UserAction;
			Log("Played", userAction);
			OnUserActionPlayed(userAction);
        }

        private void RecorderFinishedPlaying(object sender, EventArgs e)
        {
			Log("Recorder finished playing");
			OnFinishedPlaying();
        }

        private void UserActionReceived(object sender, UserActionEventArgs e)
        {
	        var userAction = e.UserAction;
			Log("Received", userAction);
            WebView.SetAction(userAction);
			if (IsRecording)
		        Record(userAction);
        }

        private void OnConsoleMessageReceived(string message)
        {
            var handler = ConsoleMessageReceived;
            if (handler != null)
                handler(this, new ConsoleMessageEventArgs(message));
        }

        private void OnLogMessageReceived(string message)
        {
            var handler = LogMessageReceived;
            if (handler != null)
                handler(this, new LogMessageEventArgs(message));
        }

		private void OnUserActionPlaying(UserAction userAction)
		{
			var handler = UserActionPlaying;
			if (handler != null)
				handler(this, new UserActionEventArgs(userAction.Type, userAction));
		}

		private void OnUserActionPlayed(UserAction userAction)
		{
			var handler = UserActionPlayed;
			if (handler != null)
				handler(this, new UserActionEventArgs(userAction.Type, userAction));
		}

		private void OnUserActionRecording(UserAction userAction)
		{
			var handler = UserActionRecording;
			if (handler != null)
				handler(this, new UserActionEventArgs(userAction.Type, userAction));
		}

		private void OnUserActionRecorded(UserAction userAction)
		{
			var handler = UserActionRecorded;
			if (handler != null)
				handler(this, new UserActionEventArgs(userAction.Type, userAction));
		}

        private void OnFinishedPlaying()
        {
            var handler = FinishedPlaying;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
