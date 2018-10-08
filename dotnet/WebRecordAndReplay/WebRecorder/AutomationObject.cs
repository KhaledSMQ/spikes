using System;

namespace WebRecorder
{
	public class AutomationObject
	{
		public event EventHandler<UserActionEventArgs> UserActionReceived;

		public void SetAction(string type, string actionParameters)
		{
			var userAction = UserActionFactory.Create(type, actionParameters);
			OnUserActionReceived(type, userAction);
		}

		private void OnUserActionReceived(string type, UserAction userAction)
		{
			var handler = UserActionReceived;
			if (handler != null)
				handler(this, new UserActionEventArgs(type, userAction));
		}
	}
}