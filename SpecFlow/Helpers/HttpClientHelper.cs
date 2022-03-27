using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject.Helpers
{
    public static class HttpClientHelper
    {
        public static HttpClient Create()
        {
            return 
                new HttpClient(
                    new HttpClientHandler
                    {
                        UseDefaultCredentials = true,
                        ServerCertificateCustomValidationCallback = (_, _, _, _) => true,
                        UseProxy = true,
                        Proxy = new WebProxy
                        {
                            Address = new Uri("http://proxy.avp.ru"),
                            BypassProxyOnLocal = true,
                            UseDefaultCredentials = true
                        }
                    });
        }

        public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string requestUri, string jsonContent)
        {
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            return httpClient.PostAsync(requestUri, content);
        }
    }
}