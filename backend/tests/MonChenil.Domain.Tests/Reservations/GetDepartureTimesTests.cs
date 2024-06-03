using MonChenil.Domain.Pets;
using MonChenil.Domain.Reservations;

namespace MonChenil.Domain.Tests.Reservations;

public class GetDepartureTimesTests
{
    // @note: Kennel max capacity is assumed to be 3 pets
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IReservationTimes _reservationTimes;

    public GetDepartureTimesTests()
    {
        _reservationsRepository = new FakeReservationsRepository();
        _reservationTimes = new ReservationTimes(_reservationsRepository);
    }

    public static IEnumerable<object[]> GetDepartureTimesSimpleData =>
        [
            [
                new DateTime(2024, 1, 3, 9, 0, 0),
                new DateTime(2024, 1, 3, 10, 0, 0),
                new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") },
                new List<DateTime>
                {
                    new(2024, 1, 3, 9, 0, 0),
                    new(2024, 1, 3, 9, 30, 0),
                }
            ],
            [
                new DateTime(2024, 1, 3, 8, 0, 0),
                new DateTime(2024, 1, 3, 11, 0, 0),
                new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") },
                new List<DateTime>
                {
                    new(2024, 1, 3, 9, 0, 0),
                    new(2024, 1, 3, 9, 30, 0),
                    new(2024, 1, 3, 10, 0, 0),
                    new(2024, 1, 3, 10, 30, 0),
                }
            ],
            [
                new DateTime(2024, 1, 3, 8, 0, 0),
                new DateTime(2024, 1, 3, 11, 0, 0),
                new List<Pet> {
                    new Dog(new("287383724237054"), "name", "ownerId"),
                    new Dog(new("480531850353143"), "name", "ownerId"),
                    new Dog(new("358488615963723"), "name", "ownerId"),
                    new Dog(new("983987398738490"), "name", "ownerId"),
                },
                new List<DateTime> { }
            ],
        ];

    public static IEnumerable<object[]> GetDepartureTimesConflictingReservationsData =>
        [
            [
                new DateTime(2024, 1, 3, 9, 0, 0),
                new DateTime(2024, 1, 3, 10, 0, 0),
                new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") },
                new List<Reservation>
                {
                    new(new(Guid.NewGuid()), "", new(2024, 1, 1, 9, 30, 0), new(2024, 1, 3, 9, 30, 0)),
                },
                new List<DateTime>
                {
                    new(2024, 1, 3, 9, 0, 0),
                }
            ],
        ];

    [Theory]
    [MemberData(nameof(GetDepartureTimesSimpleData))]
    public void GetDepartureTimes_ShouldReturnExpectedTimes(
        DateTime startDate,
        DateTime endDate,
        List<Pet> pets,
        List<DateTime> expectedTimes)
    {
        var departureTimes = _reservationTimes.GetDepartureTimes(startDate, endDate, pets);
        Assert.Equal(expectedTimes, departureTimes);
    }

    [Theory]
    [MemberData(nameof(GetDepartureTimesConflictingReservationsData))]
    public void GetDepartureTimes_WithConflictingReservations_ShouldReturnExpectedTimes(
        DateTime startDate,
        DateTime endDate,
        List<Pet> pets,
        List<Reservation> reservations,
        List<DateTime> expectedTimes)
    {
        foreach (var reservation in reservations)
        {
            _reservationsRepository.AddReservation(reservation);
        }

        var departureTimes = _reservationTimes.GetDepartureTimes(startDate, endDate, pets);
        Assert.Equal(expectedTimes, departureTimes);
    }

    [Fact]
    public void GetDepartureTimes_MaxCapacityReachedByOneReservation_ShouldNotReturnTimesAfterThatReservationStart()
    {
        List<Pet> pets = [
            new Dog(new("287383724237054"), "name", "ownerId"),
            new Dog(new("480531850353143"), "name", "ownerId"),
            new Dog(new("358488615963723"), "name", "ownerId"),
        ];

        Reservation existingReservation = new(new(Guid.NewGuid()), "", new(2024, 1, 3, 11, 0, 0), new(2024, 1, 4, 11, 0, 0));
        existingReservation.AddPets(pets);
        _reservationsRepository.AddReservation(existingReservation);

        DateTime startDate = new(2024, 1, 3, 9, 0, 0);
        DateTime endDate = new(2024, 1, 4, 23, 0, 0);
        List<DateTime> expectedTimes = [
            new(2024, 1, 3, 9, 0, 0),
            new(2024, 1, 3, 9, 30, 0),
            new(2024, 1, 3, 10, 0, 0),
            new(2024, 1, 3, 10, 30, 0),
        ];

        List<DateTime> departureTimes = _reservationTimes.GetDepartureTimes(startDate, endDate, pets);

        Assert.Equal(expectedTimes, departureTimes);
    }

