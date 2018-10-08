using System.ServiceModel.Dispatcher;

namespace Client
{
	public class MessageInspector : IClientMessageInspector
	{
		public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
		{
		}

		public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
		{
			// returns a correlation state, which should be a new guid
			return null;
		}
	}
}
