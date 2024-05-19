using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MonChenil.Data;
using MonChenil.Entities;

var builder = WebApplication.CreateBuilder(args);

// Database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Authentication
builder.Services.AddDefaultIdentity<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddApiEndpoints();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Example of a protected endpoint: Load the current user's information
app.MapGet("/me", async (ClaimsPrincipal claims, ApplicationDbContext db) =>
{
    string? userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);
    if (userId == null)
    {
        return Results.Unauthorized();
    }

    var user = await db.Users.FindAsync(userId);
    if (user == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(user);
}).RequireAuthorization();

// Route to check if the user is authenticated
app.MapGet("/is-authenticated", (ClaimsPrincipal claims) =>
{
    return claims.Identity?.IsAuthenticated == true;
});

app.UseHttpsRedirection();

app.MapIdentityApi<User>();
app.MapControllers();

app.UseAuthorization();

app.Run();
