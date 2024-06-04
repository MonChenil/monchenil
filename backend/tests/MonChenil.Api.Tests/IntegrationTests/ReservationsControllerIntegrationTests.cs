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
            EnsureTestReservationsDeleted(context);
        }

        private static void EnsureTestPetCreated(ApplicationDbContext context)
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

        private static void EnsureTestReservationsDeleted(ApplicationDbContext context)
        {
            context.Reservations.RemoveRange(context.Reservations);
            context.SaveChanges();
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
        public async Task CreateReservation_ReturnsOk_AndAddsReservation_WhenTimesAreAvailable()
        {
            await EnsureAuthenticated();

            var startDate = DateTime.Now.Date.AddDays(1).AddHours(9).AddMinutes(30);
            var endDate = DateTime.Now.Date.AddDays(2).AddHours(9).AddMinutes(30);

            var newReservationRequest = new CreateReservationRequest(
                StartDate: startDate,
                EndDate: endDate,
                PetIds: [new PetId("123456789012345")]
            );

            var createResponse = await Client.PostAsJsonAsync("/reservations", newReservationRequest);
            Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);

            var response = await Client.GetAsync("/reservations");
            var reservations = await response.Content.ReadFromJsonAsync<List<Reservation>>();
            Assert.NotNull(reservations);
            Assert.NotEmpty(reservations);
            Assert.Contains(reservations, r => r.StartDate == startDate && r.EndDate == endDate);
        }

        [Fact]
        public async Task CreateReservation_ReturnsOkThenBadRequest_WhenAddingReservationTwice()
        {
            await EnsureAuthenticated();

            var newReservationRequest = new CreateReservationRequest(
                StartDate: DateTime.Now.Date.AddDays(1).AddHours(9).AddMinutes(30),
                EndDate: DateTime.Now.Date.AddDays(2).AddHours(9).AddMinutes(30),
                PetIds: [new PetId("123456789012345")]
            );

            var createResponseA = await Client.PostAsJsonAsync("/reservations", newReservationRequest);
            var createResponseB = await Client.PostAsJsonAsync("/reservations", newReservationRequest);
            Assert.Equal(HttpStatusCode.OK, createResponseA.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, createResponseB.StatusCode);
        }
    }
}
