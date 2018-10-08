using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Receiver
{
    class Program
    {
        private const string ConnectionString = "Endpoint=sb://gensb-rt-dev.servicebus.windows.net/;SharedAccessKeyName=Listener;SharedAccessKey=wiJNj7S6njCdsgGp50lRq/DA4WZsM27HsPli5d9sgso=;EntityPath=int.q.reporting.reports";
        private static int _count = 0;

        static void Main(string[] args)
        {
            var client = QueueClient.CreateFromConnectionString(ConnectionString);
            client.OnMessage(OnMessageReceived);
            Console.WriteLine("Waiting for messages...");
            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        static void OnMessageReceived(BrokeredMessage message)
        {
            Console.WriteLine("Message received: " + message.GetBody<string>());
            Console.WriteLine(++_count);
        }
    }
}
