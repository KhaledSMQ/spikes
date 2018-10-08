using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace Manager
{
	public class Manager
	{
		public Uri Address { get; set; }

		private NamespaceManager Client { get; set; }

		public Manager()
		{
			Client = NamespaceManager.Create();
			Address = Client.Address;
		}

		public async Task<IEnumerable<QueueInformation>> GetQueues()
		{
			var queues = await Client.GetQueuesAsync();
			var queueInfos = queues.Select(q => new QueueInformation { Path = q.Path, MessageCount = q.MessageCount }).ToList();
			return queueInfos;			
		}

		public async Task<IEnumerable<TopicInformation>> GetTopics()
		{
			var topics = await Client.GetTopicsAsync();
			var topicInfos = new List<TopicInformation>();
			foreach (var topic in topics)
			{
				var subscriptions = await GetSubscriptions(topic.Path);
				var topicInfo = new TopicInformation { Path = topic.Path, Subscriptions = subscriptions };
				topicInfos.Add(topicInfo);
			}
			return topicInfos;
		}

		public async Task<IEnumerable<SubscriptionInformation>> GetSubscriptions(string topicPath)
		{
			var subscriptions = await Client.GetSubscriptionsAsync(topicPath);
			var subscriptionInfos =
				subscriptions.Select(s => new SubscriptionInformation { Name = s.Name, MessageCount = s.MessageCount });
			return subscriptionInfos;
		}

		public async Task<SubscriptionInformation> CreateSubscription(string topicPath, string subscriptionName)
		{
			var description = new SubscriptionDescription(topicPath, subscriptionName)
			                  {
				                  DefaultMessageTimeToLive =
					                  TimeSpan.FromMinutes(10)
			                  };
			var created = await Client.CreateSubscriptionAsync(description);
			var subscriptionInfo = new SubscriptionInformation { Name = created.Name, MessageCount = created.MessageCount };
			return subscriptionInfo;
		}

		public async Task DeleteSubscription(string topicPath, string subscriptionName)
		{
			await Client.DeleteSubscriptionAsync(topicPath, subscriptionName);
		}
	}
}
