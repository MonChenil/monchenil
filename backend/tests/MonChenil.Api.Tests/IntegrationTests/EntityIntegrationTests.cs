using Microsoft.AspNetCore.Mvc.Testing;
using MonChenil.Domain.Pets;
using MonChenil.Api.Requests;
using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using MonChenil.Data;

namespace MonChenil.Api.Tests.IntegrationTests
{
    public class PetsControllerIntegrationTests : BaseIntegrationTests
    {
        private const string TEST_PET_ID = "701605119112944";
        public PetsControllerIntegrationTests(WebApplicationFactory<Program> factory) : base(factory)
        {
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            EnsureTestPetDeleted(context);
            context.SaveChanges();
        }

        private static void EnsureTestPetDeleted(ApplicationDbContext context)
        {
            var testPet = context.Pets.Find(new PetId(TEST_PET_ID));
            if (testPet == null)
            {
                return;
            }

            context.Pets.Remove(testPet);
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

            var validPetIdRequest = new CreatePetRequest(new PetId(TEST_PET_ID), "Fluffy", PetType.Dog);
            var createResponse = await Client.PostAsJsonAsync("/pets", validPetIdRequest);
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);

            var getResponse = await Client.GetAsync("/pets");
            var pets = await getResponse.Content.ReadFromJsonAsync<List<Dog>>();
            Assert.NotNull(pets);
            Assert.Contains(pets, p => p.Id == validPetIdRequest.Id);
        }
    }
}
