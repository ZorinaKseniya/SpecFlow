using FluentAssertions;
using FluentAssertions.Json;
using SpecFlowProject.Entities;
using SpecFlowProject.Helpers;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowProject.Steps
{
    [Binding]
    public class FindPetSteps: BaseSteps
    {
        private string _jsonRequest;
        private string _expectedResponse;

        [Given(@"user has added his pet to store with parameters (.*), (.*), (.*), (.*), (.*), (.*), (.*), (.*)")]
        public Task GivenUserHasAddedHisPetToStoreWithParameters(int id, int categoryId, string categoryName, string name, string photoUrls, int tagsId, string tagsName, string status)
        {
            _jsonRequest = SerealizeJson(id, categoryId, categoryName, name, photoUrls, tagsId, tagsName, status);
            return _client.PostAsync($"{_settings.HostName}/v2/pet", _jsonRequest);
        }

        [When(@"user tries to find him with id (.*)")]
        public async Task WhenUserTriesToFindThemWithId(int id)
        {
            _response = await _client.GetAsync(String.Concat($"{_settings.HostName}/v2/pet/", id.ToString()));
        }

        [Then(@"user gets his pet with correct parametres (.*), (.*), (.*), (.*), (.*), (.*), (.*), (.*)")]
        public async Task ThenUserGetsHisPetWithCorrectParametres(int id, int categoryId, string categoryName, string name, string photoUrls, int tagsId, string tagsName, string status)
        {
            _expectedResponse = SerealizeJson(id, categoryId, categoryName, name, photoUrls, tagsId, tagsName, status);
            var actualResponse = await _response.Content.ReadAsStringAsync();
            actualResponse.Should().BeValidJson().Which.Should().BeEquivalentTo(_expectedResponse);
        }

        [Given(@"user has not added pet to store with id 55")]
        public void GivenuserHasNotAddedPetToStoreWithId()
        {
            //to make sure that pet with such id does not exist 
            _client.DeleteAsync($"{_settings.HostName}/v2/pet/55").Wait();
        }

        [When(@"user tries to find pet by id 55")]
        public async Task WhenUserTriesToFindPetById()
        {
            _response = await _client.GetAsync($"{_settings.HostName}/v2/pet/55");
        }

        [Then(@"user gets NotFound as pet does not exists")]
        public void ThenUserGetsNotFound()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        private static string SerealizeJson(int id, int categoryId, string categoryName, string name, string photoUrls, int tagsId, string tagsName, string status)
        {
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
    }
}
