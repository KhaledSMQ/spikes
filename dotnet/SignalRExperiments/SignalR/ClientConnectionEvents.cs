using System;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRWrapper
{
	public static class ClientConnectionEvents
	{
		public static event EventHandler<EventArgs<HubCallerContext>> Connected;
		public static event EventHandler<EventArgs<HubCallerContext>> Disconnected;
		public static event EventHandler<EventArgs<HubCallerContext>> Reconnected;

		internal static void OnConnected(object sender, HubCallerContext context)
		{
			var handler = Connected;
			if (handler != null)
				handler(sender, new EventArgs<HubCallerContext>(context));
		}

		internal static void OnDisconnected(object sender, HubCallerContext context)
		{
			var handler = Disconnected;
			if (handler != null)
				handler(sender, new EventArgs<HubCallerContext>(context));
		}

		internal static void OnReconnected(object sender, HubCallerContext context)
		{
			var handler = Reconnected;
			if (handler != null)
				handler(sender, new EventArgs<HubCallerContext>(context));
		}
	}
}
