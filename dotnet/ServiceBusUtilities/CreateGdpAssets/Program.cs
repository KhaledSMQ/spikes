using System;
using System.Collections.Generic;
using System.Management.Instrumentation;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using AuthorizationRule = Microsoft.ServiceBus.Messaging.AuthorizationRule;

namespace CreateGdpAssets
{
	class Program
	{
		private readonly string[][] Namespaces =
			{
				new [] { "old-dev", "Endpoint=sb://spikecodatapump-dev.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=Pn9VysAF8udoo9ay/EvP79uUCuryu8Ir3UyQAbXpgIc=" },
				//new [] { "old-qa", "Endpoint=sb://spikecodatapump-qa.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=/8sUa33tvCBhbNTtHuoAv1ANgxkEdujgqrqe7+/odU8=" },
				//new [] { "old-prod", "Endpoint=sb://spikecodatapump-prod.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=wezGYoP9+ZDCThiheYMVnPK4XSsF0UYbP/czXyQOwjo=" },
				new [] { "old-apasca", "Endpoint=sb://spikecodatapump-apasca.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=S+8GlXi/8N/kUgh54Dw2p/+84YldkES4UgmFN4/vH0o=" },
				new [] { "old-iantoniu", "Endpoint=sb://spikecodatapump-iantoniu.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=aFQ8bPGZDlnW2dtkX4j7qXG3bDcMLfZ8oQfXRYhdKoo=" },
				new [] { "old-schernev", "Endpoint=sb://spikecodatapump-schernev.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=nvif4Wdt13TDw6/QHMkA29OpVDJqkxmjv1nZdbDmM2A=" },
				new [] { "old-ssperanta", "Endpoint=sb://spikecodatapump-ssperanta.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=/8vou+a0JGfNI89aa7s1QvRfFsggTKXQ5DezOB5M4yo=" },

				new [] { "dev", "Endpoint=sb://spikeco-gdp-dev.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=GOwvzJ7sFY8KYMD91JMFdBmabbKmYvPjjzO4WqP+A+k=" },
				new [] { "qa", "Endpoint=sb://spikeco-gdp-qa.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=frC0MaH8AyDl9NqHMQOg9D6mFj+X4OEQN+BKnIAk4pM=" },
				new [] { "prod", "Endpoint=sb://spikeco-gdp-prod.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=poBV7h/MNoaIfQmXXj7RCETy6+MqS9m8BaDIEWb30jE=" },
				new [] { "apasca", "Endpoint=sb://spikeco-gdp-apasca.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=q3rHbzoLxws7kMM7EBhWcsVGJbXmaGr50qz+5gBX7zU=" },
				new [] { "hgheorghiu", "Endpoint=sb://spikeco-gdp-hgheorghiu.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=JbZ+xu+3lnMzpnt5TEd2sAODZdkEgazIW2rLsMVl3jw=" },
				new [] { "iantoniu", "Endpoint=sb://spikeco-gdp-iantoniu.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=9NjrxaXyIw/kpFoGE25/dartziUwk8xgyVs1Q2Csclo=" },
				new [] { "schernev", "Endpoint=sb://spikeco-gdp-schernev.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=sPd5PlFOks7f4k4y3QhmE4bl0bJjmVGbaurMwdNY50o=" },
				new [] { "ssperanta", "Endpoint=sb://spikeco-gdp-ssperanta.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=vmV/Cf2OQKoUScW+eEG/7NhSNKrvkKN+EFqv8JP8IuI=" },
				new [] { "svijelie", "Endpoint=sb://spikeco-gdp-svijelie.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=nO9iGA/r4Olg0MLLAVUs1aYclO4k33BczA6JqFi1qDM=" }
			};

