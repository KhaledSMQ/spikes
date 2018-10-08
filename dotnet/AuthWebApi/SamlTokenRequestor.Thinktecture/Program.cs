using System;

namespace SamlTokenRequestor.Thinktecture
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			Console.WriteLine("Starting SamlTokenRequestor.Thinktecture...");
			var str = new SamlTokenRequestor();
			str.GetToken();
			Console.WriteLine("Done. Press any key to exit.");
			Console.ReadKey();
		}
	}
}
