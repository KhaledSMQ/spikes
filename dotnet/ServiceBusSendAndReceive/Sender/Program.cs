using System;
using System.Linq;
using Microsoft.ServiceBus.Messaging;

namespace Sender
{
    class Program
    {
        private const string ConnectionString =
            "Endpoint=sb://gensb-rt-dev.servicebus.windows.net/;SharedAccessKeyName=Sender;SharedAccessKey=CttjLlFhc9f3Ct5BmIrN30yqDPuKun1i2kro/1tycgk=;EntityPath=int.q.reporting.reports";
        
        static void Main(string[] args)
        {
            var client = QueueClient.CreateFromConnectionString(ConnectionString);
            foreach (var i in Enumerable.Range(1, 10000))
            {
                var message =
                    new BrokeredMessage("Hello World " + i)
                    {
                        ScheduledEnqueueTimeUtc = DateTime.UtcNow.AddMinutes(10)
                    };
                client.Send(message);
            }

            Console.WriteLine("Sent messages.");
        }
    }
}
