using System.Collections.Generic;
using System.Web.Http;

namespace Host
{
	[RoutePrefix("api/simple")]
	public class SimpleController : ApiController
	{
		[Route("")]
		public IEnumerable<string> Get()
		{
			return new[] { "1", "2", "3" };
		}

		[Route("{id:int}")]
		public string Get(string id)
		{
			return "Result for " + id;
		}
	}
}
