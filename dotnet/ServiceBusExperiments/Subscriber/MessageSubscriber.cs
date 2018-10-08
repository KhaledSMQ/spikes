using System;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Shared;

namespace Subscriber
{
	public class MessageSubscriber
	{
		public event EventHandler<MessageReceivedEventArgs> MessageReceived;

		private const string TopicName = "test.t";
		private const string SubscriptionName = "test.s";
		private const string TimestampKey = "Timestamp";
		private const string PayloadKey = "Payload";

		private SubscriptionClient Client { get; set; }

		public MessageSubscriber()
		{
			Client = SubscriptionClient.Create(TopicName, SubscriptionName);
            Client.AddRule(new RuleDescription("testrule"));
		}

		public void StartReceiving()
		{
			Client.OnMessageAsync(ReceiveAsync);
		}

		public void StopReceiving()
		{
			Client.Close();
		}

		private Task ReceiveAsync(BrokeredMessage bm)
		{
			var task = Task.Run(() =>
				                    {
					                    var message = Create(bm);
					                    OnMessageReceived(message);
				                    });
			return task;
		}

		private static Message Create(BrokeredMessage bm)
		{
			var timestamp = (DateTime)bm.Properties[TimestampKey];
			var content = (string)bm.Properties[PayloadKey];
			var message = new Message(content, timestamp);
			return message;
		}

		private void OnMessageReceived(Message message)
		{
			var handler = MessageReceived;
			if (handler != null)
				handler(this, new MessageReceivedEventArgs(message));
		}
	}
}
