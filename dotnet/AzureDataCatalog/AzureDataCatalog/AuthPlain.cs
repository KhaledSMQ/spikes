using System;
using System.Globalization;
using System.Security;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AzureDataCatalog
{
    public class AuthPlain
    {
        private const string ClientId = "54482213-0eb2-4fe6-b479-ef70ff2e075c";
        private const string ResourceId = "https://genappsdt.onmicrosoft.com/rtservices";
        private const string AadInstance = "https://login.windows.net/{0}";
        private const string Tenant = "genappsdt.onmicrosoft.com";
        private readonly string Authority = string.Format(CultureInfo.InvariantCulture, AadInstance, Tenant);

        public async Task<string> Authenticate()
        {
            var creds = ChallengeUser();
            return await Authenticate(creds.Username, creds.Password);
        }

        public async Task<string> Authenticate(string username, SecureString password)
        {
            var authContext = new AuthenticationContext(Authority);
            var credential = new UserPasswordCredential(username, password);
            var authResult = await authContext.AcquireTokenAsync(ResourceId, ClientId, credential);
            return authResult.AccessToken;
        }

        public static (string Username, SecureString Password) ChallengeUser()
        {
            Console.WriteLine("Username?");
            var username = Console.ReadLine();
            Console.WriteLine("Password?");
            var password = new SecureString();
            var ready = false;
            while (!ready)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        ready = true;
                        break;
                    default:
                        password.AppendChar(key.KeyChar);
                        break;
                }
            }

            return (username, password);
        }
    }
}
