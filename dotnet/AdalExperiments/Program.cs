using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AdalExperiments
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			const string aadInstance = "https://login.windows.net/{0}";
			const string tenant = "genrt.onmicrosoft.com";
			
			var authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

			var tokenCache = new LocalFileTokenCache();
			var authContext = new AuthenticationContext(authority, tokenCache);

			//const string targetAppIdUri = "https://genrt.onmicrosoft.com/rtservices";
			const string targetAppIdUri = "http://localhost:50926";
			const string clientId = "1f54957f-0309-47c6-883c-81956fb95751";
			var redirectUri = new Uri("http://NativeClientSample/");

			tokenCache.Clear();
			ClearCookies();

			AuthenticationResult result = null;

			/*var needsSignIn = false;

			try
			{
				var uc = new UserCredential("pmouat@genrt.onmicrosoft.com", "1dentity!2");
				result = authContext.AcquireToken(targetAppIdUri, clientId, uc);
				//result = authContext.AcquireToken(targetAppIdUri, clientId, redirectUri, PromptBehavior.Never);
			}
			catch (AdalException e)
			{
				if (e.ErrorCode == "user_interaction_required" || e.ErrorCode == "invalid_grant")
				{
					needsSignIn = true;
				}
				else
				{
					Console.WriteLine(e);
					throw;
				}
			}*/

			//if (needsSignIn)
			{
				try
				{
					Console.WriteLine("Username?");
					var username = Console.ReadLine();
					Console.WriteLine("Password?");
					var password = Console.ReadLine();
					var uc = new UserCredential(username, password);
					result = authContext.AcquireToken(targetAppIdUri, clientId, uc);
					//result = authContext.AcquireToken(targetAppIdUri, clientId, redirectUri, PromptBehavior.Always);
				}
				catch (AdalException e)
				{
					if (e.ErrorCode == "authentication_canceled")
					{
						Console.WriteLine("Sign in was canceled by the user");
					}
					else
					{
						Console.WriteLine(e);
						throw;
					}
				}

				var httpClient = new HttpClient();

				httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

				//const string targetServiceUri = "https://genrtentitydata.azurewebsites.net/api/plants";
				const string targetServiceUri = "http://localhost:50926/api/values";


				var response = httpClient.GetAsync(targetServiceUri).Result;

				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Response: " + response);
					var content = response.Content.ReadAsStringAsync().Result;
					Console.WriteLine("Content: " + content);
				}
				else
				{
					Console.WriteLine("An error occurred: " + response.ReasonPhrase);
				}
			}

			ClearCookies();
			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();
		}

		// This function clears cookies from the browser control used by ADAL.
		private static void ClearCookies()
		{
			const int INTERNET_OPTION_END_BROWSER_SESSION = 42;
			InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
		}

		[DllImport("wininet.dll", SetLastError = true)]
		private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);
	}
}
