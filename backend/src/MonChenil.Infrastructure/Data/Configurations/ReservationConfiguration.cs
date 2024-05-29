using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MonChenil.Domain.Reservations;


namespace MonChenil.Infrastructure.Data.Configurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(reservation => reservation.Id);
        builder.Property(reservation => reservation.Id).HasConversion(
            id => id.Value,
            value => new ReservationId(value)
        );
        builder.Property(reservation => reservation.StartDate);
        builder.Property(reservation => reservation.EndDate);
        builder.Property(reservation => reservation.OwnerId);
        builder.HasMany(reservation => reservation.Pets)
            .WithMany();
    }
}