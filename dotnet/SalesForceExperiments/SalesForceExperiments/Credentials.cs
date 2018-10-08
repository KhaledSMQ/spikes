using System.Configuration;

namespace SalesForceExperiments
{
	public class Credentials
	{
		public string AuthenticationUri { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string SecurityToken { get; set; }

		public Credentials()
		{
			AuthenticationUri = ConfigurationManager.AppSettings["SalesForce:AuthUri"];
			Username = ConfigurationManager.AppSettings["SalesForce:Username"];
			Password = ConfigurationManager.AppSettings["SalesForce:Password"];
			ClientId = ConfigurationManager.AppSettings["SalesForce:ClientId"];
			ClientSecret = ConfigurationManager.AppSettings["SalesForce:ClientSecret"];
			SecurityToken = ConfigurationManager.AppSettings["SalesForce:SecurityToken"];
		}
	}
}
