using Microsoft.AspNetCore.Mvc.Testing;
using MonChenil.Api.Requests;
using MonChenil.Domain.Pets;
using MonChenil.Domain.Reservations;
using System.Net;
using System.Net.Http.Json;

namespace MonChenil.Api.Tests.IntegrationTests
{
    public class ReservationsControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        private readonly object _testUser = new
        {
            email = "test@example.com",
            password = "pTtlry08cy11*"
        };

        public ReservationsControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
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
            return await _client.PostAsJsonAsync("/register", _testUser);
        }

        private async Task<HttpResponseMessage> LoginTestUser()
        {
            return await _client.PostAsJsonAsync("/login?useCookies=true", _testUser);
        }

        [Fact]
        public async Task ReservationPage_Displayed_WhenUserIsAuthenticated()
        {
            await EnsureAuthenticated();

            var response = await _client.GetAsync("/reservations");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ReservationPage_NotDisplayed_WhenUserIsNotAuthenticated()
        {
            var response = await _client.GetAsync("/reservations");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateReservation_ReturnsOk_AndAddsReservation()
        {
            await EnsureAuthenticated();

            var newReservationRequest = new CreateReservationRequest(
                StartDate: DateTime.Now.AddDays(1),
                EndDate: DateTime.Now.AddDays(2),
                PetIds: new List<PetId> { new PetId("123456789012345") }
            );

            var createResponse = await _client.PostAsJsonAsync("/reservations", newReservationRequest);
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);

            var response = await _client.GetAsync("/reservations");
            var reservations = await response.Content.ReadFromJsonAsync<List<Reservation>>();
            Assert.NotNull(reservations);
            Assert.NotEmpty(reservations);
        }
    }
}
