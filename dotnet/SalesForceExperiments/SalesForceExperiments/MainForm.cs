using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using Newtonsoft.Json;
using SalesForceExperiments.Model;

namespace SalesForceExperiments
{
	public partial class MainForm : Form
	{
		private AuthenticationResponse Authentication { get; set; }

		public MainForm()
		{
			InitializeComponent();
			ApiCallBox.KeyPress += ApiCallBox_KeyPress;
		}

		private void ApiCallBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Enter)
				ExecuteAndOutput(ApiCallBox.Text);
		}

		private void AuthButton_Click(object sender, EventArgs e)
		{
			var authenticator = new AuthenticationStrategy();
			var auth = authenticator.Authenticate();
			Authentication = auth;
			Output(auth);
			label1.Text = "User is authenticated";
		}

		private void Output(AuthenticationResponse response)
		{
			Output(string.Format("Instance URL: {0} Token type: {1} Token: {2}", response.InstanceUrl, response.TokenType, response.Token));
		}

		private void Output(string text)
		{
            Output<object>(text);
		}

        private void Output<TObject>(string text)
        {
            var output = text;
            try
            {
                var parsed = JsonConvert.DeserializeObject<TObject>(text);
                output = JsonConvert.SerializeObject(parsed, Formatting.Indented);
            }
            catch (JsonReaderException)
            {
            }

            OutputBox.Text = output;// + Environment.NewLine + OutputBox.Text;
        }

        private string RunRequest(string path)
		{
			try
			{
				using (var client = new HttpClient())
				{
					client.BaseAddress = new Uri(Authentication.InstanceUrl);

					var header = new AuthenticationHeaderValue(Authentication.TokenType, Authentication.Token);
					client.DefaultRequestHeaders.Authorization = header;

					var result = client.GetStringAsync(path).Result;
					return result;
				}
			}
			catch (Exception e)
			{
				return e.ToString();
			}
		}

		private string ExecuteAndOutput(string path)
		{
			ApiCallBox.Text = path;
			var resp = RunRequest(path);
			Output(resp);
            return resp;
		}

		private void ListVersionsButton_Click(object sender, EventArgs e)
		{
			ExecuteAndOutput("/services/data/");
		}

		private void ListResourcesButton_Click(object sender, EventArgs e)
		{
			ExecuteAndOutput("/services/data/v31.0/");
		}

		private void ListObjectsButton_Click(object sender, EventArgs e)
		{
			ExecuteAndOutput("/services/data/v31.0/sobjects");
		}

		private void ExecuteButton_Click(object sender, EventArgs e)
		{
			ExecuteAndOutput(ApiCallBox.Text);
		}

		private void AccountsButton_Click(object sender, EventArgs e)
		{
			ExecuteAndOutput(Queries.QueryPrefix + Queries.Accounts);
		}

        private void ContactsButton_Click(object sender, EventArgs e)
        {
            ExecuteAndOutput(Queries.QueryPrefix + Queries.Contacts);
        }

        private void ProductsButton_Click(object sender, EventArgs e)
        {
            ExecuteAndOutput(Queries.QueryPrefix + Queries.Products);
        }

        private void ContactsPerAccountButton_Click(object sender, EventArgs e)
        {
            var rawAccounts = RunRequest(Queries.QueryPrefix + Queries.Accounts);
            ParseResults<AccountRecord>(rawAccounts);
            var rawContacts = RunRequest(Queries.QueryPrefix + Queries.Contacts);
            ParseResults<ContactRecord>(rawContacts);
        }

        private void DescribeAccountsButton_Click(object sender, EventArgs e)
        {
            var accounts = ExecuteAndOutput(DescribeQueries.QueryPrefix + DescribeQueries.Accounts);
            ParseDescribeResults<DescribeResults>(accounts);
        }

        private void DescribeContactsButton_Click(object sender, EventArgs e)
        {
            var contacts = ExecuteAndOutput(DescribeQueries.QueryPrefix + DescribeQueries.Contacts);
            ParseDescribeResults<DescribeResults>(contacts);
        }

        private void ParseResults<TRecord>(string results)
        {
            try
            {
                var parsed = JsonConvert.DeserializeObject<QueryResults<TRecord>>(results);
            }
            catch (Exception)
            {
            }
        }

        private void ParseDescribeResults<TResults>(string results)
        {
            try
            {
                Output<TResults>(results);
            }
            catch (Exception)
            {
            }
        }

        private void DescribeProductsButton_Click(object sender, EventArgs e)
        {
            var results = ExecuteAndOutput(DescribeQueries.QueryPrefix + DescribeQueries.Products);
            ParseDescribeResults<DescribeResults>(results);
        }

        private void AssetsButton_Click(object sender, EventArgs e)
        {
            ExecuteAndOutput(Queries.QueryPrefix + Queries.Assets);
        }

        private void DescribeAssetsButton_Click(object sender, EventArgs e)
        {
            var results = ExecuteAndOutput(DescribeQueries.QueryPrefix + DescribeQueries.Assets);
            ParseDescribeResults<DescribeResults>(results);
        }

        private void DescribeAssignedUsersButton_Click(object sender, EventArgs e)
        {
            var results = ExecuteAndOutput(DescribeQueries.QueryPrefix + DescribeQueries.AssignedUsers);
            ParseDescribeResults<DescribeResults>(results);
        }
    }
}
