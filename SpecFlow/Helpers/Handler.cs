using System;
using System.Net;
using System.Net.Http;

namespace SpecFlowProject.Helpers
{
    class Handler
    {
        public static HttpClientHandler CreateHandler()
        {
            return new HttpClientHandler
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
            };
        }
    }
}