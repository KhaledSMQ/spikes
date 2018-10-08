using System.Collections.Generic;
using CefSharp;

namespace WebRecorder
{
    public static class WebBrowserExtensions
    {
        private static List<IRequest> Requests { get; set; }
        private static UserAction LastAction { get; set; }
 
        public static void AddRequest(this IWebBrowser webBrowser, IRequest request)
        {
            if (Requests == null)
                Requests = new List<IRequest>();
            Requests.Add(request);
        }

        public static IRequest GetRequest(this IWebBrowser webBrowser)
        {
            return Requests[0];
        }

        public static void ClearRequests(this IWebBrowser webBrowser)
        {
            Requests.Clear();
        }

        public static void SetAction(this IWebBrowser webBrowser, UserAction userAction)
        {
            LastAction = userAction;
        }

        public static UserAction GetLastAction(this IWebBrowser webBrowser)
        {
            return LastAction;
        }
    }
}
