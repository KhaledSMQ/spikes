using System;
using System.Security.Claims;
using System.Threading;

namespace TokenHelpers
{
	public static class TokenHelpers
	{
		public static void Print(string tokenXml)
		{
			Console.WriteLine("Token XML:\n{0}", tokenXml);
		}

		public static void PrintCurrentPrincipal()
		{
			var principal = Thread.CurrentPrincipal;
			var identity = principal.Identity;
			Console.WriteLine("Current principal and identity:");
			Console.WriteLine("Name: {0}, IsAuthenticated: {1}, AuthenticationType: {2}", identity.Name, identity.IsAuthenticated, identity.AuthenticationType);
			var claimsIdentity = identity as ClaimsIdentity;
			Console.WriteLine("Claims:");
			foreach (var claim in claimsIdentity.Claims)
			{
				Console.WriteLine("Type: {0}, Value: {1}", claim.Type, claim.Value);
			}
		}
	}
}
