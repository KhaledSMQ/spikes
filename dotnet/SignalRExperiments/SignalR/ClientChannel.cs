using System;
using System.Collections.Generic;
using System.Diagnostics;
/*using Microsoft.AspNet.SignalR.Client.Hubs;

namespace SignalRWrapper
{
	public class ClientChannel
	{
		public Uri BaseAddress { get; private set; }
		public string Name { get; private set; }

		private IDictionary<string, Action<dynamic>> Handlers { get; set; }
		private HubConnection Connection { get; set; }
		private IHubProxy Proxy { get; set; }
		private int RetryAttempt { get; set; }
		private const int MaxRetryCount = 3;
		private const int RetryInterval = 5;

		public ClientChannel(string name, Uri baseAddress)
		{
			Name = name;
			BaseAddress = baseAddress;
			Handlers = new Dictionary<string, Action<dynamic>>();
		}

		public void AddHandler(string eventName, Action<dynamic> handler)
		{
			Handlers[eventName] = handler;
		}

		public async void Start()
		{
			try
			{
				Connection = new HubConnection(BaseAddress.ToString());
				Proxy = Connection.CreateHubProxy(Name);
				Subscribe();
				try
				{
					Trace.TraceInformation("Connecting to hub on {0}...", BaseAddress);
					await Connection.Start();
				}
				catch (Exception e)
				{
					Trace.TraceError("There was an error opening the connection: {0}", e);
					throw;
				}

				Trace.TraceInformation("Connected. This client has an id of {0}.", Connection.ConnectionId);
			}
			catch (Exception e)
			{
				if (RetryAttempt < MaxRetryCount)
				{
					Trace.TraceError("There was an error initializing the connection or the proxy. Retrying in {0} seconds... {1}", RetryInterval, e);
					Retry();
				}
				else
					Trace.TraceError("The connection or the proxy could not be initialized. {0}", e);
			}
		}

		private void Retry()
		{
			RetryAttempt++;
			System.Threading.Thread.Sleep(RetryInterval * 1000);
			Start();
		}

		public async void SendMessage<TPayload>(string method, TPayload payload)
		{
			try
			{
				Trace.TraceInformation("Sending message '{0}' to {1}...", payload.ToString(), Name);
				await Proxy.Invoke<TPayload>(method, payload);
				Trace.TraceInformation("Message sent.");
			}
			catch (Exception e)
			{
				Trace.TraceError("Error sending message '{0}' to {1}. {2}", payload.ToString(), Name, e);
				throw;
			}
		}

		private void Subscribe()
		{
			foreach (var eventName in Handlers.Keys)
			{
				var handler = Handlers[eventName];
				Proxy.On(eventName, handler);
			}
		}
	}
}*/
