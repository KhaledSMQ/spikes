using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.ServiceBus;

namespace Service
{
	class Program
	{
		static void Main(string[] args)
		{
			//RegularWcf();
			ServiceBusRelayOverHttp();
		}

		static void RegularWcf()
		{
			Console.WriteLine("Using regular WCF...");
			var sh = new ServiceHost(typeof(HelloWorld));
			sh.AddServiceEndpoint(typeof(IHelloWorld), new NetTcpBinding(), "net.tcp://localhost:8001/helloworld");
			sh.Open();

			Console.WriteLine("Press enter to exit");
			Console.ReadLine();

			sh.Close();

			Console.WriteLine("Done.");
		}

		static void ServiceBusRelay()
		{
			Console.WriteLine("Using Service Bus Relay...");

			var tp = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", "+asnU81ZF6TkCflSe2XgFgxapqDxVgmClCUu2lq1lm4=");
			var sh = new ServiceHost(typeof(HelloWorld));
			var se = sh.AddServiceEndpoint(typeof(IHelloWorld), new NetTcpRelayBinding(), "sb://spikecodatapump.servicebus.windows.net/helloworld");
			se.Behaviors.Add(new TransportClientEndpointBehavior(tp));
			se.Behaviors.Add(new ServiceRegistrySettings(DiscoveryType.Public));

			sh.Open();

			Console.WriteLine("Press enter to exit");
			Console.ReadLine();

			sh.Close();

			Console.WriteLine("Done.");
		}

		static void ServiceBusRelayOverHttp()
		{
			Console.WriteLine("Using Service Bus Relay over HTTP...");

			var tp = TokenProvider.CreateSharedAccessSignatureTokenProvider("RootManageSharedAccessKey", "+asnU81ZF6TkCflSe2XgFgxapqDxVgmClCUu2lq1lm4=");
			var sh = new ServiceHost(typeof(HelloWorld));
			var se = sh.AddServiceEndpoint(typeof(IHelloWorld), new WebHttpRelayBinding(EndToEndWebHttpSecurityMode.Transport, RelayClientAuthenticationType.None), "https://spikecodatapump.servicebus.windows.net/helloworld");
			se.Behaviors.Add(new TransportClientEndpointBehavior(tp));
			se.Behaviors.Add(new ServiceRegistrySettings(DiscoveryType.Public));
			se.Behaviors.Add(new WebHttpBehavior());

			sh.Open();

			Console.WriteLine("Press enter to exit");
			Console.ReadLine();

			sh.Close();

			Console.WriteLine("Done.");
		}
	}
}
