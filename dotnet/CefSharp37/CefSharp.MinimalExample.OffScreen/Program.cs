// Copyright © 2010-2014 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;
using System.IO;
using System.Linq;
using CefSharp.OffScreen;

namespace CefSharp.MinimalExample.OffScreen
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("This example application will load a URL, take a screenshot, and save it to your desktop.");
			Console.WriteLine("You may see Chromium debugging output, please wait...");
			Console.WriteLine();

			Cef.Initialize(new CefSettings());

			foreach (var n in Enumerable.Range(1, 3))
			{
				foreach (var i in Enumerable.Range(1, 4))
				{
					LoadUrlIntoNewBrowser("https://www.google.com/");
					LoadUrlIntoNewBrowser("http://www.yahoo.com");
					LoadUrlIntoNewBrowser("http://www.spikesco.com");
				}
				Console.WriteLine("Batch {0} complete. Press any key to continue", n);
				Console.ReadKey();
			}

			// We have to wait for something, otherwise the process will exit too soon.
			Console.WriteLine("All done. Press any key to exit.");
			Console.ReadKey();

			// Clean up Chromium objects.  You need to call this in your application otherwise
			// you will get a crash when closing.
			Cef.Shutdown();
		}

		private static void LoadUrlIntoNewBrowser(string url)
		{
			// Create the offscreen Chromium browser.
			var browser = new ChromiumWebBrowser();

			// An event that is fired when the first page is finished loading.
			// This returns to us from another thread.
			browser.FrameLoadEnd += BrowserFrameLoadEnd;

			// Start loading the test URL in Chrome's thread.
			browser.Load(url);
		}

		private static void TakeScreenshot(ChromiumWebBrowser browser, string filename)
		{
			var task = browser.ScreenshotAsync();
			task.Wait();

			var screenshotPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);

			Console.WriteLine("Screenshot ready. Saving to {0}", screenshotPath);
			task.Result.Save(screenshotPath);
			Console.WriteLine("Screenshot saved.");

			task.Result.Dispose();
		}

		private static void SaveContent(ChromiumWebBrowser browser, string filename)
		{
			var task = browser.EvaluateScriptAsync("document.documentElement.outerHTML");
			task.Wait();
			var jsresponse = task.Result;
			var jsresult = jsresponse.Result;

			var contentPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), filename);

			Console.WriteLine("Content ready. Saving to {0}", contentPath);
			File.WriteAllText(contentPath, jsresult.ToString());
			Console.WriteLine("Content saved.");
		}

		private static void BrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
		{
			var browser = sender as ChromiumWebBrowser;
			// Check to ensure it is the main frame which has finished loading
			// (rather than an iframe within the main frame).
			if (e.IsMainFrame)
			{
				// Remove the load event handler, because we only want one snapshot of the initial page.
				browser.FrameLoadEnd -= BrowserFrameLoadEnd;

				// Make a file to save it to (e.g. C:\Users\jan\Desktop\CefSharp screenshot.png)
				var ticks = DateTime.UtcNow.Ticks;
				var screenshotFilename = string.Format("CefSharp screenshot {0}.png", ticks);
				var contentFilename = string.Format("CefSharp content {0}.html", ticks);

				TakeScreenshot(browser, screenshotFilename);
				SaveContent(browser, contentFilename);

				browser.Dispose();
			}
		}
	}
}