		private readonly Dictionary<string, string[]> TopicsAndSubscriptions =
			new Dictionary<string, string[]> {
				{ "old-dev", new[]
				{
					"ahofeldt_spikesco.com", "apasca_pentalog.fr", "ddeutsch_spikesco.com", "dxie_spikesco.com",
					"hgheorghiu_pentalog.fr", "iantoniu_pentalog.fr", "pmouat_spikesco.com", "schernev_pentalog.fr",
					"ssperanta_pentalog.fr", "svijelie_pentalog.fr"
				} },
				//new [] { "old-qa", new[]
				//{
				//	"ahofeldt_spikesco.com", "apasca_pentalog.fr", "ddeutsch_spikesco.com", "dxie_spikesco.com",
				//	"hgheorghiu_pentalog.fr", "iantoniu_pentalog.fr", "pmouat_spikesco.com", "schernev_pentalog.fr",
				//	"ssperanta_pentalog.fr", "svijelie_pentalog.fr"
				//} },
				//new [] { "old-prod", new[]
				//{
				//	"ahofeldt_spikesco.com", "apasca_pentalog.fr", "ddeutsch_spikesco.com", "dxie_spikesco.com",
				//	"hgheorghiu_pentalog.fr", "iantoniu_pentalog.fr", "pmouat_spikesco.com", "schernev_pentalog.fr",
				//	"ssperanta_pentalog.fr", "svijelie_pentalog.fr"
				//} },
				{ "old-apasca", new[]
				{
					"apasca_pentalog.fr"
				} },
				{ "old-iantoniu", new[]
				{
					"iantoniu_pentalog.fr"
				} },
				{ "old-schernev", new[]
				{
					"schernev_pentalog.fr"
				} },
				{ "old-ssperanta", new[]
				{
					"ssperanta_pentalog.fr"
				} },

				{ "dev", new[]
				{
					"ahofeldt_spikesco.com", "apasca_pentalog.fr", "ddeutsch_spikesco.com", "dxie_spikesco.com",
					"hgheorghiu_pentalog.fr", "iantoniu_pentalog.fr", "pmouat_spikesco.com", "schernev_pentalog.fr",
					"ssperanta_pentalog.fr", "svijelie_pentalog.fr"
				} },
				{ "qa", new[]
				{
					"ahofeldt_spikesco.com", "apasca_pentalog.fr", "ddeutsch_spikesco.com", "dxie_spikesco.com",
					"hgheorghiu_pentalog.fr", "iantoniu_pentalog.fr", "pmouat_spikesco.com", "schernev_pentalog.fr",
					"ssperanta_pentalog.fr", "svijelie_pentalog.fr"
				} },
				{ "prod", new[]
				{
					"ahofeldt_spikesco.com", "apasca_pentalog.fr", "ddeutsch_spikesco.com", "dxie_spikesco.com",
					"hgheorghiu_pentalog.fr", "iantoniu_pentalog.fr", "pmouat_spikesco.com", "schernev_pentalog.fr",
					"ssperanta_pentalog.fr", "svijelie_pentalog.fr"
				} },
				{ "apasca", new[]
				{
					"apasca_pentalog.fr"
				} },
				{ "hgheorghiu", new[]
				{
					"hgheorghiu_pentalog.fr"
				} },				
				{ "iantoniu", new[]
				{
					"iantoniu_pentalog.fr"
				} },
				{ "schernev", new[]
				{
					"schernev_pentalog.fr"
				} },
				{ "ssperanta", new[]
				{					
					"ssperanta_pentalog.fr"
				} },
				{ "svijelie", new[]
				{					
					"svijelie_pentalog.fr"
				} }
			};

		private NamespaceManager Manager { get; set; }

		static void Main(string[] args)
		{
			var p = new Program();
			if (args.Length > 0)
			{
				switch (args[0].ToLower())
				{
					case "create":
						p.CreateAssetsAcrossAllNamespaces();
						break;
					case "list":
						p.ListConnectionStringsForAllNamespaces();
						break;
					case "update":
						p.UpdateAssetsAcrossAllNamespaces();
						break;
				}
			}
		}

