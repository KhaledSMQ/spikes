using System;
using Shared;

namespace Producer
{
	public class MessageSentEventArgs : EventArgs
	{
		public Message Message { get; private set; }

		public MessageSentEventArgs(Message message)
		{
			Message = message;
		}
	}
}
