using System.Collections.Generic;

namespace Manager
{
	public class TopicInformation
	{
		public string Path { get; set; }
		public IEnumerable<SubscriptionInformation> Subscriptions { get; set; }
	}
}
