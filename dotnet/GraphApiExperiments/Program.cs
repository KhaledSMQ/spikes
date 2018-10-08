using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Azure.ActiveDirectory.GraphClient;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using MoreLinq;

namespace GraphApiExperiments
{
	internal class Program
	{
	    private const string NoMail = "<no mail>";
	    private const string NoOtherMails = "<no other mails>";

        private static string Username { get; set; }
		private static SecureString Password { get; set; }

		[STAThread]
		private static void Main(string[] args)
		{
			RunAuthentication().Wait();
			//RunAuthenticationWithRefreshToken();
			ListUsers().Wait();
			Console.WriteLine("Press Enter to exit.");
			Console.ReadLine();
		}

		private static async Task<AuthenticationResult> RunAuthentication()
		{
			if (string.IsNullOrEmpty(Username))
				ChallengeUser();
			var result = await AuthHelper.Authenticate(Username, Password);
			return result;
		}

		private static async Task<AuthenticationResult> RunGraphApiAuthentication()
		{
			if (string.IsNullOrEmpty(Username))
				ChallengeUser();
			var result = await GraphApiAuthHelper.Authenticate(Username, Password);
			return result;
		}

		/*private static void RunAuthenticationWithRefreshToken()
		{
			var result = RunAuthentication();
			var refreshToken = result.RefreshToken;
			var resultFromRefresh = AuthHelper.Authenticate(refreshToken);
		}*/

		private static async Task ListUsers()
		{
			var result = await RunAuthentication(); // we're authenticating to obtain the tenant id dynamically
			var tenantId = result.TenantId;

			var client = new ActiveDirectoryClient(new Uri("https://graph.windows.net/" + tenantId), AcquireGraphApiTokenAsync);

			Console.WriteLine("Retrieving users...");

            //var tenant = client.TenantDetails.ExecuteAsync().Result.CurrentPage.ToList();

		    //var me = client.Me.ExecuteAsync().Result;
		    //var dobjs = client.DirectoryObjects.ExecuteAsync().Result.CurrentPage.ToList();

		    var users = new List<IUser>();
		    var userBatch = await client.Users.ExecuteAsync();
            users.AddRange(userBatch.CurrentPage);
		    while (userBatch.MorePagesAvailable)
		    {
                userBatch = await userBatch.GetNextPageAsync();
                users.AddRange(userBatch.CurrentPage);
            }

		    var allUsers = ListUsers(users).ToList();

		    var validSuffixes = new[] { "spikesco.com", "spikesco.it", "gmail.com", "hotmail.com", "scalefocus.com", "pentalog.fr" };
		    var noMailUsers = allUsers.Where(u => u.OtherMails == NoOtherMails).ToList();
            var internalUsers = allUsers.Except(noMailUsers).Where(u => validSuffixes.Any(s => u.OtherMails.EndsWith(s))).ToList();
            var externalUsers = allUsers.Except(noMailUsers).Except(internalUsers).ToList();

            Console.WriteLine($"There are {internalUsers.Count} internal users:");
            OutputUsers(internalUsers);

            Console.WriteLine("------------------------------------------------");
		    Console.WriteLine($"There are {externalUsers.Count} external users:");
            OutputUsers(externalUsers);

		    Console.WriteLine("------------------------------------------------");
		    Console.WriteLine($"There are {noMailUsers.Count} users without emails:");
		    OutputUsers(noMailUsers);

		    //await RemoveUsers(externalUsers.Select(u => u.IUser), client);
		}

        private static IEnumerable<(string DisplayName, string Mail, string OtherMails, IUser IUser)> ListUsers(IEnumerable<IUser> users)
	    {
	        var rows = from user in users
	            select (DisplayName: user.DisplayName,
                    Mail: user.Mail?.Length > 0 ? user.Mail : NoMail,
	                OtherMails: user.OtherMails?.Count > 0 ? string.Join(", ", user.OtherMails) : NoOtherMails,
                    IUser: user);

	        return rows;
	    }

        private static void OutputUsers(IEnumerable<(string DisplayName, string Mail, string OtherMails, IUser IUser)> users)
	    {
	        var table = users.ToList();

	        if (!table.Any())
	            return;

	        var maxWidths = (DisplayName: table.Max(r => r.DisplayName.Length), Mail: table.Max(r => r.Mail.Length), OtherMails: table.Max(r => r.OtherMails.Length));
	        var format = "{0," + -maxWidths.DisplayName + "} {1," + -maxWidths.Mail + "} {2," + -maxWidths.OtherMails + "}";

            foreach (var row in table)
            {
                Console.WriteLine(format, row.DisplayName, row.Mail, row.OtherMails);
            }
        }


	    private static async Task RemoveUsers(IEnumerable<IUser> users, ActiveDirectoryClient client)
	    {
	        const int batchSize = 5;
            const bool useDeferredSave = true;

	        foreach (var userBatch in users.Batch(batchSize))
	        {
	            foreach (var user in userBatch)
	            {
                    Console.WriteLine("Removing user " + user.OtherMails[0]);
	                await user.DeleteAsync(useDeferredSave);
	            }

	            if (useDeferredSave)
	            {
	                var response = await client.Context.SaveChangesAsync(
	                    System.Data.Services.Client.SaveChangesOptions.BatchWithIndependentOperations);
	                response.EnsureSuccessStatusCode();
                    Console.WriteLine("Deleted batch.");
	            }
	        }
        }

	    private static void ChallengeUser()
		{
			Console.WriteLine("Username?");
			Username = Console.ReadLine();
			Console.WriteLine("Password?");
			Password = new SecureString();
			var ready = false;
			while (!ready)
			{
				var key = Console.ReadKey(true);
				switch (key.Key)
				{
					case ConsoleKey.Enter:
						ready = true;
						break;
					default:
						Password.AppendChar(key.KeyChar);
						break;
				}
			}
		}

		private static async Task<AuthenticationResult> AuthenticateAsync()
		{
			var authResult = await RunAuthentication();
			return authResult;
		}

		private static async Task<string> AcquireGraphApiTokenAsync()
		{
			var authResult = await RunGraphApiAuthentication();
			return authResult.AccessToken;
		}
	}
}
