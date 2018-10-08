using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Shared;

namespace Consumer
{
	public class MessageConsumer
	{
		public event EventHandler<MessageReceivedEventArgs> MessageReceived;

		private const string QueueName = "test.q";
		private const string TimestampKey = "Timestamp";
		private const string PayloadKey = "Payload";
		private const int MaxConcurrentReceivers = 8;

		private QueueClient Client { get; set; }

		public MessageConsumer()
		{
			Client = QueueClient.Create(QueueName);
		}

		public void StartReceiving()
		{
			Client.OnMessage(Receive, GetMessageOptions());
			//Client.OnMessageAsync(ReceiveAsync);
		}

		public void StopReceiving()
		{
			Client.Close();
		}

		private void Receive(BrokeredMessage bm)
		{
			var message = Create(bm);
			OnMessageReceived(message);
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


		private OnMessageOptions GetMessageOptions()
		{
			var options = new OnMessageOptions
			{
				AutoComplete = true,
				MaxConcurrentCalls = MaxConcurrentReceivers
			};
			options.ExceptionReceived += MessagingExceptionReceived;
			return options;
		}

		private void MessagingExceptionReceived(object sender, ExceptionReceivedEventArgs e)
		{
			if (e.Exception != null)
			{
				Console.WriteLine("Error while receiving message from queue {0}: {1}", QueueName, e.Exception);
			}
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
			Thread.Sleep(TimeSpan.FromSeconds(10));
		}
	}
}
