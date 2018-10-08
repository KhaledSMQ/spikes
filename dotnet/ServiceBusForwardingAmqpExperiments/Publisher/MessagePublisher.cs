using System;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Shared;

namespace Publisher
{
	public class MessagePublisher
	{
		public event EventHandler<MessageSentEventArgs> MessageSent;

		private string TopicName { get; set; }
		private const string TimestampKey = "Timestamp";
		private const string PayloadKey = "Payload";

		private TopicClient Client { get; set; }

		public MessagePublisher(string topicName, string connectionString)
		{
		    TopicName = topicName;
			Client = TopicClient.CreateFromConnectionString(connectionString, TopicName);
		}

		public async Task SendMessage(Message message)
		{
			var bm = Create(message);
			await Client.SendAsync(bm);
            OnMessageSent(message);
        }

		private void OnMessageSent(Message message)
		{
			var handler = MessageSent;
			if (handler != null)
				handler(this, new MessageSentEventArgs(message));
		}

		private static BrokeredMessage Create(Message message)
		{
			var bm = new BrokeredMessage
				         {
					         Properties =
						         {
							         { TimestampKey, message.Timestamp },
									 { PayloadKey, message.Content },
                                     { "Color", message.Color }
						         }
				         };
			return bm;
		}
	}
}
