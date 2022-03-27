using FluentAssertions;
using FluentAssertions.Json;
using SpecFlowProject.Entities;
using SpecFlowProject.Helpers;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowProject.Steps
{
    [Binding]
    public class CreateAnOrderSteps: BaseSteps
    {
        private HttpResponseMessage _responseForInvalidInput;
        private string _expectedResponse;
        private string _jsonRequest;
        private const string _jsonPostPet = @"
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
        
        [Given(@"a pet with (.*) has been added to store")]
        public Task GivenAPetWithIdHasBeenAddedToStore(int id)
        {
            return _client.PostAsync($"{_settings.HostName}/v2/pet", _jsonPostPet.Replace("#id#",id.ToString()));
        }

        [When(@"user makes an order for that pet with parameters (.*), (.*), (.*), (.*), (.*), (.*)")]
        public async Task WhenUserMakesAnOrderForThatPetWithParameters(int id, int petId, int quantity, DateTime shipDate, string status, bool complete)
        {
            _jsonRequest = SerealizeJson(id, petId, quantity, shipDate, status, complete);
            _response = await _client.PostAsync($"{_settings.HostName}/v2/store/order", _jsonRequest);
        }

        [Then(@"an order has been placed with parameters (.*), (.*), (.*), (.*), (.*), (.*)")]
        public async Task ThenMyOrderHasBeenPlacedWithParameters(int id, int petId, int quantity, DateTime shipDate, string status, bool complete)
        {
            _expectedResponse = SerealizeJson(id, petId, quantity, shipDate, status, complete);
            var actualResponse = await _response.Content.ReadAsStringAsync();
            actualResponse.Should().BeValidJson().Which.Should().BeEquivalentTo(_expectedResponse);
        }

        [When(@"user makes an order with unvalid input")]
        public async Task WhenUserMakesAnOrderWithUnvalidInput()
        {
            var _postInvalidOrder = @"
            {
                ""id"": 0,
                ""quantity"": -3,
                ""status"": ""a"",
                ""complete"": f
             }";

            _responseForInvalidInput = await _client.PostAsync($"{_settings.HostName}/v2/store/order", _postInvalidOrder);
        }

        [Then(@"an order is not saved")]
        public void ThenAnOrderIsNotSaved()
        {
            _responseForInvalidInput.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        public static string SerealizeJson(int id, int petId, int quantity, DateTime shipDate, string status, bool complete)
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
    }
}
