using System;

namespace EventingExperiments
{
    public class Client1
    {
        public Client1(EventHolder events)
        {
            events.Initialize += InitializeHandler;
            events.Terminate += TerminateHandler;
        }

        private void InitializeHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Client1 on Initialize...");
        }

        private void TerminateHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Client1 on Terminate...");
        }
    }
}
