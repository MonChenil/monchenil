using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MonChenil.Api.Requests;
using MonChenil.Data;
using MonChenil.Domain.Pets;
using MonChenil.Domain.Reservations;
using MonChenil.Infrastructure.Users;
using System.Net;
using System.Net.Http.Json;

namespace MonChenil.Api.Tests.IntegrationTests
{
    public class ReservationsControllerIntegrationTests : BaseIntegrationTests
    {
        private const string TEST_USER_EMAIL = "TEST_USER_EMAIL@example.com";
        private const string RESERVATION_TEST_USER_PASSWORD = "pTtlry08cy11*";

        public ReservationsControllerIntegrationTests(WebApplicationFactory<Program> factory) : base(factory)
        {
            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            EnsureTestUserCreated(context, userManager);
            EnsureTestPetCreated(context);
        }

        private void EnsureTestPetCreated(ApplicationDbContext context)
        {
            var testUser = context.Users.Where(u => u.Email == TEST_USER_EMAIL).FirstOrDefault();
            Assert.NotNull(testUser);

            Pet testPet = new Cat(
                new PetId("123456789012345"),
                "TestPet",
                testUser.Id
            );

            var pet = context.Pets.Find(testPet.Id);
            if (pet != null)
            {
                return;
            }

            context.Pets.Add(testPet);
            context.SaveChanges();
        }

        private static void EnsureTestUserCreated(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            var user = context.Users.Find(TEST_USER_EMAIL);
            if (user != null)
            {
                return;
            }

            var applicationUser = new ApplicationUser
            {
                Email = TEST_USER_EMAIL,
                UserName = TEST_USER_EMAIL,
            };

            userManager.CreateAsync(applicationUser, RESERVATION_TEST_USER_PASSWORD).Wait();
        }

        [Fact]
        public async Task ReservationPage_Displayed_WhenUserIsAuthenticated()
        {
            await EnsureAuthenticated();

            var response = await Client.GetAsync("/reservations");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ReservationPage_NotDisplayed_WhenUserIsNotAuthenticated()
        {
            var response = await Client.GetAsync("/reservations");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task CreateReservation_ReturnsOk_AndAddsReservation()
        {
            await EnsureAuthenticated();

            var newReservationRequest = new CreateReservationRequest(
                StartDate: DateTime.Now.AddDays(1),
                EndDate: DateTime.Now.AddDays(2),
                PetIds: [new PetId("123456789012345")]
            );

            var createResponse = await Client.PostAsJsonAsync("/reservations", newReservationRequest);
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);

            var response = await Client.GetAsync("/reservations");
            var reservations = await response.Content.ReadFromJsonAsync<List<Reservation>>();
            Assert.NotNull(reservations);
            Assert.NotEmpty(reservations);
        }
    }
}
