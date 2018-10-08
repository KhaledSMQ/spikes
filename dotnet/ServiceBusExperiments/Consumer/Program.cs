using System;

namespace Consumer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Consumer is starting...");
			var consumer = new MessageConsumer();
			consumer.MessageReceived += consumer_MessageReceived;
			Console.WriteLine("Consumer started.");

			Console.WriteLine("Waiting for messages...");
			consumer.StartReceiving();
				
			Console.WriteLine("Press Enter to stop receiving.");
			var input = Console.ReadLine();
			consumer.StopReceiving();
			Console.WriteLine("Consumer stopped receiving.");

			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();
		}

		static void consumer_MessageReceived(object sender, MessageReceivedEventArgs e)
		{
			Console.WriteLine("Received message at {0}", DateTime.UtcNow);
			Console.WriteLine("Message is:");
			Console.WriteLine("  Timestamp: {0}", e.Message.Timestamp);
			Console.WriteLine("  Content: {0}", e.Message.Content);
		}
	}
}
