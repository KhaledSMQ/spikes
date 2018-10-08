using System;

namespace WebRecorder
{
	public class LogMessageEventArgs : EventArgs
	{
		public string Message { get; private set; }

		public LogMessageEventArgs(string message)
		{
			Message = message;
		}
	}
}
