using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonChenil.Domain.Pets;


namespace MonChenil.Infrastructure.Data.Configurations;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.HasKey(pet => pet.Id);
        builder.Property(pet => pet.Id).HasConversion(
            id => id.Value,
            value => new PetId(value)
        );
        builder.ToTable("Pets")
            .HasDiscriminator<PetType>("Type")
            .HasValue<Dog>(PetType.Dog)
            .HasValue<Cat>(PetType.Cat);

        builder.Property(pet => pet.Name);
        builder.Property(pet => pet.OwnerId);
    }
}
