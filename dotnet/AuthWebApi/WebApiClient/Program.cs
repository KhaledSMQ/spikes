using System;
using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;

namespace WebApiClient
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Client starting...");
			var client = new HttpClient();
			string token = null;
			Console.WriteLine("Client started.");
			Console.WriteLine("Press any key to send a message to the server or X to terminate.");
			var input = Console.ReadKey();
			while (true)
			{
				if (input.Key == ConsoleKey.X)
					break;

				if (token != null)
				{
					var header = new AuthenticationHeaderValue("JWT", token);
					client.DefaultRequestHeaders.Authorization = header;
				}

				var task = client.GetAsync("http://localhost:9500/Hello");
				var response = task.Result;

				if (response.StatusCode == HttpStatusCode.Unauthorized)
				{
					Console.WriteLine("Unauthorized access. Please type your credentials:");
					Console.Write("Username? ");
					var username = Console.ReadLine();
					Console.Write("Password? ");
					var password = Console.ReadLine();
					var authenticator = new IdSrvAuthenticator();
					try
					{
						token = authenticator.Authenticate(username, password);						
					}
					catch (Exception e)
					{
						Console.WriteLine("Error while authenticating: " + e);
					}

					if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
						Console.WriteLine("User authenticated successfully.");
					else
						Console.WriteLine("Authentication failed.");

					continue;
				}

				try
				{
					response.EnsureSuccessStatusCode();
					var content = response.Content.ReadAsStringAsync().Result;
					Console.WriteLine(content);
				}
				catch (Exception e)
				{
					Console.WriteLine("Response from server was not successful: " + e);
				}

				input = Console.ReadKey();
			}

			Console.WriteLine("Client terminated. Press any key to exit.");
			Console.ReadKey();
		}
	}
}
