using System;
using System.IdentityModel.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace WebApiServer
{
	public class ApiHost
	{
		public Uri BaseAddress { get; private set; }
		private HttpSelfHostServer Server { get; set; }

		public ApiHost(Uri baseAddress)
		{
			BaseAddress = baseAddress;
		}

		public void Initialize()
		{
			var config = new HttpSelfHostConfiguration(BaseAddress);

			//SecurityConfig.ConfigureGlobal(config);

			config.Routes.MapHttpRoute(
				"ControllerActionId",
				"{controller}/{action}/{id}");
			config.Routes.MapHttpRoute(
				"ControllerId",
				"{controller}/{id}");
			config.Routes.MapHttpRoute(
				"ControllerOnly",
				"{controller}",
				new { action = "DefaultAction" });

			var ic = new IdentityConfiguration();
			var realm = ic.AudienceRestriction.AllowedAudienceUris.FirstOrDefault();
			var issuer = ((ValidatingIssuerNameRegistry) ic.IssuerNameRegistry).IssuingAuthorities.FirstOrDefault();
			var issuerName = issuer.Name;
			var symmetricKey = issuer.SymmetricKeys.FirstOrDefault();

			var authenticationHandler = new AuthenticationHandler
				                            {
					                            Realm = realm.ToString(),
					                            IssuerName = issuerName,
					                            SymmetricKey = symmetricKey
				                            };

			config.MessageHandlers.Add(authenticationHandler);

			Server = new HttpSelfHostServer(config);
		}

		public void Open()
		{
			Initialize();
			Server.OpenAsync().Wait();
		}

		public void Close()
		{
			Server.CloseAsync().Wait();
		}
	}
}
