using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace SignalRExperiments.Hubs
{
	public class ChatHub : Hub
	{
		public void Hello()
		{
			Clients.All.hello();
		}

		public void Send(string name, string message)
		{
			Clients.Others.addNewMessageToPage(name, message);
		}
	}
}