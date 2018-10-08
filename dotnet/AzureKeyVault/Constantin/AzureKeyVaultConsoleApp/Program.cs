using System;
using System.Threading.Tasks;
using AzureKeyVaultService;
using Newtonsoft.Json;

namespace AzureKeyVaultConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();
            p.Run().GetAwaiter().GetResult();

            Console.WriteLine("Done. Press any key to exit.");
            Console.ReadKey(true);
        }

        public async Task Run()
        {
            var dataSourceId = 1;

            //My proposal is to save credentials as JSON object.
            //The other details (server name, database name, port, and others) are saved in the database
            var jsonString = JsonConvert.SerializeObject(new CredentialModel
            {
                UserName = "myUsername",
                Password = "myPassword"
            });

            var akv = new KeyVaultService();

            //SecretName must be unique.
            var secretName = $"DataSource-{dataSourceId}";

            //save credential
            var responseSave = await akv.CreateSecret(secretName, jsonString);
            Console.WriteLine($"Save status: {responseSave}");

            //retrieve
            var secret = await akv.GetSecret(secretName);
            Console.WriteLine($"Secret: {secret}");

            //Update credential
            var jsonStringUpdated = JsonConvert.SerializeObject(new CredentialModel
            {
                UserName = "userName-updated",
                Password = "Password-updated"
            });

            var updateResponse = await akv.CreateSecret(secretName, jsonStringUpdated);
            Console.WriteLine($"Update status: {updateResponse}");

            //retrieve
            var secret2 = await akv.GetSecret(secretName);
            Console.WriteLine($"Secret: {secret2}");

            //delete
            var deleteStatus = await akv.DeleteSecret(secretName);
            Console.WriteLine($"Delete status: {deleteStatus}");
        }
    }
}
