namespace GraphApiExperiments
{
    public class ApiDevDetails
    {
        public string AadInstance = "https://login.windows.net/";
        public string Tenant = "genappsdt.onmicrosoft.com";
        public string TargetAppIdUri = "https://graph.windows.net";
        public string ClientId = "6f7b8543-89d5-4167-aff1-180ad932f3c8";
        public string Authority => AadInstance + Tenant;
    }
}