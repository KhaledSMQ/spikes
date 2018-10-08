using System.Threading.Tasks;

namespace AzureKeyVaultService
{
    public interface IKeyVaultService
    {
        Task<bool> CreateSecret(string secretName, string value);
        Task<string> GetSecret(string secretName);
        Task<bool> DeleteSecret(string secretName);
    }
}
