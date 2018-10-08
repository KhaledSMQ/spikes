using System;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Rest.Azure;

namespace AzureKeyVault
{
    public class KeyVaultHelpers
    {
        private string VaultBaseUrl { get; }
        private KeyVaultClient.AuthenticationCallback AuthCallback { get; }

        public KeyVaultHelpers(string vaultBaseUrl, KeyVaultClient.AuthenticationCallback authCallback)
        {
            VaultBaseUrl = vaultBaseUrl;
            AuthCallback = authCallback;
        }

        public async Task<SecretBundle> CreateSecret(string secretName, string value)
        {
            var client = new KeyVaultClient(AuthCallback);
            var r = await client.SetSecretAsync(VaultBaseUrl, secretName, value);
            return r;
        }

        public async Task<SecretBundle> GetSecret(string secretName, string secretVersion = "")
        {
            var client = new KeyVaultClient(AuthCallback);
            var r = await client.GetSecretAsync(VaultBaseUrl, secretName, secretVersion);
            return r;
        }

        public async Task<DeletedSecretBundle> DeleteSecret(string secretName)
        {
            var client = new KeyVaultClient(AuthCallback);
            var r = await client.DeleteSecretAsync(VaultBaseUrl, secretName);
            return r;
        }

        public async Task<IPage<SecretItem>> GetSecrets()
        {
            var client = new KeyVaultClient(AuthCallback);
            var r = await client.GetSecretsAsync(VaultBaseUrl);
            return r;
        }

        public async Task<IPage<SecretItem>> GetSecretVersions(string secretName)
        {
            var client = new KeyVaultClient(AuthCallback);
            var r = await client.GetSecretVersionsAsync(VaultBaseUrl, secretName);
            return r;
        }

        public async Task<KeyBundle> CreateKey(string keyName, string value)
        {
            var client = new KeyVaultClient(AuthCallback);
            var r = await client.CreateKeyAsync(VaultBaseUrl, keyName, value);
            return r;
        }

        public async Task<KeyBundle> GetKey(string keyName, string keyVersion = "")
        {
            var client = new KeyVaultClient(AuthCallback);
            var r = await client.GetKeyAsync(VaultBaseUrl, keyName, keyVersion);
            return r;
        }

        public async Task<DeletedKeyBundle> DeleteKey(string keyName)
        {
            var client = new KeyVaultClient(AuthCallback);
            var r = await client.DeleteKeyAsync(VaultBaseUrl, keyName);
            return r;
        }

        public async Task<IPage<KeyItem>> GetKeys()
        {
            var client = new KeyVaultClient(AuthCallback);
            var r = await client.GetKeysAsync(VaultBaseUrl);
            return r;
        }

        public async Task<IPage<KeyItem>> GetKeyVersions(string keyName)
        {
            var client = new KeyVaultClient(AuthCallback);
            var r = await client.GetKeyVersionsAsync(VaultBaseUrl, keyName);
            return r;
        }

        public async Task<IPage<StorageAccountItem>> GetStorageAccounts()
        {
            var client = new KeyVaultClient(AuthCallback);
            var r = await client.GetStorageAccountsAsync(VaultBaseUrl);
            return r;
        }
    }
}
