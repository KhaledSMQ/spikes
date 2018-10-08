using System;
using System.Configuration;
using Consumer;
using Shared;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Receiver is starting...");
            var consumer1 = new MessageConsumer(ConfigurationManager.AppSettings["q1.Path"], ConfigurationManager.AppSettings["q1.connectionString"]);
            consumer1.MessageReceived += consumer1_MessageReceived;
            Console.WriteLine("Consumer1 started.");
            var consumer2 = new MessageConsumer(ConfigurationManager.AppSettings["q2.Path"], ConfigurationManager.AppSettings["q2.connectionString"]);
            consumer2.MessageReceived += consumer2_MessageReceived;
            Console.WriteLine("Consumer2 started.");

            Console.WriteLine("Waiting for messages...");
            consumer1.StartReceiving();
            consumer2.StartReceiving();

            Console.WriteLine("Press Enter to stop receiving.");
            var input = Console.ReadLine();
            consumer1.StopReceiving();
            Console.WriteLine("Consumer 1 stopped receiving.");
            consumer2.StopReceiving();
            Console.WriteLine("Consumer 2 stopped receiving.");

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }

        static void consumer1_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Print("1", e.Message);
        }

        static void consumer2_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            Print("2", e.Message);
        }

        static void Print(string consumer, Message message)
        {
            Console.WriteLine($"Consumer {consumer} received message with color {message.Color}");
        }
    }
}
