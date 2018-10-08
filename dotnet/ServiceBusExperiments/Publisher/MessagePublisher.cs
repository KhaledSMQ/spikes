using System;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Shared;

namespace Publisher
{
	public class MessagePublisher
	{
		public event EventHandler<MessageSentEventArgs> MessageSent;

		private const string TopicName = "test.t";
		private const string TimestampKey = "Timestamp";
		private const string PayloadKey = "Payload";

		private TopicClient Client { get; set; }

		public MessagePublisher()
		{
			Client = TopicClient.Create(TopicName);
		}

		public Task SendMessage(Message message)
		{
			var bm = Create(message);
			var task = Task.Factory.FromAsync(Client.BeginSend, EndSendMessage, bm, message);
			return task;
		}

		private void EndSendMessage(IAsyncResult result)
		{
			var message = result.AsyncState as Message;
			Client.EndSend(result);
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
									 { PayloadKey, message.Content }
						         }
				         };
			return bm;
		}
	}
}
