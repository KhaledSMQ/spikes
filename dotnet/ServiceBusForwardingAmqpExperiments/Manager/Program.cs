using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Manager
{
	class Program
	{
		static void Main(string[] args)
		{
			/*var p = new Program();
			p.Run();
			Console.WriteLine("Press any key to exit.");
			Console.ReadLine();*/
		}

		/*private async void Run()
		{
			Console.WriteLine("Manager starting...");
			var manager = new Manager();
			Console.WriteLine("Manager started.");
			Console.WriteLine("Connected to address " + manager.Address);
			Console.WriteLine("Getting queue information...");
			var queues = await manager.GetQueues();
			PrintQueues(queues);
			Console.WriteLine("Getting topic information...");
			var topics = await manager.GetTopics();
			PrintTopics(topics);
			Console.WriteLine("Creating subscription test.s2 on topic test.t...");
			await manager.CreateSubscription("test.t", "test.s2");
			Console.WriteLine("Subscription created. Will wait 30 seconds before deleting...");
			foreach (var i in Enumerable.Range(0, 29))
			{
				Thread.Sleep(1000);
				Console.Write(29 - i + "... ");
			}
			Console.WriteLine();
			Console.WriteLine("Deleting subscription test.s2 on topic test.t...");
			await manager.DeleteSubscription("test.t", "test.s2");
			Console.WriteLine("Subscription deleted.");
			Console.WriteLine("Press any key to exit.");
		}

		private void PrintTopics(IEnumerable<TopicInformation> obj)
		{
			Console.WriteLine("Topic information received.");
			Console.WriteLine("This namespace has the following topics and subscriptions:");
			foreach (var topicInfo in obj)
			{
				Console.WriteLine("  " + topicInfo.Path + " has " + topicInfo.Subscriptions.Count() + " subscriptions:");
				foreach(var subscriptionInfo in topicInfo.Subscriptions)
					Console.WriteLine("    " + subscriptionInfo.Name + ", " + subscriptionInfo.MessageCount);
			}
		}

		private void PrintQueues(IEnumerable<QueueInformation> obj)
		{
			Console.WriteLine("Queue information received.");
			Console.WriteLine("This namespace has the following queues and pending messages:");
			foreach(var queueInfo in obj)
				Console.WriteLine("  " + queueInfo.Path + ", " + queueInfo.MessageCount);
		}*/
	}
}
