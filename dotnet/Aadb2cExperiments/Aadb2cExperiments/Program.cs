using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace Aadb2cExperiments
{
    class Program
    {
        private const string Tenant = "genb2cpocdev.onmicrosoft.com";
        private const string ClientId = "ad984181-9d61-4e1a-9937-9c117b0cdfec";        
        private const string PolicySignUpSignIn = "b2c_1_susi";
        private const string PolicySignUp = "b2c_1_signup";
        private const string PolicySignIn = "b2c_1_signin";
        private const string PolicyEditProfile = "b2c_1_edit_profile";
        private const string PolicyResetPassword = "b2c_1_reset";

        private const string SelectedPolicy = PolicySignUpSignIn;

        private static string[] ApiScopes = { "https://genb2cpocdev.onmicrosoft.com/testwebapi/user_impersonation" };
        private const string AuthorityTemplate = "https://login.microsoftonline.com/tfp/{0}/{1}/oauth2/v2.0/authorize";
        private static readonly string AuthoritySignUpSignIn = string.Format(AuthorityTemplate, Tenant, PolicySignUpSignIn);
        private static readonly string AuthoritySignIn = string.Format(AuthorityTemplate, Tenant, PolicySignIn);
        private static readonly string AuthorityEditProfile = string.Format(AuthorityTemplate, Tenant, PolicyEditProfile);
        private static readonly string AuthorityResetPassword = string.Format(AuthorityTemplate, Tenant, PolicyResetPassword);

        private const string Username = "testuser1@genb2cpocdev.onmicrosoft.com";
        private const string Password = "1dentity!";

        private static string Authority => string.Format(AuthorityTemplate, Tenant, SelectedPolicy);

        //private const string TestWebApiUrl = "http://localhost:5907/api/values";
        private const string TestWebApiUrl = "https://aadb2cpocwebapi1.azurewebsites.net/api/values";

        private PublicClientApplication ClientApp { get; set; }

        private static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
            Console.ReadLine();
        }

        private async void Run()
        {
            TokenCacheHelper.DeleteUserCache();
            ClientApp = new PublicClientApplication(ClientId, Authority, TokenCacheHelper.GetUserCache());
            var token = await SignIn();
            var content = await GetHttpContentWithToken(TestWebApiUrl, token);
            Console.WriteLine($"Output from web request: {content}");
        }

        private async Task<string> SignIn()
        {
            AuthenticationResult authResult;
            try
            {
                var user = GetUserByPolicy(ClientApp.Users, SelectedPolicy);
                authResult = await ClientApp.AcquireTokenAsync(ApiScopes, user, UIBehavior.SelectAccount, string.Empty, null, Authority);
                DisplayBasicTokenInfo(authResult);
                return authResult.AccessToken;
            }
            catch (MsalServiceException ex)
            {
                try
                {
                    if (ex.Message.Contains("AADB2C90118"))
                    {
                        authResult = await ClientApp.AcquireTokenAsync(
                            ApiScopes,
                            GetUserByPolicy(ClientApp.Users, SelectedPolicy),
                            UIBehavior.SelectAccount, string.Empty, null, AuthorityResetPassword);
                        DisplayBasicTokenInfo(authResult);
                        return authResult.AccessToken;
                    }

                    Console.WriteLine($"Error Acquiring Token:{Environment.NewLine}{ex}");
                }
                catch (Exception)
                {
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Users: {string.Join(", ", ClientApp.Users.Select(u => u.Identifier))}{Environment.NewLine}Error Acquiring Token: {Environment.NewLine}{ex}");
            }

            return null;
        }

        public async Task<string> GetHttpContentWithToken(string url, string token)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await new HttpClient().SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        private static void DisplayBasicTokenInfo(AuthenticationResult authResult)
        {
            if (authResult == null)
                return;

            Console.WriteLine($"Name: {authResult.User.Name}");
            Console.WriteLine($"Token Expires: {authResult.ExpiresOn.ToLocalTime()}");
            Console.WriteLine($"Access Token: {authResult.AccessToken}");
        }

        private static IUser GetUserByPolicy(IEnumerable<IUser> users, string policy)
        {
            foreach (var user in users)
            {
                var userIdentifier = Base64UrlDecode(user.Identifier.Split('.')[0]);
                if (userIdentifier.EndsWith(policy.ToLower()))
                    return user;
            }

            return null;
        }

        private static string Base64UrlDecode(string s)
        {
            s = s.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            var byteArray = Convert.FromBase64String(s);
            var decoded = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
            return decoded;
        }
    }
}
