using System.ServiceModel.Description;
using Microsoft.ServiceBus;
using Service;
using System;
using System.ServiceModel;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
			System.Threading.Thread.Sleep(2000);
			ServiceBusRelayOverHttp();
        }

		static void RegularWcf()
		{
			Console.WriteLine("Using regular WCF...");
			var cf = new ChannelFactory<IHelloWorld>(new NetTcpBinding(), "net.tcp://localhost:8001/helloworld");
			var ch = cf.CreateChannel();
			Console.WriteLine(ch.Hello());
			Console.WriteLine("Done.");
			Console.ReadLine();
		}

		static void ServiceBusRelay()
		{
			Console.WriteLine("Using Service Bus Relay...");
			var tp = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", "+asnU81ZF6TkCflSe2XgFgxapqDxVgmClCUu2lq1lm4=");
			var cf = new ChannelFactory<IHelloWorld>(new NetTcpRelayBinding(), "sb://spikecodatapump.servicebus.windows.net/helloworld");
			cf.Endpoint.Behaviors.Add(new TransportClientEndpointBehavior(tp));
			var ch = cf.CreateChannel();
			Console.WriteLine(ch.Hello());
			Console.WriteLine("Done.");
			Console.ReadLine();
		}

		static void ServiceBusRelayOverHttp()
		{
			Console.WriteLine("Using Service Bus Relay over HTTP...");
			var tp = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", "+asnU81ZF6TkCflSe2XgFgxapqDxVgmClCUu2lq1lm4=");
			var cf = new ChannelFactory<IHelloWorld>(new WebHttpRelayBinding(EndToEndWebHttpSecurityMode.Transport, RelayClientAuthenticationType.None), "https://spikecodatapump.servicebus.windows.net/helloworld");
			cf.Endpoint.Behaviors.Add(new ErrorHandlingBehavior());
			cf.Endpoint.Behaviors.Add(new TransportClientEndpointBehavior(tp));
			cf.Endpoint.Behaviors.Add(new WebHttpBehavior());
			//cf.Endpoint.Behaviors.Add(new ErrorHandlingBehavior());
			cf.Opening += cf_Opening;
			cf.Opened += cf_Opened;
			cf.Faulted += cf_Faulted;
			var ch = cf.CreateChannel();
			try
			{
				var response = ch.Hello();
				Console.WriteLine(response);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("Done.");
			Console.ReadLine();
		}

		static void cf_Faulted(object sender, EventArgs e)
		{
			Console.WriteLine("Channel is faulted.");
		}

		static void cf_Opened(object sender, EventArgs e)
		{
			Console.WriteLine("Channel is opened.");
		}

		static void cf_Opening(object sender, EventArgs e)
		{
			Console.WriteLine("Channel is opening.");
		}
	}
}
