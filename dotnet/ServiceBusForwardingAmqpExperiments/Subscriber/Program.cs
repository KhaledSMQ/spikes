using System;

namespace Subscriber
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Subscriber is starting...");
			var subscriber = new MessageSubscriber();
			subscriber.MessageReceived += subscriber_MessageReceived;
			Console.WriteLine("Subscriber started.");

			Console.WriteLine("Waiting for messages...");
			subscriber.StartReceiving();

			Console.WriteLine("Press Enter to stop receiving.");
			var input = Console.ReadLine();
			subscriber.StopReceiving();
			Console.WriteLine("Subscriber stopped receiving.");

			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();
		}

		static void subscriber_MessageReceived(object sender, MessageReceivedEventArgs e)
		{
			Console.WriteLine("Received message at {0}", DateTime.UtcNow);
			Console.WriteLine("Message is:");
			Console.WriteLine("  Timestamp: {0}", e.Message.Timestamp);
			Console.WriteLine("  Content: {0}", e.Message.Content);
		}
	}
}
