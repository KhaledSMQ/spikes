using System.Web.Http;
using SpikesCo.Platform.Security.AzureActiveDirectory.Owin;
using Owin;

namespace WebApi5
{
    public static class AppBuilderExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "config")]
        public static void UseAuthentication(this IAppBuilder app, HttpConfiguration config)
        {
            var configuration = new SecurityConfigurationFactory().ReadFromConfigurationFile();
            var authOptions = new AuthenticationOptionsFactory().Create(configuration);
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(authOptions);
        }
    }
}
