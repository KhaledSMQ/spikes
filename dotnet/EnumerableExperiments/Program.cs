using System;

namespace EnumerableExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Starting...");
			var p = new Program();
			p.Run();
			Console.WriteLine("Finished.");
			Console.ReadLine();
		}

		private void Run()
		{
			var things = new Things();
			foreach(var t in things)
				Console.WriteLine(t + "\t" + t.GetType());
		}
	}
}
