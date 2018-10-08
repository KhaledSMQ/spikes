using System.IO;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Threading;
using System.Xml;

namespace WebApiClient
{
	public class IdSrvAuthenticator
	{
		private const string StsWsTrustEndpoint = "https://dvwauthsrvc.prod.int/idsrv/issue/wstrust/mixed/username";
		private const string Realm = "http://localhost:9500/";

		public string Authenticate(string username, string password)
		{
			// Need this because the server has a self-signed certificate that doesn't pass validation
			ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;

			var factory = new WSTrustChannelFactory(
				new UserNameWsTrustBinding(),
				StsWsTrustEndpoint)
			{
				TrustVersion = TrustVersion.WSTrust13
			};

			factory.Credentials.UserName.UserName = username;
			factory.Credentials.UserName.Password = password;

			var rst = new RequestSecurityToken
			{
				RequestType = RequestTypes.Issue,
				KeyType = KeyTypes.Bearer,
				TokenType = "urn:ietf:params:oauth:token-type:jwt",
				AppliesTo = new EndpointReference(Realm)
			};

			var securityToken = factory.CreateChannel().Issue(rst);
			var genericToken = securityToken as GenericXmlSecurityToken;
			var tokenXml = genericToken.TokenXml.OuterXml;
			var handler = new JwtSecurityTokenHandler();
			var token = handler.ReadToken(new XmlTextReader(new StringReader(tokenXml)));

			var jwtToken = (JwtSecurityToken) token;
			SetPrincipal(jwtToken);
			var tokenStr = handler.WriteToken(jwtToken);
			tokenStr += jwtToken.EncodedSignature;
			return tokenStr;
		}

		private static void SetPrincipal(JwtSecurityToken token)
		{
			Thread.CurrentPrincipal = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "JWT"));
		}

		private class UserNameWsTrustBinding : Binding
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
}
