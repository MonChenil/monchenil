using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MonChenil.Data;
using MonChenil.Domain.Models;
using MonChenil.Domain.Pets;
using MonChenil.Infrastructure.Pets;
using MonChenil.Infrastructure.Repositories;
using MonChenil.Infrastructure.Users;

var builder = WebApplication.CreateBuilder(args);

// Database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Authentication
builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddScoped<IPetsRepository, PetsRepository>();
builder.Services.AddScoped<IRepository<TimeSlot>, EntityFrameworkRepository<TimeSlot>>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Route to check if the user is authenticated
app.MapGet("/is-authenticated", (ClaimsPrincipal claims) =>
{
    return claims.Identity?.IsAuthenticated == true;
});

// Route to logout
app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
});

app.UseHttpsRedirection();

app.MapIdentityApi<ApplicationUser>();
app.MapControllers();

app.UseAuthorization();

app.Run();