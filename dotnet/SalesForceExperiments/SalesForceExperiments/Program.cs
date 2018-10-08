using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace SalesForceExperiments
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			/*var p = new Program();
			p.Run();
			Console.WriteLine("Press return to exit.");
			Console.ReadLine();*/
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

		/*
		private void Run()
		{
			var authenticator = new AuthenticationStrategy();
			var auth = authenticator.Authenticate();
			Console.WriteLine(auth.InstanceUrl);
			Console.WriteLine(auth.Token);
			Console.WriteLine(auth.TokenType);
			var listVersions = ListVersions(auth);
			var listResources = ListResources(auth);
			var listObjects = ListObjects(auth);
			Console.WriteLine(listVersions);
			Console.WriteLine(listResources);
			Console.WriteLine(listObjects);
		}

		private string ListVersions(AuthenticationResponse auth)
		{
			var resp = RunRequest(auth, "/services/data/");
			return resp;
		}

		private string ListResources(AuthenticationResponse auth)
		{
			var resp = RunRequest(auth, "/services/data/v31.0/");
			return resp;
		}

		private string ListObjects(AuthenticationResponse auth)
		{
			var resp = RunRequest(auth, "/services/data/v31.0/sobjects");
			return resp;
		}

		private string RunRequest(AuthenticationResponse auth, string path)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(auth.InstanceUrl);

				var header = new AuthenticationHeaderValue(auth.TokenType, auth.Token);
				client.DefaultRequestHeaders.Authorization = header;

				var result = client.GetStringAsync(path).Result;
				return result;
			}
		}*/
	}
}
