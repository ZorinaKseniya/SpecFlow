using FluentAssertions;
using FluentAssertions.Json;
using SpecFlowProject.Helpers;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace SpecFlowProject.Steps
{
    [Binding]
    public class DeleteAUserSteps : BaseSteps
    {
        private const string _jsonPostUser = @"
        {
          ""id"": 34,
          ""username"": ""#name"",
          ""firstName"": ""string"",
          ""lastName"": ""string"",
          ""email"": ""string"",
          ""password"": ""string"",
          ""phone"": ""string"",
          ""userStatus"": 0
        }";

        private const string _expectedResponse = @"
        {
            ""code"": 200,
            ""type"": ""unknown"",
            ""message"": ""ExistingUser""
        }";

        [Given(@"user with name ""(.*)"" has been created")]
        public async Task GivenUserWithNameHasBeenCreated(string nameOfUser)
        {
            await _client.GetAsync($"{_settings.HostName}/v2/user/login");
            await _client.PostAsync($"{_settings.HostName}/v2/user", _jsonPostUser.Replace("#name", nameOfUser));
        }
        
        [Given(@"petstore administrator is a logged in user")]
        public Task GivenPetstoreAdministratorIsALoggedInUser()
        {
            return _client.GetAsync($"{_settings.HostName}/v2/user/login");
        }
        
        [Given(@"user with name ""(.*)"" does not exist")]
        public Task GivenUserWithNameDoesNotExist(string username)
        {
            return _client.DeleteAsync($"{_settings.HostName}/v2/user/{username}");
        }         
        
        [When(@"petstore administrator tries to delete a user with name ""(.*)""")]
        public async Task WhenPetstoreAdministratorTriesToDeleteAUser(string username)
        {
            _response = await _client.DeleteAsync($"{_settings.HostName}/v2/user/{username}");
        }
        
        [Then(@"user is deleted")]
        public async Task ThenUserIsDeleted()
        {
            var actualResponse = await _response.Content.ReadAsStringAsync();
            actualResponse.Should().BeValidJson().Which.Should().BeEquivalentTo(_expectedResponse);
        }
        
        [Then(@"user gets NotFound as User does not exists")]
        public void ThenUserGetsNotFound()
        {
            _response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
