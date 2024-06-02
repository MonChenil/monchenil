using MonChenil.Domain.Pets;
using MonChenil.Domain.Reservations;

namespace MonChenil.Domain.Tests.Reservations;

public class GetDepartureTimesTests
{
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
                new DateTime(2024, 1, 1, 8, 0, 0),
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
                new DateTime(2024, 1, 1, 8, 0, 0),
                new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") },
                new List<DateTime>
                {
                    new(2024, 1, 3, 9, 0, 0),
                    new(2024, 1, 3, 9, 30, 0),
                    new(2024, 1, 3, 10, 0, 0),
                    new(2024, 1, 3, 10, 30, 0),
                }
            ],
        ];

    public static IEnumerable<object[]> GetDepartureTimesConflictingReservationsData =>
        [
            [
                new DateTime(2024, 1, 3, 9, 0, 0),
                new DateTime(2024, 1, 3, 10, 0, 0),
                new DateTime(2024, 1, 1, 8, 0, 0),
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
        DateTime arrivalTime,
        List<Pet> pets,
        List<DateTime> expectedTimes)
    {
        var departureTimes = _reservationTimes.GetDepartureTimes(startDate, endDate, arrivalTime, pets);
        Assert.Equal(expectedTimes, departureTimes);
    }

    [Theory]
    [MemberData(nameof(GetDepartureTimesConflictingReservationsData))]
    public void GetDepartureTimes_WithConflictingReservations_ShouldReturnExpectedTimes(
        DateTime startDate,
        DateTime endDate,
        DateTime arrivalTime,
        List<Pet> pets,
        List<Reservation> reservations,
        List<DateTime> expectedTimes)
    {
        foreach (var reservation in reservations)
        {
            _reservationsRepository.AddReservation(reservation);
        }

        var departureTimes = _reservationTimes.GetDepartureTimes(startDate, endDate, arrivalTime, pets);
        Assert.Equal(expectedTimes, departureTimes);
    }

    [Fact]
    public void GetDepartureTimes_ShouldNotReturnTimesIfMaxCapacityReachedDuringReservation()
    {
        // MAX_CAPACITY = 3;

        List<Pet> pets = [
            new Dog(new("287383724237054"), "name", "ownerId"),
            new Dog(new("480531850353143"), "name", "ownerId"),
            new Dog(new("358488615963723"), "name", "ownerId"),
        ];

        Reservation existingReservation = new(new(Guid.NewGuid()), "", new(2024, 1, 3, 11, 0, 0), new(2024, 1, 4, 11, 0, 0));
        existingReservation.AddPets(pets);
        _reservationsRepository.AddReservation(existingReservation);

        DateTime startDate = new(2024, 1, 3, 9, 0, 0);
        DateTime endDate = new(2024, 1, 3, 23, 0, 0);
        DateTime arrivalTime = new(2024, 1, 1, 9, 0, 0);
        List<DateTime> expectedTimes = [
            new(2024, 1, 3, 9, 0, 0),
            new(2024, 1, 3, 9, 30, 0),
            new(2024, 1, 3, 10, 0, 0),
            new(2024, 1, 3, 10, 30, 0),
        ];

        List<DateTime> departureTimes = _reservationTimes.GetDepartureTimes(startDate, endDate, arrivalTime, pets);

        Assert.Equal(expectedTimes, departureTimes);
    }
}