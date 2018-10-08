using System;
using System.IdentityModel.Tokens;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.ServiceModel.Security.Tokens;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiServer
{
	public class AuthenticationHandler : DelegatingHandler
	{
		private const string Scheme = "JWT";

		public string Realm { get; set; }
		public string IssuerName { get; set; }
		public string SymmetricKey { get; set; }
		//private const string Realm = "http://localhost:9500/";
		//private const string IssuerName = "http://platformservices.spikesco.com/devsts";
		//private const string SharedKey = "wDX2EWxt11yqTcDC3QKwwtJ88onq6q02QxWZBq0YIfQ=";

		protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
		{
			try
			{
				var headers = request.Headers;
				ProcessHeaders(headers);

				var response = await base.SendAsync(request, cancellationToken);
				if (response.StatusCode == HttpStatusCode.Unauthorized)
					response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(Scheme));

				return response;
			}
			catch (Exception e)
			{
				Console.WriteLine("Error occurred while authenticating request: " + e);
				var response = request.CreateResponse(HttpStatusCode.Unauthorized);
				response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(Scheme));
				return response;
			}
		}

		private void ProcessHeaders(HttpRequestHeaders headers)
		{
			if (headers.Authorization != null && Scheme.Equals(headers.Authorization.Scheme))
			{
				var tokenRaw = headers.Authorization.Parameter;
				var handler = new JwtSecurityTokenHandler();
				var validation = new TokenValidationParameters
					{
						AllowedAudience = Realm,
						SigningToken = new BinarySecretSecurityToken(Convert.FromBase64String(SymmetricKey)),
						ValidIssuer = IssuerName,
						ValidateIssuer = true
					};
				var principal = handler.ValidateToken(tokenRaw, validation);
				Thread.CurrentPrincipal = principal;
			}
		}
	}
}
