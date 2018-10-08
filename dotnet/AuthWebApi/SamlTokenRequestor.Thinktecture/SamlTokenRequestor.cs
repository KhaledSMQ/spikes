using System;
using System.IO;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Security.Claims;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Threading;
using System.Xml;
using Thinktecture.IdentityModel.Constants;
using Thinktecture.IdentityModel.WSTrust;

namespace SamlTokenRequestor.Thinktecture
{
    public class SamlTokenRequestor
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
				new UserNameWSTrustBinding(SecurityMode.TransportWithMessageCredential),
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
				TokenType = TokenTypes.Saml2TokenProfile11,
				AppliesTo = new EndpointReference(Realm)
			};

			var genericToken = factory.CreateChannel().Issue(rst) as GenericXmlSecurityToken;
			var tokenXml = genericToken.TokenXml.OuterXml;

			var tokenHandlers = SecurityTokenHandlerCollection.CreateDefaultSecurityTokenHandlerCollection();
			var config = tokenHandlers.Configuration;
			config.AudienceRestriction.AllowedAudienceUris.Add(new Uri(Realm));
			config.CertificateValidator = X509CertificateValidator.None;
			var registry = new ConfigurationBasedIssuerNameRegistry();
			registry.AddTrustedIssuer("CB317A2E635B47310D50A67C7B40081F7B4BD280", "http://platformservices.spikesco.com/devsts");
			config.IssuerNameRegistry = registry;
			var token = tokenHandlers.ReadToken(new XmlTextReader(new StringReader(tokenXml)));
			var identity = tokenHandlers.ValidateToken(token);
			Thread.CurrentPrincipal = new ClaimsPrincipal(identity);

			TokenHelpers.TokenHelpers.Print(tokenXml);
			TokenHelpers.TokenHelpers.PrintCurrentPrincipal();

			return token;
		}
	}
}
