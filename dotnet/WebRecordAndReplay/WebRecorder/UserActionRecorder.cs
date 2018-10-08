using System;
using System.Collections.Generic;
using CefSharp.Wpf;

namespace WebRecorder
{
    public class UserActionRecorder
    {
		public event EventHandler<UserActionEventArgs> UserActionPlaying;
		public event EventHandler<UserActionEventArgs> UserActionPlayed;
		public event EventHandler FinishedPlaying;

        private IList<UserAction> RecordedActions { get; set; }
        private Queue<UserAction> PlayingActions { get; set; } 

        public WebView Browser { get; set; }

        public IEnumerable<UserAction> Actions
        {
            get { return RecordedActions; }
        }

        public UserActionRecorder()
        {
            RecordedActions = new List<UserAction>();
        }

        public void Clear()
        {
            RecordedActions.Clear();
        }

        public void Load(IEnumerable<UserAction> userActions)
        {
			((List<UserAction>)RecordedActions).AddRange(userActions);
		}

        public void Record(UserAction userAction)
        {
			RecordedActions.Add(userAction);
        }

        public void PlayAll()
        {
            PlayingActions = new Queue<UserAction>(RecordedActions);
            PlayNext();
        }

        public void StopPlaying()
        {
            PlayingActions.Clear();
        }

        public void Play(UserAction userAction)
        {
			OnUserActionPlaying(userAction);
			Subscribe(userAction);
            try
            {
                userAction.Execute(Browser);
            }
            catch (Exception)
            {
                Unsubscribe(userAction);
                throw;
            }
        }

        private void PlayNext()
        {
            if (PlayingActions.Count == 0)
            {
                OnFinishedPlaying();
                return;
            }

            var userAction = PlayingActions.Dequeue();
            Play(userAction);
        }

        private void Subscribe(UserAction userAction)
        {
            userAction.ExecutedUserAction += ExecutedUserAction;
        }

        private void Unsubscribe(UserAction userAction)
        {
            userAction.ExecutedUserAction -= ExecutedUserAction;
        }

        private void ExecutedUserAction(object sender, EventArgs e)
        {
            var userAction = (UserAction) sender;
            Unsubscribe(userAction);
            OnUserActionPlayed(userAction);
            PlayNext();
        }

		private void OnUserActionPlaying(UserAction userAction)
		{
			var handler = UserActionPlaying;
			if (handler != null)
				handler(this, new UserActionEventArgs(userAction.Type, userAction));
		}

        private void OnUserActionPlayed(UserAction userAction)
        {
            var handler = UserActionPlayed;
            if (handler != null) 
                handler(this, new UserActionEventArgs(userAction.Type, userAction));
        }

        private void OnFinishedPlaying()
        {
            var handler = FinishedPlaying;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
	}
}
