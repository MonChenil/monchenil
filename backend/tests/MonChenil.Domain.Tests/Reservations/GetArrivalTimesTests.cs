using MonChenil.Domain.Pets;
using MonChenil.Domain.Reservations;

namespace MonChenil.Domain.Tests.Reservations;

public class GetArrivalTimesTests
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IReservationTimes _reservationTimes;

    public GetArrivalTimesTests()
    {
        _reservationsRepository = new FakeReservationsRepository();
        _reservationTimes = new ReservationTimes(_reservationsRepository);
    }

    public static IEnumerable<object[]> GetArrivalTimesSimpleData =>
        [
            [
                new DateTime(2024, 1, 1, 8, 0, 0),
                new DateTime(2024, 1, 1, 9, 0, 0),
                new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") },
                new List<DateTime>()
            ],
            [
                new DateTime(2024, 1, 1, 8, 0, 0),
                new DateTime(2024, 1, 1, 10, 0, 0),
                new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") },
                new List<DateTime>
                {
                    new(2024, 1, 1, 9, 0, 0),
                    new(2024, 1, 1, 9, 30, 0),
                }
            ],
            [
                new DateTime(2024, 1, 1, 9, 0, 0),
                new DateTime(2024, 1, 1, 11, 0, 0),
                new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") },
                new List<DateTime>
                {
                    new(2024, 1, 1, 9, 0, 0),
                    new(2024, 1, 1, 9, 30, 0),
                    new(2024, 1, 1, 10, 0, 0),
                    new(2024, 1, 1, 10, 30, 0),
                }
            ],
            [
                new DateTime(2024, 1, 1, 9, 1, 1),
                new DateTime(2024, 1, 1, 11, 0, 0),
                new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") },
                new List<DateTime>
                {
                    new(2024, 1, 1, 9, 30, 0),
                    new(2024, 1, 1, 10, 0, 0),
                    new(2024, 1, 1, 10, 30, 0),
                }
            ],
            [
                new DateTime(2024, 1, 1, 17, 1, 1),
                new DateTime(2024, 1, 1, 19, 0, 0),
                new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") },
                new List<DateTime>
                {
                    new(2024, 1, 1, 17, 30, 0),
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

    public static IEnumerable<object[]> GetArrivalTimesConflictingReservationsData =>
        [
            [
                new DateTime(2024, 1, 1, 9, 0, 0),
                new DateTime(2024, 1, 1, 10, 0, 0),
                new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") },
                new List<Reservation>
                {
                    new(new(Guid.NewGuid()), "", new(2024, 1, 1, 9, 30, 0), new(2024, 1, 2, 0, 30, 0)),
                },
                new List<DateTime>
                {
                    new(2024, 1, 1, 9, 0, 0),
                }
            ],
            [
                new DateTime(2024, 1, 1, 9, 0, 0),
                new DateTime(2024, 1, 1, 11, 0, 0),
                new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") },
                new List<Reservation>
                {
                    new(new(Guid.NewGuid()), "", new(2024, 1, 1, 9, 30, 0), new(2024, 1, 2, 0, 30, 0)),
                },
                new List<DateTime>
                {
                    new(2024, 1, 1, 9, 0, 0),
                    new(2024, 1, 1, 10, 0, 0),
                    new(2024, 1, 1, 10, 30, 0),
                }
            ],
            [
                new DateTime(2024, 1, 1, 9, 0, 0),
                new DateTime(2024, 1, 1, 11, 0, 0),
                new List<Pet> { new Dog(new("287383724237054"), "name", "ownerId") },
                new List<Reservation>
                {
                    new(new(Guid.NewGuid()), "", new(2020, 12, 31, 0, 30, 0), new(2024, 1, 1, 9, 30, 0)),
                    new(new(Guid.NewGuid()), "", new(2024, 1, 1, 10, 0, 0), new(2024, 1, 2, 1, 0, 0)),
                },
                new List<DateTime>
                {
                    new(2024, 1, 1, 9, 0, 0),
                    new(2024, 1, 1, 10, 30, 0),
                }
            ],
        ];

    [Theory]
    [MemberData(nameof(GetArrivalTimesSimpleData))]
    public void GetArrivalTimes_ShouldReturnExpectedTimes(
        DateTime startDate,
        DateTime endDate,
        List<Pet> pets,
        List<DateTime> expectedTimes)
    {
        var result = _reservationTimes.GetArrivalTimes(startDate, endDate, pets);
        Assert.Equal(expectedTimes, result);
    }

    [Theory]
    [MemberData(nameof(GetArrivalTimesConflictingReservationsData))]
    public void GetArrivalTimes_ShouldNotReturnTimesWithConflictingReservations(
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

        var result = _reservationTimes.GetArrivalTimes(startDate, endDate, pets);
        Assert.Equal(expectedTimes, result);
    }

}