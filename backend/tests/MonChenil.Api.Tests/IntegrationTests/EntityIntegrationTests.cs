using Microsoft.AspNetCore.Mvc.Testing;
using MonChenil.Domain.Pets;
using MonChenil.Api.Requests;
using System.Net;
using System.Net.Http.Json;

namespace MonChenil.Api.Tests.IntegrationTests
{
public class PetsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public PetsControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreatePet_ReturnsBadRequest_WhenPetIdIsNot15DigitString()
        {
            var client = _factory.CreateClient();

            var invalidPetIdRequest = new CreatePetRequest(new PetId("123"), "Fluffy", PetType.Dog);

            var response = await client.PostAsJsonAsync("/pets", invalidPetIdRequest);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreatePet_ReturnsOk_WhenPetIdIsValid()
        {
            var client = _factory.CreateClient();

            var validPetIdRequest = new CreatePetRequest(new PetId("123456789012345"), "Fluffy", PetType.Dog);

            var response = await client.PostAsJsonAsync("/pets", validPetIdRequest);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
