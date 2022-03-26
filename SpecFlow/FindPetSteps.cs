using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using SpecFlowProject.Helpers;
using TechTalk.SpecFlow.Assist;
using System.Linq;
using System.Collections.Generic;
using FluentAssertions.Json;

namespace SpecFlowProject
{
    [Binding]
    public class FindPetSteps
    {
        private string _jsonRequest;
        private HttpClient _client = new HttpClient(Handler.CreateHandler());
        private HttpResponseMessage _response;
        private string _expectedResponse;
        public String SerealizeJson(int? id, int categoryId, string categoryName, string name, string photoUrls, int tagsId, string tagsName, string status)
        {
            if (tagsName == "null")
            {
                tagsName = null;
            }
            
            var tag = new OneTag(tagsId, tagsName);

            var tags = new OneTag[] { tag };
            var category = new Category(categoryId, categoryName);
            var pet = new Pet(id, name, photoUrls, status, category, tags);

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                IgnoreNullValues = true

            };
            return JsonSerializer.Serialize(pet, serializeOptions);
        }
        [Given(@"i have added my pet to store with parameters (.*), (.*), (.*), (.*), (.*), (.*), (.*), (.*)")]
        public async Task GivenIHaveAddedMyPetToStoreWithParameters(int id, int categoryId, string categoryName, string name, string photoUrls, int tagsId, string tagsName, string status)
        {
            _jsonRequest = SerealizeJson(id, categoryId, categoryName, name, photoUrls, tagsId, tagsName, status);
            _client.PostAsync("https://petstore.swagger.io/v2/pet", _jsonRequest).Wait();
            
        }
        [When(@"i try to find them with id (.*)")]
        public async Task WhenITryToFindThemWithId(int id)
        {
            _response = await _client.GetAsync(String.Concat("https://petstore.swagger.io/v2/pet/", id.ToString()));
        }

        [Then(@"i get my pet with correct parametres (.*), (.*), (.*), (.*), (.*), (.*), (.*), (.*)")]
        public async Task ThenIGetMyPetWithCorrectParametres(int id, int categoryId, string categoryName, string name, string photoUrls, int tagsId, string tagsName, string status)
        {
            _expectedResponse = SerealizeJson(id, categoryId, categoryName, name, photoUrls, tagsId, tagsName, status);
            var actualResponse = await _response.Content.ReadAsStringAsync();
            actualResponse.Should().BeValidJson().Which.Should().BeEquivalentTo(_expectedResponse);
        }
        [Given(@"i have not added pet to store with id 55")]
        public void GivenIHaveNotAddedPetToStoreWithId()
        {
            //to make sure that pet with such id does not exist 
            _client.DeleteAsync("https://petstore.swagger.io/v2/pet/55").Wait();
        }
        [When(@"i try to find pet by id 55")]
        public async Task WhenITryToFindPetById()
        {
            _response = await _client.GetAsync("https://petstore.swagger.io/v2/pet/55");
        }
        [Then(@"i get NotFound")]
        public void ThenIGetNotFound()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
