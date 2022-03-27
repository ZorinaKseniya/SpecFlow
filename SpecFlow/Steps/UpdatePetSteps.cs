using FluentAssertions;
using FluentAssertions.Json;
using SpecFlowProject.Helpers;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowProject.Steps
{
    [Binding]
    public class UpdatePetSteps: BaseSteps
    {
        private string _expectedResponse;
        private const string _jsonPostPet = @"
        {
          ""id"": 14,
          ""category"": {
            ""id"": 0,
            ""name"": ""string""
          },
          ""name"": ""OldName"",
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

        private const string _jsonUpdatePet = @"
        {
          ""id"": 14,
          ""category"": {
            ""id"": 1,
            ""name"": ""string""
          },
          ""name"": ""NewName"",
          ""photoUrls"": [
            ""stringNew""
          ],
          ""tags"": [
            {
              ""id"": 1,
              ""name"": ""tagNew""
            }
          ],
          ""status"": ""sold""
        }";

        [Given(@"user has added his pet to store with one set of parameters")]
        public Task GivenUserHasAddedHisPetToStoreWithOneSetOfParameters()
        {
            return _client.PostAsync($"{_settings.HostName}/v2/pet", _jsonPostPet);
        }
        
        [When(@"user tries to update all its parameters")]
        public async Task WhenUserTriesToUpdateAllItsParameters()
        {
            _response = await _client.PostAsync($"{_settings.HostName}/v2/pet", _jsonUpdatePet);
        }
        
        [Then(@"all new parametres have been saved")]
        public async Task ThenAllNewParametresHaveBeenSaved()
        {
            _expectedResponse = _jsonUpdatePet;
            var actualResponse = await _response.Content.ReadAsStringAsync();
            actualResponse.Should().BeValidJson().Which.Should().BeEquivalentTo(_expectedResponse);
        }
    }
}
