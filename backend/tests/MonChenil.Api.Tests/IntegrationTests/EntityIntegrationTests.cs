using Microsoft.AspNetCore.Mvc.Testing;
using MonChenil.Domain.Pets;
using MonChenil.Api.Requests;
using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using MonChenil.Data;

namespace MonChenil.Api.Tests.IntegrationTests
{
    public class PetsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private HttpClient Client { get; init; }
        private object TestUser { get; } = new
        {
            email = "test@example.com",
            password = "pTtlry08cy11*"
        };

        public PetsControllerIntegrationTests(WebApplicationFactory<Program> factory)
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
            context.Pets.RemoveRange(context.Pets);
            context.SaveChanges();
        }

        private async Task EnsureAuthenticated()
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

        [Fact]
        public async Task CreatePet_ReturnsBadRequest_WhenPetIdIsNot15DigitString()
        {
            await EnsureAuthenticated();

            var invalidPetIdRequest = new
            {
                petId = "123",
                name = "Fluffy",
                type = PetType.Dog
            };

            var response = await Client.PostAsJsonAsync("/pets", invalidPetIdRequest);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreatePet_ReturnsOk_AndCreatesPet_WhenPetIdIsValid()
        {
            await EnsureAuthenticated();
            
            var validPetIdRequest = new CreatePetRequest(new PetId("123456789012345"), "Fluffy", PetType.Dog);
            var createResponse = await Client.PostAsJsonAsync("/pets", validPetIdRequest);
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);

            var getResponse = await Client.GetAsync("/pets");
            var pets = await getResponse.Content.ReadFromJsonAsync<List<Dog>>();
            Assert.NotNull(pets);
            Assert.Contains(pets, p => p.Id == validPetIdRequest.Id);
        }
    }
}
