using System;
using System.Threading;
using TIBCO.EMS;

namespace TibcoSample
{
    class MyMessageListener : IMessageListener
    {
	    public static int Counter;
	    public DateTime Start { get; set; }

	    public MyMessageListener()
	    {
		    Start = DateTime.UtcNow;
	    }

        public void OnMessage(Message m)
        {
	        Interlocked.Increment(ref Counter);

	        var textMessage = m as TextMessage;
	        if (textMessage != null)
	        {
		        Console.WriteLine("Received TextMessage: {0}", textMessage.Text);
		        return;
	        }

	        var objectMessage = m as ObjectMessage;
	        if (objectMessage != null)
	        {
				Console.WriteLine("Received ObjectMessage: {0}", objectMessage);
		        return;
	        }

			Console.WriteLine("Received message: {0}", m);
        }
    }

}
