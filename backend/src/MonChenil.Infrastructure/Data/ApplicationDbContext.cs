using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonChenil.Infrastructure.Entities;
using MonChenil.Infrastructure.Pets;
using MonChenil.Infrastructure.Users;

namespace MonChenil.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<PetEntity> Pets { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
