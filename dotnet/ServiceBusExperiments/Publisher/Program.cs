using System;
using System.Linq;
using Shared;

namespace Publisher
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Publisher is starting...");
			var publisher = new MessagePublisher();
			Console.WriteLine("Publisher started.");

			foreach (var i in Enumerable.Range(1, 5))
			{
				var message = "Hello there! This is message " + i;
				Console.WriteLine("Sending message " + i + "...");
				publisher.SendMessage(new Message(message, DateTime.UtcNow));
				Console.WriteLine("Done.");
			}

			Console.WriteLine("Sent all messages.");
			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();
		}
	}
}
