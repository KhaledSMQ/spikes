using System;

namespace WebApiServer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Host starting...");
			var api = new ApiHost(new Uri("http://localhost:9500"));
			api.Open();
			Console.WriteLine("Host started.");
			Console.WriteLine("Press any key to stop it.");
			Console.ReadKey();
			api.Close();
			Console.WriteLine("Host stopped.");
			Console.WriteLine("Press any key to exit.");
			Console.ReadKey();
		}
	}
}
