using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AadAuth
{
	public class AuthHelper
	{
		public static async Task<LoginResponse> Authenticate(string username, string password, string url)
		{
		    using (var client = new HttpClient())
		    {
		        var login = new Login { UserName = username, Password = password };
		        var serialized = JsonConvert.SerializeObject(login);
		        var content = new StringContent(serialized, System.Text.Encoding.Default, "application/json");
		        var response = await client.PostAsync(url, content);
		        var result = await response.Content.ReadAsStringAsync();
		        if (!string.IsNullOrEmpty(result))
		        {
		            var deserialized = JsonConvert.DeserializeObject<LoginResponse>(result);
                    return deserialized;
		        }
		        return new LoginResponse();
		    }
		}
    }
}
