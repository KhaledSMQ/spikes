using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace AzureKeyVaultService
{
    public class KeyVaultService : IKeyVaultService
    {
        private readonly string _vaultBaseUrl = ConfigurationManager.AppSettings["vaultBaseUrl"];
        private readonly string _clientId = ConfigurationManager.AppSettings["clientId"];
        private readonly string _clientSecret = ConfigurationManager.AppSettings["clientSecret"];

        public async Task<bool> CreateSecret(string secretName, string value)
        {
            try
            {
                var keyVaultClient = new KeyVaultClient(AuthenticateVault);
                await keyVaultClient.SetSecretAsync(_vaultBaseUrl, secretName, value);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error creating secret: " + e);
                return false;
            }
        }

        public async Task<string> GetSecret(string secretName)
        {
            try
            {
                var keyVaultClient = new KeyVaultClient(AuthenticateVault);
                var result =
                    await keyVaultClient.GetSecretAsync(_vaultBaseUrl, secretName);

                return result.Value;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting secret: " + e);
                throw;
            }
        }

        public async Task<bool> DeleteSecret(string secretName)
        {
            try
            {
                var keyVaultClient = new KeyVaultClient(AuthenticateVault);
                await keyVaultClient.DeleteSecretAsync(_vaultBaseUrl, secretName);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error deleting secret: " + e);
                return false;
            }
        }

        private async Task<string> AuthenticateVault(string authority, string resource, string scope)
        {
            var clientCredential = new ClientCredential(_clientId, _clientSecret);
            var authenticationContext = new AuthenticationContext(authority);
            var result = await authenticationContext.AcquireTokenAsync(resource, clientCredential);
            return result.AccessToken;
        }
    }
}
