using System;

namespace RxExperiments
{
	class Program
	{
		static void Main(string[] args)
		{
		    var se = new SimpleEnumerable();
		    se.Run();

			var example1 = new SimpleStringObservable();
			example1.Run();

			var example2 = new EventsVsObservables();
			example2.Run();

			var example3 = new ComplexEventProcessing();
			example3.Run();

			Console.WriteLine("Done. Press any key to exit.");
			Console.ReadLine();
		}
	}
}
