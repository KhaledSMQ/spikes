using System;

namespace SamlTokenRequestor.Wif
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting SamlTokenRequestor.Wif...");
			var str = new SamlTokenRequestor();
			str.GetToken();
			Console.WriteLine("Done. Press any key to exit.");
			Console.ReadKey();
		}
	}
}
