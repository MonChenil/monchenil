using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MonChenil.Data;
using MonChenil.Infrastructure.Users;
using System.Net;
using System.Net.Http.Json;

namespace MonChenil.Api.Tests.IntegrationTests
{
    public class UserAuthenticationIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> Factory;

        private HttpClient Client { get; init; }

        private const string LOGIN_TEST_USER_EMAIL = "LOGIN_TEST_USER_EMAIL@example.com";
        private const string LOGIN_TEST_USER_PASSWORD = "pTtlry08cy11*";
        private object LoginTestUser { get; } = new
        {
            email = LOGIN_TEST_USER_EMAIL,
            password = LOGIN_TEST_USER_PASSWORD,
        };

        private const string REGISTER_TEST_USER_EMAIL = "REGISTER_TEST_USER_EMAIL@example.com";
        private const string REGISTER_TEST_USER_PASSWORD = "pTtlry08cy11*";
        private object RegisterTestUser { get; } = new
        {
            email = REGISTER_TEST_USER_EMAIL,
            password = REGISTER_TEST_USER_PASSWORD,
        };

        public UserAuthenticationIntegrationTests(WebApplicationFactory<Program> factory)
        {
            Factory = factory;
            Client = Factory.CreateClient();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var scope = Factory.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ApplicationDbContext>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            EnsureLoginTestUserCreated(context, userManager);
            EnsureRegisterTestUserDeleted(context, userManager);
            context.SaveChanges();
        }

        private static void EnsureLoginTestUserCreated(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            var user = context.Users.Find(LOGIN_TEST_USER_EMAIL);
            if (user != null)
            {
                return;
            }

            var applicationUser = new ApplicationUser
            {
                Email = LOGIN_TEST_USER_EMAIL,
                UserName = LOGIN_TEST_USER_EMAIL,
            };

            userManager.CreateAsync(applicationUser, LOGIN_TEST_USER_PASSWORD).Wait();
        }

        private static void EnsureRegisterTestUserDeleted(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            var user = context.Users.Where(u => u.Email == REGISTER_TEST_USER_EMAIL).FirstOrDefault();
            if (user == null)
            {
                return;
            }

            userManager.DeleteAsync(user).Wait();
        }


        [Fact]
        public async Task RegisterUser_ReturnsOk()
        {
            var response = await Client.PostAsJsonAsync("/register", RegisterTestUser);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task LoginUser_ReturnsOk_WhenValidCredentials()
        {
            var response = await Client.PostAsJsonAsync("/login?useCookies=true", LoginTestUser);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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
