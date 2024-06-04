using MonChenil.Domain.Pets;
using MonChenil.Domain.Reservations;

namespace MonChenil.Domain.Tests.Reservations;

public class TimesAvailabilityTests
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IReservationTimes _reservationTimes;

    public TimesAvailabilityTests()
    {
        _reservationsRepository = new FakeReservationsRepository();
        _reservationTimes = new ReservationTimes(_reservationsRepository);
    }

    [Fact]
    public void AreTimesAvailableForPets_WhenTimesAreAvailable_ReturnsTrue()
    {
        var startDate = new DateTime(2024, 1, 3, 9, 30, 0);
        var endDate = new DateTime(2024, 1, 4, 9, 30, 0);
        var pets = new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") };

        var result = _reservationTimes.AreTimesAvailableForPets(startDate, endDate, pets);

        Assert.True(result);
    }

    [Fact]
    public void AreTimesAvailableForPets_WhenTimesAreNotAvailable_ReturnsFalse()
    {
        var existingReservation = new Reservation(
            new(Guid.NewGuid()),
            "otherOwnerId",
            new DateTime(2024, 1, 3, 9, 30, 0),
            new DateTime(2024, 1, 4, 9, 30, 0)
        );

        List<Pet> existingReservationPets = [
            new Dog(new("480531850353143"), "name", "ownerId"),
            new Dog(new("358488615963723"), "name", "ownerId"),
            new Dog(new("358488615963723"), "name", "ownerId"),
        ];

        existingReservation.AddPets(existingReservationPets);
        _reservationsRepository.AddReservation(existingReservation);

        var startDate = new DateTime(2024, 1, 3, 9, 30, 0);
        var endDate = new DateTime(2024, 1, 4, 9, 30, 0);
        List<Pet> pets = [new Dog(new("287383724237054"), "name", "ownerId")];

        var result = _reservationTimes.AreTimesAvailableForPets(startDate, endDate, pets);

        Assert.False(result);
    }
}