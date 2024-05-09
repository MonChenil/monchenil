using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MonChenil.Entities;
using MonChenil.Entities.Pets;

namespace MonChenil.Data;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<Cat> Cats { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pet>()
            .HasDiscriminator(p => p.Type)
            .HasValue<Dog>(PetType.Dog)
            .HasValue<Cat>(PetType.Cat);
    }
}
