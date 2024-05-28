using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonChenil.Infrastructure.Entities;

namespace MonChenil.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Pet> Pets { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
