using System.Web.Http;
using SpikesCo.Gdp.WebApi;

namespace Host
{
	public class SimpleHost : ApiOwinSelfHost
	{
		public SimpleHost(string name, string address)
			: base(name, address)
		{ }

		public override void Configure(HttpConfiguration configuration)
		{
			base.Configure(configuration);
			configuration.MapHttpAttributeRoutes();
		}
	}
}
