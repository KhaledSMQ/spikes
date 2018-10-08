namespace GraphClient
{
    public class GraphApiDevDetails
    {
        public const string AadInstance = "https://login.windows.net/";
        public const string Tenant = "genb2cpocdev.onmicrosoft.com";
        public const string TenantId = "53385b0e-5beb-4a8e-a989-4f8b087bf529";
        public const string GraphApiBaseUri = "https://graph.windows.net";
        public const string ClientId = "f93f8a81-d862-499b-8768-b8d3a8483548";
        public const string ClientSecret = "Ihs0FKJViYzM9NfuePVVjOz6VN0rkcGQks35W/EaQrI=";
        public static string Authority => AadInstance + Tenant;
    }
}