    [Fact]
    public void GetDepartureTimes_MaxCapacityReachedByOneReservation_ShouldNotReturnTimesAfterThatReservationEnd()
    {
        List<Pet> pets = [
            new Dog(new("287383724237054"), "name", "ownerId"),
            new Dog(new("480531850353143"), "name", "ownerId"),
            new Dog(new("358488615963723"), "name", "ownerId"),
        ];

        Reservation existingReservation = new(new(Guid.NewGuid()), "", new(2024, 1, 3, 11, 0, 0), new(2024, 1, 4, 11, 0, 0));
        existingReservation.AddPets(pets);
        _reservationsRepository.AddReservation(existingReservation);

        DateTime startDate = new(2024, 1, 4, 11, 0, 0);
        DateTime endDate = new(2024, 1, 4, 13, 0, 0);
        List<DateTime> expectedTimes = [];

        List<DateTime> departureTimes = _reservationTimes.GetDepartureTimes(startDate, endDate, pets);

        Assert.Equal(expectedTimes, departureTimes);
    }

    [Fact]
    public void GetDepartureTimes_MaxCapacityReachedByStackedReservations_ShouldNotReturnTimesAfterMaxCapacityReached()
    {
        List<Pet> pets = [
            new Dog(new("287383724237054"), "name", "ownerId"),
            new Dog(new("480531850353143"), "name", "ownerId"),
            new Dog(new("358488615963723"), "name", "ownerId"),
            new Dog(new("358488615963723"), "name", "ownerId"),
        ];

        Reservation r1 = new(new(Guid.NewGuid()), "", new(2024, 1, 3, 10, 0, 0), new(2024, 1, 4, 10, 0, 0));
        r1.AddPets([pets[0]]);

        Reservation r2 = new(new(Guid.NewGuid()), "", new(2024, 1, 3, 11, 30, 0), new(2024, 1, 4, 11, 30, 0));
        r2.AddPets([pets[1], pets[2]]);

        _reservationsRepository.AddReservation(r1);
        _reservationsRepository.AddReservation(r2);

        DateTime startDate = new(2024, 1, 3, 9, 0, 0);
        DateTime endDate = new(2024, 1, 3, 23, 0, 0);
        List<DateTime> expectedTimes = [
            new(2024, 1, 3, 9, 0, 0),
            new(2024, 1, 3, 9, 30, 0),
            new(2024, 1, 3, 10, 30, 0),
            new(2024, 1, 3, 11, 0, 0),
        ];

        List<DateTime> departureTimes = _reservationTimes.GetDepartureTimes(startDate, endDate, [pets[2]]);

        Assert.Equal(expectedTimes, departureTimes);
    }

    [Fact]
    public void GetDepartureTimes_MaxCapacityReachedByStackedReservations_ShouldNotReturnTimesWhenThereIsRoomAgain()
    {
        List<Pet> pets = [
            new Dog(new("287383724237054"), "name", "ownerId"),
            new Dog(new("480531850353143"), "name", "ownerId"),
            new Dog(new("358488615963723"), "name", "ownerId"),
            new Dog(new("358488615963723"), "name", "ownerId"),
        ];

        Reservation r1 = new(new(Guid.NewGuid()), "", new(2024, 1, 3, 10, 0, 0), new(2024, 1, 4, 10, 0, 0));
        r1.AddPets([pets[0]]);

        Reservation r2 = new(new(Guid.NewGuid()), "", new(2024, 1, 3, 11, 30, 0), new(2024, 1, 4, 11, 30, 0));
        r2.AddPets([pets[1], pets[2]]);

        _reservationsRepository.AddReservation(r1);
        _reservationsRepository.AddReservation(r2);

        DateTime startDate = new(2024, 1, 4, 10, 0, 0);
        DateTime endDate = new(2024, 1, 4, 13, 0, 0);
        List<DateTime> expectedTimes = [];

        List<DateTime> departureTimes = _reservationTimes.GetDepartureTimes(startDate, endDate, [pets[2]]);

        Assert.Equal(expectedTimes, departureTimes);
    }
}