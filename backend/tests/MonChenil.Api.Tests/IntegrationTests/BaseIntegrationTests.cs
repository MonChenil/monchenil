using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MonChenil.Data;

namespace MonChenil.Api.Tests.IntegrationTests
{
    public class BaseIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        protected readonly WebApplicationFactory<Program> Factory;

        protected HttpClient Client { get; init; }
        private object TestUser { get; } = new
        {
            email = "BaseIntegrationTests@example.com",
            password = "pTtlry08cy11*"
        };

        public BaseIntegrationTests(WebApplicationFactory<Program> factory)
        {
            Factory = factory;
            Client = Factory.CreateClient();
        }

        protected async Task EnsureAuthenticated()
        {
            var registerResponse = await RegisterTestUser();
            if (!registerResponse.IsSuccessStatusCode)
            {
                var loginResponse = await LoginTestUser();
                loginResponse.EnsureSuccessStatusCode();
            }
        }

        private async Task<HttpResponseMessage> RegisterTestUser()
        {
            return await Client.PostAsJsonAsync("/register", TestUser);
        }

        private async Task<HttpResponseMessage> LoginTestUser()
        {
            return await Client.PostAsJsonAsync("/login?useCookies=true", TestUser);
        }
    }
}
