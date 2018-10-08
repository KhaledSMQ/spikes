using System;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using WebApp1.Models;

namespace WebApp1
{
    public partial class Startup
    {
        private const string Tenant = "genb2cpocdev.onmicrosoft.com";
        private const string ClientId = "008dea3f-b4e9-4b70-8208-760e417717c4";
        //private static string RedirectUri = "http://localhost:6493/";
        private static string RedirectUri = "https://aadb2cpocwebapp1.azurewebsites.net/";

        private const string AadInstance = "https://login.microsoftonline.com/tfp/{0}/{1}/v2.0/.well-known/openid-configuration";
        public const string PolicySignUpSignIn = "b2c_1_susi";
        public const string PolicySignUp = "b2c_1_signup";
        public const string PolicySignIn = "b2c_1_signin";
        public const string PolicyEditProfile = "b2c_1_edit_profile";
        public const string PolicyResetPassword = "b2c_1_reset";

        private const string DefaultPolicy = PolicySignUpSignIn;
        private static string ClientSecret = @"85\9K,$4V1|Y603i";

        private static string Authority = String.Format(AadInstance, Tenant, DefaultPolicy);
        private static string[] Scopes = { "https://genb2cpocdev.onmicrosoft.com/testwebapi/user_impersonation" };

        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(
                new OpenIdConnectAuthenticationOptions
                {
                    // Generate the metadata address using the tenant and policy information
                    MetadataAddress = String.Format(AadInstance, Tenant, DefaultPolicy),

                    // These are standard OpenID Connect parameters, with values pulled from web.config
                    ClientId = ClientId,
                    RedirectUri = RedirectUri,
                    PostLogoutRedirectUri = RedirectUri,

                    // Specify the callbacks for each type of notifications
                    Notifications = new OpenIdConnectAuthenticationNotifications
                    {
                        RedirectToIdentityProvider = OnRedirectToIdentityProvider,
                        AuthorizationCodeReceived = OnAuthorizationCodeReceived,
                        AuthenticationFailed = OnAuthenticationFailed,
                    },

                    // Specify the claims to validate
                    TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name"
                    },

                    // Specify the scope by appending all of the scopes requested into one string (separated by a blank space)
                    //Scope = $"openid profile offline_access user_impersonation"
                    Scope = $"openid profile offline_access https://genb2cpocdev.onmicrosoft.com/testwebapi/user_impersonation"
                }
            );
        }

        /*
         *  On each call to Azure AD B2C, check if a policy (e.g. the profile edit or password reset policy) has been specified in the OWIN context.
         *  If so, use that policy when making the call. Also, don't request a code (since it won't be needed).
         */
        private Task OnRedirectToIdentityProvider(RedirectToIdentityProviderNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            var policy = notification.OwinContext.Get<string>("Policy");

            if (!string.IsNullOrEmpty(policy) && !policy.Equals(DefaultPolicy))
            {
                notification.ProtocolMessage.Scope = OpenIdConnectScopes.OpenId;
                notification.ProtocolMessage.ResponseType = OpenIdConnectResponseTypes.IdToken;
                notification.ProtocolMessage.IssuerAddress = notification.ProtocolMessage.IssuerAddress.ToLower().Replace(DefaultPolicy.ToLower(), policy.ToLower());
            }

            return Task.FromResult(0);
        }

        /*
         * Catch any failures received by the authentication middleware and handle appropriately
         */
        private Task OnAuthenticationFailed(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> notification)
        {
            notification.HandleResponse();

            // Handle the error code that Azure AD B2C throws when trying to reset a password from the login page 
            // because password reset is not supported by a "sign-up or sign-in policy"
            if (notification.ProtocolMessage.ErrorDescription != null && notification.ProtocolMessage.ErrorDescription.Contains("AADB2C90118"))
            {
                // If the user clicked the reset password link, redirect to the reset password route
                notification.Response.Redirect("/Account/ResetPassword");
            }
            else if (notification.Exception.Message == "access_denied")
            {
                notification.Response.Redirect("/");
            }
            else
            {
                notification.Response.Redirect("/Home/Error?message=" + notification.Exception.Message);
            }

            return Task.FromResult(0);
        }


        /*
         * Callback function when an authorization code is received 
         */
        private async Task OnAuthorizationCodeReceived(AuthorizationCodeReceivedNotification notification)
        {
            // Extract the code from the response notification
            var code = notification.Code;

            string signedInUserID = notification.AuthenticationTicket.Identity.FindFirst(ClaimTypes.NameIdentifier).Value;
            TokenCache userTokenCache = new MSALSessionCache(signedInUserID, notification.OwinContext.Environment["System.Web.HttpContextBase"] as HttpContextBase).GetMsalCacheInstance();
            ConfidentialClientApplication cca = new ConfidentialClientApplication(ClientId, Authority, RedirectUri, new ClientCredential(ClientSecret), userTokenCache, null);
            try
            {
                AuthenticationResult result = await cca.AcquireTokenByAuthorizationCodeAsync(code, Scopes);
            }
            catch (Exception ex)
            {
                //TODO: Handle
                throw;
            }
        }
    }
}
