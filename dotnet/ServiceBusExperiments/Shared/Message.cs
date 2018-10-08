using System;

namespace Shared
{
	public class Message
	{
		public DateTime Timestamp { get; private set; }
		public string Content { get; private set; }

		public Message(string content, DateTime timestamp)
		{
			Content = content;
			Timestamp = timestamp;
		}
	}
}
