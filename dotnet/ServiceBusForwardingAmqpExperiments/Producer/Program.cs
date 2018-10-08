using System;
using System.Linq;
using Shared;

namespace Producer
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Producer is starting...");
			var producer = new MessageProducer();
			Console.WriteLine("Producer started.");

			foreach (var i in Enumerable.Range(1, 50))
			{
				var message = "Hello there! This is message " + i;
				Console.WriteLine("Sending message " + i + "...");
				producer.SendMessage(new Message(message, DateTime.UtcNow));
				Console.WriteLine("Done.");
			}

			Console.WriteLine("Sent all messages.");
			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();
		}
	}
}
