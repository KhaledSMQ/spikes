using System;

namespace JwtTokenRequestor.Wif
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting JwtTokenRequestor.Wif...");
			var str = new JwtTokenRequestor();
			str.GetToken();
			Console.WriteLine("Done. Press any key to exit.");
			Console.ReadKey();
		}
	}
}
