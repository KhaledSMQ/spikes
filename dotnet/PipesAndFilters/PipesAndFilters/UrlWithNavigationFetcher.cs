using System;
using System.Threading;
using Awesomium.Core;

namespace PipesAndFilters
{
    public class UrlWithNavigationFetcher : FetcherBase<Uri, string>
    {
		private bool FinishedLoading { get; set; }
        private bool FinishedNavigating { get; set; }

        public override string Fetch(Uri uri)
        {
            WebCore.Initialize(new WebConfig()
            {
                LogPath = Environment.CurrentDirectory,
                LogLevel = LogLevel.Verbose,
                ManagedConsole = true
            });

	        FinishedLoading = false;
            FinishedNavigating = false;
            var loadCount = 0;
            var navigateCount = 0;
            using (var session = WebCore.CreateWebSession(new WebPreferences() { } ))
	        {
	            using (var view = WebCore.CreateWebView(1024, 768, session))
	            {
	                PerformLoading(uri, view);
	                while (!FinishedLoading)
	                {
	                    Thread.Sleep(100);
	                    WebCore.Update();
	                    ++loadCount;
	                }

                    PerformNavigation(view);
                    while (!FinishedNavigating)
                    {
                        Thread.Sleep(100);
                        WebCore.Update();
                        ++navigateCount;
                    }

                    var surface = (BitmapSurface)view.Surface;
                    surface.SaveToPNG("result.png", true);
                }
	        }
            Console.WriteLine("Loaded for " + loadCount * 100 + " ms and navigated for " + navigateCount * 100 + " ms.");
	        WebCore.Shutdown();
            return null;
        }

		private void PerformLoading(Uri uri, WebView view)
		{
            view.DocumentReady += view_DocumentReady;
			view.LoadingFrameFailed += view_LoadingFrameFailed;
			view.LoadingFrameComplete += view_LoadingFrameComplete;
            view.Source = uri;
        }

        private void PerformNavigation(WebView view)
        {
            const string SetValue = "(function() { " +
                "var field = document.getElementById('txtQuotes'); " +
                "field.value = 'msft'; " +
                "var button = document.getElementById('btnQuotes'); " +
                "button.click(); " +
                "return 12; })();";
            var r = (int)view.ExecuteJavascriptWithResult(SetValue);
        }

		void view_LoadingFrameFailed(object sender, LoadingFrameFailedEventArgs e)
		{
		}

		void view_DocumentReady(object sender, UrlEventArgs e)
		{
		}

		private void view_LoadingFrameComplete(object sender, FrameEventArgs e)
		{
			if (e.IsMainFrame)
			{
				var view = (WebView) sender;
			    if (FinishedLoading)
			        FinishedNavigating = true;
                else
				    FinishedLoading = true;
			}
		}
    }
}
