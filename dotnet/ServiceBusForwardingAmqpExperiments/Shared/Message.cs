using System;

namespace Shared
{
	public class Message
	{
		public DateTime Timestamp { get; private set; }
        public string Color { get; private set; }
		public string Content { get; private set; }

		public Message(string content, DateTime timestamp, string color)
		{
			Content = content;
		    Color = color;
			Timestamp = timestamp;
		}
	}
}
