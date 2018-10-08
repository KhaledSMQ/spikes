using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault.Models;

namespace AzureKeyVault
{
    class Program
    {
        private const string KeyVaultUrl = "https://genkeyvaultpocdev.vault.azure.net/";

        static void Main(string[] args)
        {
            var p = new Program();
            try
            {
                p.Run().Wait(10000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }

        public async Task Run()
        {
            var authHelper = new AuthToKeyVault();
            var kv = new KeyVaultHelpers(KeyVaultUrl, authHelper.AuthenticateToKeyVault);

            await ListAllKeys(kv);
            await ListAllSecrets(kv);

            const string multipleVersionTest = "multiple-secret-version-test";
            //await CreateMultipleVersionsOfSecret(kv, multipleVersionTest);
            //await ListAllSecretVersions(kv, multipleVersionTest);

            var latest = await kv.GetSecret(multipleVersionTest);
            Console.WriteLine("Latest of multiple versions is:");
            Output(latest);
        }

        private static async Task ListAllKeys(KeyVaultHelpers kv)
        {
            var keyPages = await kv.GetKeys();

            foreach (var keyItem in keyPages)
            {
                Console.WriteLine();

                Output(keyItem);

                var keyName = keyItem.Identifier.Name;

                if (keyItem.Attributes.Enabled == true)
                {
                    var keyBundle = await kv.GetKey(keyName);
                    Output(keyBundle);
                }

                await ListAllKeyVersions(kv, keyName);
            }
        }

        private static async Task ListAllSecrets(KeyVaultHelpers kv)
        {
            var secretPages = await kv.GetSecrets();

            foreach (var secretItem in secretPages)
            {
                Console.WriteLine();

                Output(secretItem);

                var secretName = secretItem.Identifier.Name;

                if (secretItem.Attributes.Enabled == true)
                {
                    var secretBundle = await kv.GetSecret(secretName);
                    Output(secretBundle);
                }

                await ListAllSecretVersions(kv, secretName);
            }
        }

        private static async Task ListAllKeyVersions(KeyVaultHelpers kv, string keyName)
        {
            var keyVersionPages = await kv.GetKeyVersions(keyName);
            foreach (var keyVersion in keyVersionPages)
            {
                Output(keyVersion);

                var vid = keyVersion.Identifier;
                var vidVersion = vid.Version;

                if (keyVersion.Attributes.Enabled == true)
                {
                    var keyBundle = await kv.GetKey(keyName, vidVersion);
                    Output(keyBundle);
                }
            }
        }

        private static async Task ListAllSecretVersions(KeyVaultHelpers kv, string secretName)
        {
            var secretVersionPages = await kv.GetSecretVersions(secretName);
            foreach (var secretVersion in secretVersionPages)
            {
                Output(secretVersion);

                var vid = secretVersion.Identifier;
                var vidVersion = vid.Version;

                if (secretVersion.Attributes.Enabled == true)
                {
                    var secretBundle = await kv.GetSecret(secretName, vidVersion);
                    Output(secretBundle);
                }
            }
        }

        private static async Task CreateMultipleVersionsOfSecret(KeyVaultHelpers kv, string secretName)
        {
            foreach (var i in Enumerable.Range(1, 3))
            {
                var value = DateTime.UtcNow.Ticks;
                await kv.CreateSecret(secretName, value.ToString());
                await Task.Delay(500);
            }
        }

        private static void Output(KeyItem item)
        {
            var id = item.Identifier;
            Console.WriteLine($"key item id: {id}");
            Console.WriteLine($"name: {id.Name}");
            Console.WriteLine($"version: {id.Version}");

            var attrs = item.Attributes;
            Console.WriteLine($"enabled: {attrs.Enabled}");
            Console.WriteLine($"created: {attrs.Created}");
            Console.WriteLine($"updated: {attrs.Updated}");
            Console.WriteLine($"expires: {attrs.Expires}");
        }

        private static void Output(KeyBundle bundle)
        {
            var id = bundle.KeyIdentifier;
            Console.WriteLine($"key bundle id: {id}");
            Console.WriteLine($"name: {id.Name}");
            Console.WriteLine($"version: {id.Version}");

            var attrs = bundle.Attributes;
            Console.WriteLine($"enabled: {attrs.Enabled}");
            Console.WriteLine($"created: {attrs.Created}");
            Console.WriteLine($"updated: {attrs.Updated}");
            Console.WriteLine($"expires: {attrs.Expires}");
        }

        private static void Output(SecretItem item)
        {
            var id = item.Identifier;
            Console.WriteLine($"secret item: {id}");
            Console.WriteLine($"name: {id.Name}");
            Console.WriteLine($"version: {id.Version}");

            var attrs = item.Attributes;
            Console.WriteLine($"enabled: {attrs.Enabled}");
            Console.WriteLine($"created: {attrs.Created}");
            Console.WriteLine($"updated: {attrs.Updated}");
            Console.WriteLine($"expires: {attrs.Expires}");
        }

        private static void Output(SecretBundle bundle)
        {
            var id = bundle.SecretIdentifier;
            Console.WriteLine($"secret bundle id: {id}");
            Console.WriteLine($"name: {id.Name}");
            Console.WriteLine($"version: {id.Version}");

            var attrs = bundle.Attributes;
            Console.WriteLine($"enabled: {attrs.Enabled}");
            Console.WriteLine($"created: {attrs.Created}");
            Console.WriteLine($"updated: {attrs.Updated}");
            Console.WriteLine($"expires: {attrs.Expires}");

            if (attrs.Enabled == true)
                Console.WriteLine($"value: {bundle.Value}");
        }
    }
}
