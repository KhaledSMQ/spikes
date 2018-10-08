namespace GraphApiExperiments
{
    public class ApiQaDetails
    {
        public string AadInstance = "https://login.windows.net/";
        public string Tenant = "genappsqa.onmicrosoft.com";
        public string TargetAppIdUri = "https://graph.windows.net";
        public string ClientId = "9ac514c3-859c-413e-b4fc-ae8e47794496";
        public string Authority => AadInstance + Tenant;
    }
}