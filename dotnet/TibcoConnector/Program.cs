using System;

namespace TibcoSample
{
    class Program
    {
        static void Main(string[] args)
        {
			Console.WriteLine("Starting TibcoConnector...");

            var connector = new TibcoConnector();
            connector.Init();
            connector.Start();

			Console.WriteLine("Started TibcoConnector. Press Return to stop.");

            Console.ReadKey();

			Console.WriteLine("Stopping TibcoConnector...");
			connector.Stop();
			Console.WriteLine("TibcoConnector stopped.");

			Console.ReadKey();
			Console.WriteLine("Started TibcoConnector. Press Return to exit.");	
		}
    }
}
