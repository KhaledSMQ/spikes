using System;

namespace WebRecorder
{
	public class UserActionEventArgs : EventArgs
	{
		public string Type { get; private set; }
		public UserAction UserAction { get; private set; }

		public UserActionEventArgs(string type, UserAction userAction)
		{
			Type = type;
			UserAction = userAction;
		}
	}
}
