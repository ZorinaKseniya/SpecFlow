using Microsoft.Extensions.Configuration;
using SpecFlowProject.Helpers;
using System.IO;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace SpecFlowProject.Steps
{
    [Binding]
    public class BaseSteps
    {
        static BaseSteps()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            _settings = new Settings();
            ConfigurationBinder.Bind(config, _settings);
        }

        [StepArgumentTransformation]
        public static string StringNullTransform(string str)
        {
            return str == "<null>" ? null : str;
        }

        protected HttpResponseMessage _response;
        protected readonly HttpClient _client = new HttpClient(Handler.CreateHandler());
        protected readonly static Settings _settings;
    }
}