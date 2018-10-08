using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace RestPostRequests
{
    public class PostHelper
    {
        public static async Task<List<string>> Post(string username, string password, string url, string xml, int times)
        {
            using (var client = new HttpClient())
            {
                //var encoded = /*Convert.ToBase64String(Encoding.Default.GetBytes(*/username + ":" + password/*))*/;
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encoded);

                var list = new List<string>();
                foreach (var i in Enumerable.Range(1, times))
                {
                    var contentObject = new StringContent(xml, System.Text.Encoding.Default, "application/xml");
                    var response = await client.PostAsync(url, contentObject);
                    var result = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(result))
                    {
                        list.Add(result);
                    }
                    else
                    {
                        list.Add("no response");
                    }
                }
                return list;
            }
        }

        private static string SecureStringToString(SecureString value)
        {
            var valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
