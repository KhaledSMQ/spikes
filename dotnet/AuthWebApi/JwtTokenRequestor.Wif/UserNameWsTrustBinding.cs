using System.ServiceModel;
using System.ServiceModel.Channels;

namespace JwtTokenRequestor.Wif
{
	public class UserNameWsTrustBinding : Binding
	{
		private string _scheme;

		public override string Scheme
		{
			get { return _scheme; }
		}

		public override BindingElementCollection CreateBindingElements()
		{
			var securityBindingElement = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
			securityBindingElement.MessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;

			var encodingBindingElement = new TextMessageEncodingBindingElement { ReaderQuotas = { MaxArrayLength = 0x200000, MaxStringContentLength = 0x200000 } };

			var transportBindingElement = new HttpsTransportBindingElement { MaxReceivedMessageSize = 0x200000L };
			_scheme = transportBindingElement.Scheme;

			var elements = new BindingElementCollection
				{
					securityBindingElement,
					encodingBindingElement,
					transportBindingElement
				};

			return elements;
		}
	}
}
