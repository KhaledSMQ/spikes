using Newtonsoft.Json.Linq;

namespace SalesForceExperiments
{
	public class AuthenticationResponse
	{
		public string Token { get; private set; }
		public string TokenType { get; private set; }
		public string InstanceUrl { get; private set; }

		public AuthenticationResponse(string response)
		{
			var json = JObject.Parse(response);
			Token = json["access_token"].ToString();
			TokenType = json["token_type"].ToString();
			InstanceUrl = json["instance_url"].ToString();
		}
	}
}
