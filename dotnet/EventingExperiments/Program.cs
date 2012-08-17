using System;

namespace EventingExperiments
{
    class Program
    {
        private EventHolder Events { get; set; }

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
            Events = new EventHolder();
            var c1 = new Client1(Events);
            var c2 = new Client2(Events);

            Console.WriteLine("Invoking all events...");
            InvokeEvents();

            c2.RemoveInitialize();
            Console.WriteLine("RemoveInitialize and invoking...");
            InvokeEvents();

            c2.AddInitialize();
            Console.WriteLine("AddInitialize and invoking...");
            InvokeEvents();

            c2.RemoveTerminate();
            Console.WriteLine("RemoveTerminate and invoking...");
            InvokeEvents();

            c2.AddTerminate();
            Console.WriteLine("AddTerminate and invoking...");
            InvokeEvents();
        }

        private void InvokeEvents()
        {
            Events.OnInitialize();
            Events.OnTerminate();
            
        }
    }
}
