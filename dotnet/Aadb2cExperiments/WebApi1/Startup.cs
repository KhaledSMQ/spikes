using Microsoft.Owin.Security.OAuth;
using Owin;
using JwtFormat = Microsoft.Owin.Security.Jwt.JwtFormat;
using TokenValidationParameters = System.IdentityModel.Tokens.TokenValidationParameters;

namespace WebApi1
{
    public partial class Startup
    {
        public static string AadInstance = "https://login.microsoftonline.com/{0}/v2.0/.well-known/openid-configuration?p={1}";
        public static string Tenant = "genb2cpocdev.onmicrosoft.com";
        public static string ClientId = "82eb1746-41cd-4005-b2db-b89923c24fdf";
        public static string SignUpSignInPolicy = "b2c_1_susi";
        public static string SignInPolicy = "b2c_1_signin";
        //public static string DefaultPolicy = SignInPolicy;
        public static string DefaultPolicy = SignUpSignInPolicy;
        public static string ClientSecret = "_%7ji8&jAZA7[B;Y";

        public void Configuration(IAppBuilder app)
        {
            // ConfigureAuth defined in other part of the class
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            TokenValidationParameters tvps = new TokenValidationParameters
            {
                // Accept only those tokens where the audience of the token is equal to the client ID of this app
                ValidAudience = ClientId,
                AuthenticationType = DefaultPolicy
            };

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                // This SecurityTokenProvider fetches the Azure AD B2C metadata & signing keys from the OpenIDConnect metadata endpoint
                AccessTokenFormat = new JwtFormat(tvps, new OpenIdConnectCachingSecurityTokenProvider(string.Format(AadInstance, Tenant, DefaultPolicy)))
            });
        }
    }
}