using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MonChenil.Infrastructure.Users;

namespace MonChenil.Infrastructure.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(user => user.Id);
        
        builder.HasMany(user => user.Pets)
            .WithOne()
            .HasForeignKey(pet => pet.OwnerId)
            .IsRequired();

        builder.HasMany(user => user.Reservations)
            .WithOne()
            .HasForeignKey(reservation => reservation.OwnerId)
            .IsRequired();
    }
}
