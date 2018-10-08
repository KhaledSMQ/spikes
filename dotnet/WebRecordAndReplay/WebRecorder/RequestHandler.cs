using CefSharp;

namespace WebRecorder
{
	public class RequestHandler : IRequestHandler
	{
		public bool GetAuthCredentials(IWebBrowser browser, bool isProxy, string host, int port, string realm, string scheme, ref string username, ref string password)
		{
			return false;
		}

		public bool GetDownloadHandler(IWebBrowser browser, string mimeType, string fileName, long contentLength, ref IDownloadHandler handler)
		{
			return false;
		}

		public bool OnBeforeBrowse(IWebBrowser browser, IRequest request, NavigationType navigationType, bool isRedirect)
		{
		    var message = string.Format("before browse - current: {0}, is loading: {1}, navigation type: {2}, is redirecting: {3}, request: {4} {5}",
                browser.Address, browser.IsLoading, navigationType, isRedirect, request.Method, request.Url);
		    browser.OnConsoleMessage(message, "request handler", 0);
            browser.AddRequest(request);
			return false;
		}

		public bool OnBeforeResourceLoad(IWebBrowser browser, IRequestResponse requestResponse)
		{
			return false;
		}

		public void OnResourceResponse(IWebBrowser browser, string url, int status, string statusText, string mimeType, System.Net.WebHeaderCollection headers)
		{
		}
	}
}
