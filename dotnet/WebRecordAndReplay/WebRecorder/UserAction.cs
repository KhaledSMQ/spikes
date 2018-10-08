using System;
using System.Collections.Generic;
using CefSharp.Wpf;

namespace WebRecorder
{
	public class UserAction
	{
		public event EventHandler ExecutedUserAction;
		
        public string Type { get; private set; }
		
        private IDictionary<string, string> Parameters { get; set; }

		public virtual ICollection<string> Keys
		{
			get { return Parameters.Keys; }
		}

		public virtual string Id
		{
			get { return GetParameter(UserActionParameters.Id); }
			set { AddParameter(UserActionParameters.Id, value); }
		}

		public virtual string Path
		{
			get { return GetParameter(UserActionParameters.Path); }
			set { AddParameter(UserActionParameters.Path, value); }
		}

		public virtual string Selector
		{
			get { return GetParameter(UserActionParameters.Selector); }
			set { AddParameter(UserActionParameters.Selector, value); }
		}

        public UserAction(string type)
        {
            Type = type;
            Parameters = new Dictionary<string, string>();
        }

		public virtual void AddParameter(string key, string value)
		{
			Parameters[key] = value;
		}

		public virtual void AddParameters(IDictionary<string, string> parameters)
		{
			foreach (var key in parameters.Keys)
				Parameters[key] = parameters[key];
		}

		public virtual string GetParameter(string key)
		{
			string value;
			Parameters.TryGetValue(key, out value);
			return value;
		}

        public virtual void Execute(WebView browser)
        {
            OnExecutedUserAction();
        }

        protected virtual void OnExecutedUserAction()
        {
            var handler = ExecutedUserAction;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
	}
}
