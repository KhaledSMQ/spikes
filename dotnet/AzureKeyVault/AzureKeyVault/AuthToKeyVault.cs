using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AzureKeyVault
{
    public class AuthToKeyVault
    {
        // client id is for app 'Paulo Key Vault POC Web App Client' in the 'SpikeCo, INC' directory
        private const string ClientId = "fceeb920-b06b-4998-947f-3ece09ff2a4d";
        private const string ClientSecret = "9jzMglJUPyCkkD6bPoZZLqiAgD4o/1bTqJUOw4I3xrg=";

        public async Task<string> AuthenticateToKeyVault(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            var credential = new ClientCredential(ClientId, ClientSecret);
            var result = await authContext.AcquireTokenAsync(resource, credential);
            return result.AccessToken;
        }
    }
}
