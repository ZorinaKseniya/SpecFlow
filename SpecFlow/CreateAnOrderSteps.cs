using FluentAssertions;
using FluentAssertions.Json;
using SpecFlowProject.Helpers;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowProject
{
    [Binding]
    public class CreateAnOrderSteps
    {
        private string _jsonRequest;
        private string _jsonPostPet = @"
        {
          ""id"": #id#,
          ""category"": {
            ""id"": 0,
            ""name"": ""string""
          },
          ""name"": ""doggie"",
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
        private string _expectedResponse;
        public string SerealizeJson (int id, int petId, int quantity, DateTime shipDate, string status, bool complete)
        {
            var order = new Order(id, petId, quantity, shipDate, status, complete);
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                IgnoreNullValues = true

            };
            return JsonSerializer.Serialize(order, serializeOptions);
        }
        [Given(@"a pet with (.*) has been added to store")]
        public async Task GivenAPetWithIdHasBeenAddedToStore(int id)
        {
            _client.PostAsync("https://petstore.swagger.io/v2/pet", _jsonPostPet.Replace("#id#",id.ToString())).Wait();
        }
        [When(@"i make an order for that pet with parameters (.*), (.*), (.*), (.*), (.*), (.*)")]
        public async Task WhenIMakeAnOrderForThatPetWithParameters(int id, int petId, int quantity, DateTime shipDate, string status, bool complete)
        {
            _jsonRequest = SerealizeJson(id, petId, quantity, shipDate, status, complete);
            _response = await _client.PostAsync("https://petstore.swagger.io/v2/store/order", _jsonRequest);
        }
        [Then(@"my order has been placed with parameters (.*), (.*), (.*), (.*), (.*), (.*)")]
        public async Task ThenMyOrderHasBeenPlacedWithParameters(int id, int petId, int quantity, DateTime shipDate, string status, bool complete)
        {
            _expectedResponse = SerealizeJson(id, petId, quantity, shipDate, status, complete);
            var actualResponse = await _response.Content.ReadAsStringAsync();
            actualResponse.Should().BeValidJson().Which.Should().BeEquivalentTo(_expectedResponse);
        }
    }
}
