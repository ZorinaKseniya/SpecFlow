using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using SpecFlowProject.Helpers;

namespace SpecFlowProject
{
    [Binding]
    public class FindingPetByIDSteps
    {
        
            
        
        private const string JsonRequest = @"
                {
                  ""id"": 33,
                  ""category"": {
                    ""id"": 0,
                    ""name"": ""string""
                  },
                  ""name"": ""Bimbo"",
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
        private HttpClient _client = new HttpClient(Handler.CreateHandler());
        private HttpResponseMessage _response;
        private const string _expectedResponse = @"
	            {
                    ""id"": 33,
                    ""category"": {
                        ""id"": 0,
                        ""name"": ""string""
                    },
                    ""name"": ""Bimbo"",
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
        [Given(@"i have added my pet to store with id 33")]
        [When(@"i try to add my pet to store with id 33")]
        public async Task GivenIHaveAddedMyPetToTheStore()
        {

           await _client.PostAsync("https://petstore.swagger.io/v2/pet", JsonRequest);
        }
        [When(@"i try to find him by id 33")]
        public async Task WhenITryToFindMyPetById()
        {
           _response = await _client.GetAsync("https://petstore.swagger.io/v2/pet/33");
        }

        [Then(@"i get my pet with id 33")]
        public async Task ThenIGetInformationAboutMyPet()
        {
            var actualResponse = await _response.Content.ReadAsStringAsync();
            actualResponse.ShouldBeEquivalentJson(_expectedResponse);
        }


        [Given(@"i have not added pet to store with id 55")]
        public void GivenIHaveNotAddedPetToStoreWithId()
        {
            //to make sure that pet with syuch id does not exist 
            _client.DeleteAsync("https://petstore.swagger.io/v2/pet/petId").Wait();
        }

        [When(@"i try to find pet by id 55")]
        public async Task WhenITryToFindPetById()
        {
            _response = await _client.GetAsync("https://petstore.swagger.io/v2/pet/55");
        }

        [Then(@"i get NotFound")]
        public void ThenIGetCode()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
