using SpecFlowProject.Helpers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowProject
{
    [Binding]
    public class AddAPetSteps
    {

        private HttpClient _client = new HttpClient(Handler.CreateHandler());
        private HttpResponseMessage _responseForPost;
        private const string _expectedResponse = @"
	            {
                    ""id"": 5,
                    ""category"": {
                        ""id"": 0,
                        ""name"": ""string""
                    },
                    ""name"": ""Bobik"",
                    ""photoUrls"": [
                        ""string""
                    ],
                    ""tags"": [
                        {
                            ""id"": 0,
                            ""name"": ""string""
                        }
                    ],
                    ""status"": ""available""
                }";
        [Then(@"operation goes successfully")]
        public async Task ThenOperationGoesSuccessfullyAsync()
        {
            var actualResponse = await _responseForPost.Content.ReadAsStringAsync();
            actualResponse.ShouldBeEquivalentJson(_expectedResponse);
        }

        private Task<HttpResponseMessage> GivenIHaveAddedMyPetToTheStore()
        {
            throw new NotImplementedException();
        }

        [Then(@"my pet has been stored")]
        public async Task ThenMyPetHasBeenStoredAsync()
        {
           // var actualResponse = await _client.GetAsync("https://petstore.swagger.io/v2/pet/5").Content.ReadAsStringAsync(); ;
            //actualResponse.ShouldBeEquivalentJson(_expectedResponse);
        }
    }
}
