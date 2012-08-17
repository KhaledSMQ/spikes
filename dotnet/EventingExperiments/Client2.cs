using System;

namespace EventingExperiments
{
    public class Client2
    {
        private EventHolder Events { get; set; }

        public Client2(EventHolder events)
        {
            Events = events;
            AddInitialize();
            AddTerminate();
        }

        public void AddInitialize()
        {
            Events.Initialize += InitializeHandler;
        }

        public void AddTerminate()
        {
            Events.Terminate += TerminateHandler;
        }

        public void RemoveInitialize()
        {
            Events.Initialize -= InitializeHandler;
        }

        public void RemoveTerminate()
        {
            Events.Terminate -= TerminateHandler;
        }

        private void InitializeHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Client2 on Initialize...");
        }

        private void TerminateHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Client2 on Terminate...");
        }
    }
}
