using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestPostRequests
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://pdwteamcity01.spikesco.local:8010/httpAuth/app/rest/buildQueue";
            var xml = @"<build><buildType id=""Platform_Hotfixes_Alerting""/></ build>";
            var times = 2;
            var results = PostHelper.Post(Username, Password, url, xml, times).Result;
        }

        #region creds

        private const string Username = "pmouat@spikesco.com";
        private const string Password = "dummytemp01!";

        #endregion
    }
}
