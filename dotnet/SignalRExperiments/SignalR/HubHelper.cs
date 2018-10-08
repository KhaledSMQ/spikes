using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRWrapper
{
	public static class HubHelper
	{
		public static void Send<TMessage>(IClientProxy proxy, string method, TMessage message)
		{
			proxy.Invoke(method, message);
		}

		public static Hub Resolve<T>(string hubName = null)
			where T : Hub
		{
			var hm = new DefaultHubManager(GlobalHost.DependencyResolver);
			if (hubName == null)
				hubName = typeof(T).Name;
			var hub = hm.ResolveHub(hubName) as T;
			return hub;
		}

		public static IHubContext GetHubContext<T>()
			where T : Hub
		{
			var context = GlobalHost.ConnectionManager.GetHubContext<T>();
			return context;
		}
	}
}
