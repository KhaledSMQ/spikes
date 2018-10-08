using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SalesForceExperiments
{
	public class AuthenticationStrategy
	{
		private Credentials Credentials { get; set; }

		public AuthenticationStrategy()
		{
			Credentials = new Credentials();
		}

		public AuthenticationResponse Authenticate()
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri(Credentials.AuthenticationUri);
				var content = new FormUrlEncodedContent(
					new[]
					{
						new KeyValuePair<string, string>("grant_type", "password"),
						new KeyValuePair<string, string>("client_id", Credentials.ClientId),
						new KeyValuePair<string, string>("client_secret", Credentials.ClientSecret),
						new KeyValuePair<string, string>("username", Credentials.Username),
						new KeyValuePair<string, string>("password", Credentials.Password + Credentials.SecurityToken),
					}
					);
				var result = client.PostAsync("", content).Result;
				var resultContent = result.Content.ReadAsStringAsync().Result;
				var response = new AuthenticationResponse(resultContent);
				return response;
			}
		}
	}
}
