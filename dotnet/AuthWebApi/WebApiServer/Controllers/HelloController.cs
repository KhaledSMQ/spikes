using System;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace WebApiServer.Controllers
{
	public class HelloController : ApiController
	{
		[HttpGet]
		[ActionName("DefaultAction")]
		[Authorize]
		public string Get()
		{
			var principal = (ClaimsPrincipal) Thread.CurrentPrincipal;
			var username = principal.FindFirst(ClaimTypes.Name);
			var email = principal.FindFirst(ClaimTypes.Email);

			var message = string.Format("{0}: Hello {1}, {2}", DateTime.Now.ToLongTimeString(), username, email);
			Console.WriteLine("Sent response to client: {0}", message);
			return message;
		}
	}
}
