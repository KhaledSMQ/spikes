using System;

namespace EventingExperiments
{
    public class EventHolder
    {
        private EventHandler _terminateDelegate;

        public event EventHandler Initialize;
        public event EventHandler Terminate
        {
            add
            {
                _terminateDelegate = (EventHandler)Delegate.Combine(value, _terminateDelegate);
            }
            remove
            {
                _terminateDelegate = (EventHandler) Delegate.Remove(_terminateDelegate, value);
            }
        }

        internal void OnInitialize()
        {
            var handler = Initialize;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        internal void OnTerminate()
        {
            var handler = _terminateDelegate;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}
