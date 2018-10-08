using System;
using System.Configuration;

namespace Host
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting simple host...");
			var host = new SimpleHost("simple host", ConfigurationManager.AppSettings["HostAddress"]);
			host.Initialize();
			host.Open();
			Console.WriteLine("Simple host started. Press <Enter> to exit.");
			Console.ReadLine();
			host.Close();
		}
	}
}
