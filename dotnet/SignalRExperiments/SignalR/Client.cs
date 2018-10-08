using System;
using System.Collections.Generic;
using System.Diagnostics;

/*namespace SignalRWrapper
{
	public class Client
	{
		public string Name { get; private set; }
		private IDictionary<string, ClientChannel> ClientChannels { get; set; }

		public Client(string name)
		{
			Name = name;
			ClientChannels = new Dictionary<string, ClientChannel>();
		}

		public void AddChannel(ClientChannel channel)
		{
			ClientChannels[channel.Name] = channel;
		}

		public void Start()
		{
			try
			{
				Trace.TraceInformation("Client {0} is starting...", Name);
				foreach (var channelName in ClientChannels.Keys)
				{
					var channel = ClientChannels[channelName];
					channel.Start();
				}
			}
			catch (Exception e)
			{
				Trace.TraceError("There was a problem starting client {0}. {1}", Name, e);
			}
		}
	}
}
*/