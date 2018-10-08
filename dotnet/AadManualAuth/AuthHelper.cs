using System.Net.Http;
using System.Threading.Tasks;
using AadManualAuth;
using Newtonsoft.Json;

namespace AadManualAuth
{
	public class AuthHelper
	{
		public static async Task<LoginResponse> Authenticate(string username, string password, string url)
		{
            // http://stackoverflow.com/a/42164212
            // POST to https://login.microsoftonline.com/yourdomain.onmicrosoft.com/oauth2/token
            // with resource={resource}&client_id={clientid}&grant_type=password&username={username}&password={password}&scope=openid&client_secret={clientsecret}

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
