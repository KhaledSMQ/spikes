using System;

namespace WebRecorder
{
	public class ConsoleMessageEventArgs : EventArgs
	{
		public string Message { get; private set; }

		public ConsoleMessageEventArgs(string message)
		{
			Message = message;
		}
	}
}
