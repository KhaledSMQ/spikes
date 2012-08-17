using System;
using System.Windows;
using System.Windows.Threading;

namespace AsyncWrappers
{
    public static class AsyncHelper
    {
        public static AsyncCallback DispatchExceptions(Action<IAsyncResult> callback)
        {
            AsyncCallback result = delegate(IAsyncResult ar)
            {
                try
                {
                    callback(ar);
                }
                catch (Exception e)
                {
                    Application.Current.Dispatcher.Invoke(
                        DispatcherPriority.Send,
                        (DispatcherOperationCallback)(arg => { throw e; }), null);
                }
            };
            return result;
        }
    }
}
