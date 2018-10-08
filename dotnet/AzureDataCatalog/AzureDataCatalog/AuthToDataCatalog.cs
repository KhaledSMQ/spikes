using System;
using System.Globalization;
using System.Security;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AzureDataCatalog
{
    public class AuthToDataCatalog
    {
        // client id is for app 'Paulo Data Catalog POC Client' in the 'SpikeCo, INC' directory
        private const string ClientId = "579d459a-0d05-472b-b97b-93d7f68b437d";
        private const string ResourceId = "https://api.azuredatacatalog.com";
        private const string AadInstance = "https://login.windows.net/{0}";
        private const string Tenant = "common/oauth2/authorize";
        private readonly string Authority = string.Format(CultureInfo.InvariantCulture, AadInstance, Tenant);

        public async Task<string> Authenticate()
        {
            var (username, password) = ChallengeUser();
            return await Authenticate(username, password);
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var authContext = new AuthenticationContext(Authority);
            var credential = new UserPasswordCredential(username, password);
            var authResult = await authContext.AcquireTokenAsync(ResourceId, ClientId, credential);
            return authResult.AccessToken;
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