		private void ListConnectionStringsForAllNamespaces()
		{
			foreach (var @namespace in Namespaces)
			{
				try
				{
					Console.WriteLine("Connection strings for namespace {0}", @namespace[0]);
					ListConnectionStrings(@namespace);
					Console.WriteLine("Done listing connection strings for namespace {0}", @namespace[0]);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		private void ListConnectionStrings(string[] @namespace)
		{
			Initialize(@namespace);
			ListConnectionStringsForQueues(@namespace);
			ListConnectionStringsForTopics(@namespace);
		}

		private void CreateAssetsAcrossAllNamespaces()
		{
			foreach (var @namespace in Namespaces)
			{
				try
				{
					Console.WriteLine("Creating assets for namespace {0}", @namespace[0]);
					CreateAssets(@namespace);
					Console.WriteLine("Created assets for namespace {0}", @namespace[0]);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		private void CreateAssets(string[] @namespace)
		{
			Initialize(@namespace);
			CreateQueues(@namespace);
			CreateTopicsAndSubscriptions(@namespace);
		}


		private void UpdateAssetsAcrossAllNamespaces()
		{
			foreach (var @namespace in Namespaces)
			{
				try
				{
					Console.WriteLine("Updating assets for namespace {0}", @namespace[0]);
					UpdateAssets(@namespace);
					Console.WriteLine("Updated assets for namespace {0}", @namespace[0]);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		private void UpdateAssets(string[] @namespace)
		{
			UpdateQueues(@namespace);
			UpdateTopics(@namespace);
		}

		private void Initialize(string[] @namespace)
		{
			Manager = NamespaceManager.CreateFromConnectionString(@namespace[1]);
		}

		private void CreateQueues(string[] @namespace)
		{
			var queues = new[] { "engine.jobresults.q", "engine.jobs.q", "scheduler.jobs.q", "alerting.alerts.q" };

			foreach (var queue in queues)
			{
				try
				{
					Console.WriteLine("Creating queue {0}", queue);
					if (!Manager.QueueExists(queue))
					{
						var description = GetDefaultQueueDescription(queue);
						CreateQueue(description);
						Console.WriteLine("Created queue {0}", queue);
					}
					else
						Console.WriteLine("Queue {0} already exists", queue);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		private void UpdateQueues(string[] @namespace)
		{
			var queues = new[] { "engine.jobresults.q", "engine.jobs.q", "scheduler.jobs.q", "alerting.alerts.q" };

			foreach (var queue in queues)
			{
				try
				{
					Console.WriteLine("Updating queue {0}", queue);
					if (Manager.QueueExists(queue))
					{
						var q = Manager.GetQueue(queue);
						var rules = GetDefaultAuthorizationRules();
						foreach(var rule in rules)
							q.Authorization.Add(rule);
						Manager.UpdateQueue(q);
						Console.WriteLine("Updated queue {0}", queue);
					}
					else
						Console.WriteLine("Queue {0} does not exist", queue);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		private void ListConnectionStringsForQueues(string[] @namespace)
		{
			var queues = Manager.GetQueues();
			foreach(var q in queues)
				ListConnectionStrings(q);
		}

		private void CreateTopicsAndSubscriptions(string[] @namespace)
		{
			var topics = new[] { "client.updates.t" };
			var subscriptions = TopicsAndSubscriptions[@namespace[0]];

			foreach (var topic in topics)
			{
				try
				{
					Console.WriteLine("Creating topic {0}", topic);
					if (!Manager.TopicExists(topic))
					{
						var description = GetDefaultTopicDescription(topic);
						CreateTopic(description);
						Console.WriteLine("Created topic {0}", topic);
					}
					else
						Console.WriteLine("Topic {0} already exists", topic);

					foreach (var subscription in subscriptions)
					{
						try
						{
							Console.WriteLine("Creating subscription {0} on topic {1}", subscription, topic);
							if (!Manager.SubscriptionExists(topic, subscription))
							{
								var sdesc = GetDefaultSubscriptionDescription(topic, subscription);
								CreateSubscription(sdesc);
								Console.WriteLine("Created subscription {0} on topic {1}", subscription, topic);
							}
							else
								Console.WriteLine("Subscription {0} on topic {1} already exists", subscription, topic);
						}
						catch (Exception e)
						{
							Console.WriteLine(e);
						}
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		private void UpdateTopics(string[] @namespace)
		{
			var topics = new[] { "client.updates.t" };

			foreach (var topic in topics)
			{
				try
				{
					Console.WriteLine("Updating topic {0}", topic);
					if (Manager.TopicExists(topic))
					{
						var t = Manager.GetTopic(topic);
						var rules = GetDefaultAuthorizationRules();
						foreach (var rule in rules)
							t.Authorization.Add(rule);
						Manager.UpdateTopic(t);
						Console.WriteLine("Updated topic {0}", topic);
					}
					else
						Console.WriteLine("Topic {0} does not exist", topic);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		private void ListConnectionStringsForTopics(string[] @namespace)
		{
			var topics = Manager.GetTopics();
			foreach (var t in topics)
				ListConnectionStrings(t);
		}

		private void ListConnectionStrings(object d)
		{
			var connectionStrings = new List<string>();

			var q = d as QueueDescription;
			if (q != null)
			{
				connectionStrings.AddRange(BuildConnectionStrings(q.Path, Manager, q.Authorization));
			}

			var t = d as TopicDescription;
			if (t != null)
			{
				connectionStrings.AddRange(BuildConnectionStrings(t.Path, Manager, t.Authorization));
			}

			foreach (var connectionString in connectionStrings)
			{
				Console.WriteLine(connectionString);
			}
		}

		private static IEnumerable<string> BuildConnectionStrings(string path, NamespaceManager manager, IEnumerable<AuthorizationRule> rules)
		{
			var results = new List<string>();
			foreach (var rule in rules)
			{
				var sarule = rule as SharedAccessAuthorizationRule;
				if (sarule != null)
					results.Add(BuildConnectionString(path, manager.Address, sarule.KeyName, sarule.PrimaryKey));
			}

			return results;
		}

		private static string BuildConnectionString(string path, Uri address, string keyName, string key)
		{
			const string format = "{0},Endpoint={1};SharedAccessKeyName={2};SharedAccessKey={3}";
			var cs = string.Format(format, path, address, keyName, key);
			return cs;
		}

		private QueueDescription CreateQueue(QueueDescription description)
		{
			var result = Manager.CreateQueue(description);
			return result;
		}

		private TopicDescription CreateTopic(TopicDescription description)
		{
			var result = Manager.CreateTopic(description);
			return result;			
		}

		private SubscriptionDescription CreateSubscription(SubscriptionDescription description)
		{
			var result = Manager.CreateSubscription(description);
			return result;
		}

		private static QueueDescription GetDefaultQueueDescription(string path)
		{
			var description = new QueueDescription(path)
			{
				DefaultMessageTimeToLive = TimeSpan.FromMinutes(10),
				DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(5),
				LockDuration = TimeSpan.FromSeconds(30),
				MaxDeliveryCount = 10,
				Status = EntityStatus.Active
			};

			var rules = GetDefaultAuthorizationRules();
			foreach(var rule in rules)
				description.Authorization.Add(rule);

			return description;
		}

		private static TopicDescription GetDefaultTopicDescription(string path)
		{
			var description = new TopicDescription(path)
			{
				DefaultMessageTimeToLive = TimeSpan.FromMinutes(2),
				DuplicateDetectionHistoryTimeWindow = TimeSpan.FromMinutes(1),
				Status = EntityStatus.Active
			};
	
			var rules = GetDefaultAuthorizationRules();
			foreach (var rule in rules)
				description.Authorization.Add(rule);

			return description;
		}

		private static SubscriptionDescription GetDefaultSubscriptionDescription(string topicPath, string name)
		{
			var description = new SubscriptionDescription(topicPath, name)
			{
				DefaultMessageTimeToLive = TimeSpan.FromMinutes(2),
				LockDuration = TimeSpan.FromSeconds(30),
				MaxDeliveryCount = 10,				
				Status = EntityStatus.Active
			};
			return description;
		}

		private static IEnumerable<AuthorizationRule> GetDefaultAuthorizationRules()
		{
			var rules = new List<AuthorizationRule>
			{
				new SharedAccessAuthorizationRule("SendListenPolicy", new[] { AccessRights.Send, AccessRights.Listen })
			};

			return rules;
		}
	}
}
