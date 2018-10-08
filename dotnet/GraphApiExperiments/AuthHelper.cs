using System;
using System.Security;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace GraphApiExperiments
{
	public class AuthHelper
	{
        //DEV
		//private const string AadInstance = "https://login.windows.net/";
		//private const string Tenant = "genappsdt.onmicrosoft.com";
		//private const string TargetAppIdUri = "https://genappsdt.onmicrosoft.com/rtservices";
		//private const string ClientId = "6f7b8543-89d5-4167-aff1-180ad932f3c8";
		//private const string Authority = AadInstance + Tenant;
        //QA
	    //private const string AadInstance = "https://login.windows.net/";
	    //private const string Tenant = "genappsqa.onmicrosoft.com";
	    //private const string TargetAppIdUri = "https://genappsqa.onmicrosoft.com/rtservices";
	    //private const string ClientId = "9ac514c3-859c-413e-b4fc-ae8e47794496";
	    //private const string Authority = AadInstance + Tenant;

        public static async Task<AuthenticationResult> Authenticate(string username, SecureString password)
		{
			var authContext = new AuthenticationContext(EnvironmentDetails.ApiDetails.Authority);
			var uc = new UserPasswordCredential(username, password);
			try
			{
				var result = await authContext.AcquireTokenAsync(EnvironmentDetails.ApiDetails.TargetAppIdUri, EnvironmentDetails.ApiDetails.ClientId, uc);
				return result;
			}
			catch (AdalException e)
			{
				if (e.ErrorCode == "authentication_canceled")
				{
					Console.WriteLine("Sign in was canceled by the user");
					return null;
				}

				Console.WriteLine(e);
				throw;
			}
		}

		/*public static async Task<AuthenticationResult> Authenticate(string refreshToken)
		{
			var authContext = new AuthenticationContext(Authority);
			try
			{
				var result = await authContext.AcquireTokenByRefreshToken(refreshToken, ClientId);
				return result;
			}
			catch (AdalException e)
			{
				if (e.ErrorCode == "authentication_canceled")
				{
					Console.WriteLine("Sign in was canceled by the user");
					return null;
				}

				Console.WriteLine(e);
				throw;
			}
		}*/
	}
}
