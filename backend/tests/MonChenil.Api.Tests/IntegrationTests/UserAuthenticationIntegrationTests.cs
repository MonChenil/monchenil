using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MonChenil.Data;
using System.Net;
using System.Net.Http.Json;

namespace MonChenil.Api.Tests.IntegrationTests
{
    public class UserAuthenticationIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private HttpClient Client { get; init; }
        private object TestUser { get; } = new
        {
            email = "test@example.com",
            password = "pTtlry08cy11*",
        };

        public UserAuthenticationIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            Client = _factory.CreateClient();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var scope = _factory.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Users.RemoveRange(context.Users);
            context.SaveChanges();
        }

        [Fact]
        public async Task RegisterUser_ReturnsOk()
        {
            var response = await Client.PostAsJsonAsync("/register", TestUser);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task LoginUser_ReturnsOk()
        {
            var registerResponse = await Client.PostAsJsonAsync("/register", TestUser);
            if (!registerResponse.IsSuccessStatusCode)
            {
                var loginResponse = await Client.PostAsJsonAsync("/login?useCookies=true", TestUser);
                loginResponse.EnsureSuccessStatusCode();
            }
            else
            {
                var loginResponse = await Client.PostAsJsonAsync("/login?useCookies=true", TestUser);
                Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
            }
        }

        [Fact]
        public async Task LoginUser_ReturnsUnauthorized_WhenInvalidCredentials()
        {
            var invalidUser = new
            {
                email = "invalid@example.com",
                password = "invalidPassword"
            };

            var response = await Client.PostAsJsonAsync("/login?useCookies=true", invalidUser);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}
