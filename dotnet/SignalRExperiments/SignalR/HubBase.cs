using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalRWrapper
{
	public class HubBase : Hub
	{
		public override Task OnConnected()
		{
			ClientConnectionEvents.OnConnected(this, Context);
			return base.OnConnected();
		}

		public override Task OnDisconnected()
		{
			ClientConnectionEvents.OnDisconnected(this, Context);
			return base.OnDisconnected();
		}

		public override Task OnReconnected()
		{
			ClientConnectionEvents.OnReconnected(this, Context);
			return base.OnReconnected();
		}
	}
}
