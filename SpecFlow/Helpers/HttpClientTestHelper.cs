using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject
{
    public static class HttpClientTestHelper
    {
        public static Task<HttpResponseMessage> PostAsync(this HttpClient httpClient, string requestUri, string jsonContent)
        {
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            return httpClient.PostAsync(requestUri, content);
        }
    }
}