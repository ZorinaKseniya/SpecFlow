using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using SpecFlowProject.Helpers;

namespace SpecFlowProject.Helpers
{
    class Handler
    {
        public static HttpClientHandler CreateHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
            handler.UseProxy = true;
            handler.Proxy = new WebProxy
            {
                Address = new Uri("http://proxy.avp.ru"),
                BypassProxyOnLocal = true,
                UseDefaultCredentials = true
            };
            return handler;
        }
    }
}

