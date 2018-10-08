using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace GraphClient
{
    internal class Program
    {
        private static ActiveDirectoryClient _client;

        private static ActiveDirectoryClient Client => _client ?? (_client = GetClient());

        static void Main(string[] args)
        {
            var p = new Program();
            p.Run();
            Console.ReadLine();
        }

        private async void Run()
        {
            //await CreateUser();
            //await CreateUser2();
            await ListApplications();
            await ListUsers();
        }

        private static async Task CreateUser()
        {
            var rnd = new Random().Next(1, 1000);
            var guid = Guid.NewGuid();

            var displayName = "Test User " + rnd;
            var givenName = "Test";
            var surname = "User" + rnd;
            var password = "1dentity!";
            var mailNickname = guid.ToString();
            var upn = $"{guid}@{GraphApiDevDetails.Tenant}";
            var creationType = "LocalAccount";
            var passwordPolicies = "DisablePasswordExpiration";

            var user = new User
            {
                AccountEnabled = true,
                DisplayName = displayName,
                Surname = surname,
                GivenName = givenName,
                PasswordProfile = new PasswordProfile
                {
                    Password = password,
                    EnforceChangePasswordPolicy = false,
                    ForceChangePasswordNextLogin = false
                },
                MailNickname = mailNickname,
                UserPrincipalName = upn,
                SignInNames = new List<SignInName> { new SignInName { Type = "emailAddress", Value = $"paulo.mouat+test{rnd}@gmail.com" } },
                CreationType = creationType,
                PasswordPolicies = passwordPolicies
            };

            user.SetExtendedProperty("extension_4b4f8b64183a42ab863afc5f56014d60_SalesforceId", "0123456789abcdef-" + rnd);
            user.SetExtendedProperty("extension_4b4f8b64183a42ab863afc5f56014d60_EntitlementsId", "ent0123456789abcdef-" + rnd);

            await Client.Users.AddUserAsync(user);
        }

        private static async Task CreateUser2()
        {
            var rnd = new Random().Next(1, 1000);
            var guid = Guid.NewGuid();

            var displayName = "Sarah Idriss";
            var givenName = "Sarah";
            var surname = "Idriss";
            var password = "!23ASDfgh";
            var mailNickname = guid.ToString();
            var upn = $"{guid}@{GraphApiDevDetails.Tenant}";
            var creationType = "LocalAccount";
            var passwordPolicies = "DisablePasswordExpiration";

            var user = new User
            {
                AccountEnabled = true,
                DisplayName = displayName,
                Surname = surname,
                GivenName = givenName,
                PasswordProfile = new PasswordProfile
                {
                    Password = password,
                    EnforceChangePasswordPolicy = false,
                    ForceChangePasswordNextLogin = false
                },
                MailNickname = mailNickname,
                UserPrincipalName = upn,
                SignInNames = new List<SignInName> { new SignInName { Type = "emailAddress", Value = $"sarah.idriss+b2ctest@gmail.com" } },
                CreationType = creationType,
                PasswordPolicies = passwordPolicies
            };

            user.SetExtendedProperty("extension_4b4f8b64183a42ab863afc5f56014d60_SalesforceId", "0123456789abcdef-" + rnd);
            user.SetExtendedProperty("extension_4b4f8b64183a42ab863afc5f56014d60_EntitlementsId", "ent0123456789abcdef-" + rnd);

            await Client.Users.AddUserAsync(user);
        }

        private static async Task ListApplications()
        {
            Console.WriteLine("Retrieving applications...");

            var apps = new List<IApplication>();
            var appBatch = await Client.Applications.ExecuteAsync();
            apps.AddRange(appBatch.CurrentPage);
            while (appBatch.MorePagesAvailable)
            {
                appBatch = await appBatch.GetNextPageAsync();
                apps.AddRange(appBatch.CurrentPage);
            }

            foreach (var app in apps)
            {
                Console.WriteLine($"Application: {app.DisplayName} {app.AppId} {string.Join(", ", app.IdentifierUris)}");
                var props = new List<IExtensionProperty>();
                var propBatch = app.ExtensionProperties;
                props.AddRange(propBatch.CurrentPage);
                while (propBatch.MorePagesAvailable)
                {
                    propBatch = await app.ExtensionProperties.GetNextPageAsync();
                    props.AddRange(propBatch.CurrentPage);
                }

                OutputExtensionProperties(props);
            }

            var eps = await Client.GetAvailableExtensionPropertiesAsync(true);
            OutputExtensionProperties(eps);
        }

        private static async Task ListUsers()
        {
            Console.WriteLine("Retrieving users...");

            var users = new List<IUser>();
            var userBatch = await Client.Users.ExecuteAsync();
            users.AddRange(userBatch.CurrentPage);
            while (userBatch.MorePagesAvailable)
            {
                userBatch = await userBatch.GetNextPageAsync();
                users.AddRange(userBatch.CurrentPage);
            }

            var allUsers = users.ToList();

            Console.WriteLine($"There are {allUsers.Count} users:");
            OutputUsers(allUsers);
        }

        private static void OutputUsers(IEnumerable<IUser> users)
        {
            foreach (var user in users)
            {
                Console.WriteLine($"{user.DisplayName} {user.GivenName} {user.Surname} {user.UserPrincipalName} {user.Mail} " +
                    $"{string.Join(", ", user.SignInNames.Select(c => c.Type + ": " + c.Value))} " +
                    $"{string.Join(", ", ((User)user).GetExtendedProperties().Select(p => p.Key + ": " + p.Value))}");
            }
        }

        private static void OutputExtensionProperties(IEnumerable<IExtensionProperty> properties)
        {
            foreach (var property in properties)
            {
                Console.WriteLine(property.Name);
            }
        }

        private static ActiveDirectoryClient GetClient()
        {
            var client = new ActiveDirectoryClient(new Uri("https://graph.windows.net/" + GraphApiDevDetails.TenantId), AcquireGraphApiTokenAsync);
            return client;
        }

        private static async Task<string> AcquireGraphApiTokenAsync()
        {
            var authResult = await RunGraphApiAuthentication();
            return authResult.AccessToken;
        }

        private static async Task<AuthenticationResult> RunGraphApiAuthentication()
        {
            var authContext = new AuthenticationContext(GraphApiDevDetails.Authority);
            var cred = new ClientCredential(GraphApiDevDetails.ClientId, GraphApiDevDetails.ClientSecret);
            try
            {
                var result = await authContext.AcquireTokenAsync(GraphApiDevDetails.GraphApiBaseUri, cred);
                return result;
            }
            catch (AdalException e)
            {
                if (e.ErrorCode == "authentication_canceled")
                {
                    Console.WriteLine("Sign in was canceled by the user");
                    return null;
                }

                Console.WriteLine(e);
                throw;
            }
        }
    }
}
