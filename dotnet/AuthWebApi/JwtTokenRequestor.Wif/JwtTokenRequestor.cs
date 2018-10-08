using System;
using System.IO;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using System.ServiceModel.Security;
using System.Threading;
using System.Xml;

namespace JwtTokenRequestor.Wif
{
	public class JwtTokenRequestor
	{
		private const string StsWsTrustEndpoint = "https://dvwauthsrvc.prod.int/idsrv/issue/wstrust/mixed/username";
		private const string UserName = "pmouat";
		private const string Password = "1dentity";
		private const string Realm = "http://localhost:9500/";

		public SecurityToken GetToken()
		{
			// Need this because the server has a self-signed certificate that doesn't pass validation
			ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, errors) => true;

			var factory = new WSTrustChannelFactory(
				new UserNameWsTrustBinding(),
				StsWsTrustEndpoint)
			{
				TrustVersion = TrustVersion.WSTrust13
			};

			factory.Credentials.UserName.UserName = UserName;
			factory.Credentials.UserName.Password = Password;

			var rst = new RequestSecurityToken
			{
				RequestType = RequestTypes.Issue,
				KeyType = KeyTypes.Bearer,
				TokenType = "urn:ietf:params:oauth:token-type:jwt",
				AppliesTo = new EndpointReference(Realm)
			};

			var genericToken = factory.CreateChannel().Issue(rst) as GenericXmlSecurityToken;
			var tokenXml = genericToken.TokenXml.OuterXml;
			var handler = new JwtSecurityTokenHandler();
			var token = handler.ReadToken(new XmlTextReader(new StringReader(tokenXml)));

			TokenHelpers.TokenHelpers.Print(tokenXml);
			Print(token);
			SetPrincipal((JwtSecurityToken)token);
			TokenHelpers.TokenHelpers.PrintCurrentPrincipal();

			return token;
		}

		private static void Print(SecurityToken token)
		{
			Console.WriteLine("Token JSON:\n{0}", token);
		}

		private static void SetPrincipal(JwtSecurityToken token)
		{
			Thread.CurrentPrincipal = new ClaimsPrincipal(new ClaimsIdentity(token.Claims, "JWT"));
		}
	}
}
