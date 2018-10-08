using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;

namespace SasAuth
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Program();

            p.RunOnAccount().Wait();
            p.RunOnContainer().Wait();

            Console.WriteLine("Done. Press Enter to exit.");
            Console.ReadLine();
        }

        private async Task RunOnAccount()
        {
            var sasToken = GetSasAccountToken();
            Console.WriteLine($"SAS Account Token: {sasToken}");

            await ListBlobs("container1", sasToken);
            await ListBlobs("container2", sasToken);
        }

        private async Task RunOnContainer()
        {
            var sasToken = GetSasContainerToken("container1");
            Console.WriteLine($"SAS Container Token: {sasToken}");

            try
            {
                await ListBlobs("container1", sasToken);

                // the token is not for this container, so we get a 403
                await ListBlobs("container2", sasToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private string GetSasAccountToken()
        {
            const string connectionString = "DefaultEndpointsProtocol=https;AccountName=genreportsarchivedev;AccountKey=5SJ0cWZ4jGkPJzuliDMDbFwvB+pa+Hi3X93Yrs2eaFfztQ1mmdrICZEbzzpTLE6UpHi8eHKN0IVbLdQvJ/8oDw==;EndpointSuffix=core.windows.net";

            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var policy = new SharedAccessAccountPolicy
            {
                Permissions = SharedAccessAccountPermissions.Read | SharedAccessAccountPermissions.Write | SharedAccessAccountPermissions.List | SharedAccessAccountPermissions.Create | SharedAccessAccountPermissions.Delete,
                Services = SharedAccessAccountServices.Blob,
                ResourceTypes = SharedAccessAccountResourceTypes.Container | SharedAccessAccountResourceTypes.Object,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
                Protocols = SharedAccessProtocol.HttpsOrHttp
            };

            var container = storageAccount.CreateCloudBlobClient()
                .GetContainerReference("container1");
            var perms = container.GetPermissions();

            var sasToken = storageAccount.GetSharedAccessSignature(policy);
            return sasToken;
        }

        private static string GetSasContainerToken(string containerName)
        {
            const string connectionString = "DefaultEndpointsProtocol=https;AccountName=genreportsarchivedev;AccountKey=5SJ0cWZ4jGkPJzuliDMDbFwvB+pa+Hi3X93Yrs2eaFfztQ1mmdrICZEbzzpTLE6UpHi8eHKN0IVbLdQvJ/8oDw==;EndpointSuffix=core.windows.net";

            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var container = storageAccount.CreateCloudBlobClient().GetContainerReference(containerName);
            var policy = new SharedAccessBlobPolicy
            {
                //Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.List | SharedAccessBlobPermissions.Create | SharedAccessBlobPermissions.Delete,
                Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.List,
                SharedAccessExpiryTime = DateTime.UtcNow.AddHours(24),
            };

            var sasToken = container.GetSharedAccessSignature(policy);
            return sasToken;
        }

        private async Task ListBlobs(string containerName, string sasToken)
        {
            var containerUri = new Uri("https://genreportsarchivedev.blob.core.windows.net/" + containerName);
            Console.WriteLine($"Container URI: {containerUri}");

            var storageCreds = new StorageCredentials(sasToken);
            var container = new CloudBlobContainer(containerUri, storageCreds);

            await ListBlobs(container);
        }

        private static async Task ListBlobs(CloudBlobContainer container)
        {
            BlobContinuationToken token = null;
            do
            {
                var resultSegment = await container.ListBlobsSegmentedAsync(token);
                token = resultSegment.ContinuationToken;
                foreach (var blob in resultSegment.Results)
                {
                    Console.WriteLine("{0} (type: {1}", blob.Uri, blob.GetType());
                }
            }
            while (token != null);
        }
    }
}
