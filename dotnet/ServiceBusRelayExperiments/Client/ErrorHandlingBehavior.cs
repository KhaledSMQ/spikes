using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Client
{
	public class ErrorHandlingBehavior : IEndpointBehavior
	{
		public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
		{ }

		public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
			var errorHandler = new ErrorHandler();
			clientRuntime.ClientMessageInspectors.Add(new MessageInspector());
			//foreach (ChannelDispatcher channelDispatcher in endpoint.Binding. endpointDispatcher.ChannelDispatcher.Host.ChannelDispatchers)
			//	channelDispatcher.ErrorHandlers.Add(errorHandler);
		}

		public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
		{
			var errorHandler = new ErrorHandler();
			foreach (ChannelDispatcher channelDispatcher in endpointDispatcher.ChannelDispatcher.Host.ChannelDispatchers)
				channelDispatcher.ErrorHandlers.Add(errorHandler);
		}

		public void Validate(ServiceEndpoint endpoint)
		{ }
	}
}
