using System;
using System.IO;
using System.Net;
using System.Text;

namespace AzureDataCatalog
{
    public class HttpHelpers
    {
        public string Execute(string catalogName, string urlCommand, string token)
        {
            var fullUri = $"https://api.azuredatacatalog.com/catalogs/{catalogName}/{urlCommand}";

            var request = (HttpWebRequest) WebRequest.Create(fullUri);
            request.Method = "GET";

            string responseContent;

            try
            {
                using (var httpResponse = SetRequestAndGetResponse(request, token))
                {
                    using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.Status);
                if (ex.Response != null)
                {
                    if (ex.Response.ContentLength != 0)
                    {
                        using (var stream = ex.Response.GetResponseStream())
                        {
                            using (var reader = new StreamReader(stream))
                            {
                                Console.WriteLine(reader.ReadToEnd());
                            }
                        }
                    }
                }
                return null;
            }

            return responseContent;
        }

        public HttpWebResponse SetRequestAndGetResponse(HttpWebRequest request, string token, string payload = null)
        {
            while (true)
            {
                request.Headers.Add("Authorization", $"Bearer {token}");
                request.AllowAutoRedirect = false;

                if (!string.IsNullOrEmpty(payload))
                {
                    var byteArray = Encoding.UTF8.GetBytes(payload);
                    request.ContentLength = byteArray.Length;
                    request.ContentType = "application/json";
                    request.GetRequestStream().Write(byteArray, 0, byteArray.Length);
                }
                else
                {
                    request.ContentLength = 0;
                }

                var response = request.GetResponse() as HttpWebResponse;

                // Requests to **Azure Data Catalog (ADC)** may return an HTTP 302 response to indicate  
                // redirection to a different endpoint. In response to a 302, the caller must re-issue  
                // the request to the URL specified by the Location response header.    
                if (response.StatusCode == HttpStatusCode.Redirect)
                {
                    var redirectedUrl = response.Headers["Location"];
                    var nextRequest = WebRequest.Create(redirectedUrl) as HttpWebRequest;
                    nextRequest.Method = request.Method;
                    request = nextRequest;
                }
                else
                {
                    return response;
                }
            }
        }
    }
}
