using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AadManualAuth;

namespace AadManualAuth
{
    public partial class MainForm : Form
    {
        private IDictionary<string, LoginResponse> LoginResponses { get; set; }

        public MainForm()
        {
            InitializeComponent();
            Prepare();
        }

        private void Prepare()
        {
            LoginResponses = new Dictionary<string, LoginResponse>();
            var username = ConfigurationManager.AppSettings["username"];
            if (!string.IsNullOrEmpty(username))
            {
                UsernameTextBox.Text = username;
                ActiveControl = PasswordTextBox;
            }
             
            AddTabs();
            Log("Ready.");
        }

        private void AddTabs()
        {
            var pageNames = new[] { "Dev", "QA", "Beta", "Prod" };
            foreach (var pageName in pageNames)
            {
                var tabPage = new TabPage(pageName)
                {
                    Tag = pageName.ToLowerInvariant(),
                };
                EnvironmentTabControl.TabPages.Add(tabPage);
            }

            EnvironmentTabControl.Selected += EnvironmentTabControl_Selected;
        }

        private void EnvironmentTabControl_Selected(object sender, TabControlEventArgs e)
        {
            var tabPage = e.TabPage;
            UpdateTab(tabPage);
        }

        private void AuthenticateButton_Click(object sender, EventArgs e)
        {
            AuthenticateButton.Enabled = false;
            var ct = new CancellationToken();
            RunAuthentication().
                ContinueWith(
                    t => UpdateTab(EnvironmentTabControl.SelectedTab),
                    ct,
                    TaskContinuationOptions.None,
                    TaskScheduler.FromCurrentSynchronizationContext()).
                ContinueWith(t => { AuthenticateButton.Enabled = true; },
                    ct,
                    TaskContinuationOptions.None,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task RunAuthentication()
        {
            foreach (var control in EnvironmentsGroupBox.Controls)
            {
                var checkBox = control as CheckBox;
                if (checkBox != null)
                {
                    var isChecked = checkBox.Checked;
                    var tag = checkBox.Tag as string;
                    if (isChecked)
                    {
                        var url = ConfigurationManager.AppSettings[tag + ":url"];
                        if (string.IsNullOrEmpty(url))
                        {
                            Log("URL for " + tag + " is not configured.");
                            continue;
                        }

                        Log("Authenticating in " + tag + "...");
                        var response = await RunAuthentication(url);

                        var message = "Authenticated in " + tag + ".";
                        if (!IsValidResponse(response))
                            message = "Authentication in " + tag + " failed.";
       
                        Log(message);
                        LoginResponses[tag] = response;
                    }
                }
            }
        }

        private async Task<LoginResponse> RunAuthentication(string environmentUrl)
        {
            var username = UsernameTextBox.Text;
            var password = PasswordTextBox.Text;
            var result = await AuthHelper.Authenticate(username, password, environmentUrl);
            return result;
        }

        private void UpdateTab(TabPage tabPage)
        {
            var tag = tabPage.Tag as string;
            LoginResponse response;
            if (LoginResponses.TryGetValue(tag, out response))
            {
                AccessTokenTextBox.Text = response.AccessToken;
                RefreshTokenTextBox.Text = response.RefreshToken;
                ResponseTextBox.Text = response.Entitlements?.ToString();
            }
            else
            {
                AccessTokenTextBox.Text = string.Empty;
                RefreshTokenTextBox.Text = string.Empty;
                ResponseTextBox.Text = string.Empty;
            }

            if (IsValidResponse(response))
            {
                CopyAccessTokenAsHeaderToClipboard();
            }

            if (string.IsNullOrEmpty(AccessTokenTextBox.Text))
                AccessTokenTextBox.Text = "<no access token>";
            if (string.IsNullOrEmpty(RefreshTokenTextBox.Text))
                RefreshTokenTextBox.Text = "<no refresh token>";
            if (string.IsNullOrEmpty(ResponseTextBox.Text))
                ResponseTextBox.Text = "<no response>";
        }

        private void UpdateAuthenticateButtonState()
        {
            var isAnyChecked = false;
            foreach (var control in EnvironmentsGroupBox.Controls)
            {
                var checkBox = control as CheckBox;
                if (checkBox != null)
                {
                    if (checkBox.Checked)
                    {
                        isAnyChecked = true;
                        break;
                    }
                }
            }

            AuthenticateButton.Enabled = isAnyChecked &&
                UsernameTextBox.Text.Length > 0 &&
                PasswordTextBox.Text.Length > 0;
        }

        private bool IsValidResponse(LoginResponse response)
        {
            var isValid = response != null &&
                          !string.IsNullOrEmpty(response.AccessToken) &&
                          !string.IsNullOrEmpty(response.RefreshToken) &&
                          !string.IsNullOrEmpty(response.Entitlements?.ToString());
            return isValid;
        }

        private void CopyAccessTokenToClipboard()
        {
            Clipboard.SetText(AccessTokenTextBox.Text);
            var env = EnvironmentTabControl.SelectedTab.Tag;
            Log("Copied access token from " + env + " to the clipboard.");
        }

        private void CopyAccessTokenAsHeaderToClipboard()
        {
            var header = "Authorization: Bearer " + AccessTokenTextBox.Text;
            Clipboard.SetText(header);
            var env = EnvironmentTabControl.SelectedTab.Tag;
            Log("Copied access token authorization header from " + env + " to the clipboard.");
        }

        private void CopyRefreshTokenToClipboard()
        {
            Clipboard.SetText(RefreshTokenTextBox.Text);
            var env = EnvironmentTabControl.SelectedTab.Tag;
            Log("Copied refresh token from " + env + " to the clipboard.");
        }

        private void CopyRefreshTokenAsHeaderToClipboard()
        {
            var header = "Authorization: Bearer " + RefreshTokenTextBox.Text;
            Clipboard.SetText(header);
            var env = EnvironmentTabControl.SelectedTab.Tag;
            Log("Copied refresh token authorization header from " + env + " to the clipboard.");
        }

        private void CopyResponseToClipboard()
        {
            var text = ResponseTextBox.Text;
            Clipboard.SetText(text);
            var env = EnvironmentTabControl.SelectedTab.Tag;
            Log("Copied response from " + env + " to the clipboard.");
        }

        private void Log(string message)
        {
            if (ActivityTextBox.InvokeRequired)
            {
                ActivityTextBox.Invoke(new Action<string>(Log), message);
                return;
            }

            ActivityTextBox.Text = message + Environment.NewLine + ActivityTextBox.Text;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthenticateButtonState();
        }

        private void CopyAccessTokenButton_Click(object sender, EventArgs e)
        {
            CopyAccessTokenToClipboard();
        }

        private void CopyAccessTokenAsHeaderButton_Click(object sender, EventArgs e)
        {
            CopyAccessTokenAsHeaderToClipboard();
        }

        private void CopyRefreshTokenButton_Click(object sender, EventArgs e)
        {
            CopyRefreshTokenToClipboard();
        }

        private void CopyRefreshTokenAsHeaderButton_Click(object sender, EventArgs e)
        {
            CopyRefreshTokenAsHeaderToClipboard();
        }

        private void CopyResponseButton_Click(object sender, EventArgs e)
        {
            CopyResponseToClipboard();
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateAuthenticateButtonState();
        }

        private void UsernameTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateAuthenticateButtonState();
        }
    }
}
