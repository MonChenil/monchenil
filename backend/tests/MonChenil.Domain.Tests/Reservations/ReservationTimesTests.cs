using MonChenil.Domain.Reservations;

namespace MonChenil.Domain.Tests.Reservations;

public class ReservationTimesTests
{
    public static IEnumerable<object[]> GetArrivalTimesSimpleData =>
        [
            [
                new DateTime(2021, 1, 1, 0, 0, 0),
                new DateTime(2021, 1, 1, 0, 0, 0),
                new List<DateTime>()
            ],
            [
                new DateTime(2021, 1, 1, 0, 0, 0),
                new DateTime(2021, 1, 1, 1, 0, 0),
                new List<DateTime>
                {
                    new(2021, 1, 1, 0, 0, 0),
                    new(2021, 1, 1, 0, 30, 0),
                }
            ],
            [
                new DateTime(2021, 1, 1, 0, 0, 0),
                new DateTime(2021, 1, 1, 2, 0, 0),
                new List<DateTime>
                {
                    new(2021, 1, 1, 0, 0, 0),
                    new(2021, 1, 1, 0, 30, 0),
                    new(2021, 1, 1, 1, 0, 0),
                    new(2021, 1, 1, 1, 30, 0),
                }
            ],
            [
                new DateTime(2021, 1, 1, 0, 1, 1),
                new DateTime(2021, 1, 1, 2, 0, 0),
                new List<DateTime>
                {
                    new(2021, 1, 1, 0, 30, 0),
                    new(2021, 1, 1, 1, 0, 0),
                    new(2021, 1, 1, 1, 30, 0),
                }
            ],
        ];

    public static IEnumerable<object[]> GetArrivalTimesConflictingReservationsData =>
        [
            [
                new DateTime(2021, 1, 1, 0, 0, 0),
                new DateTime(2021, 1, 1, 1, 0, 0),
                new List<Reservation>
                {
                    new(new(Guid.NewGuid()), "", new(2021, 1, 1, 0, 30, 0), new(2021, 1, 2, 0, 30, 0)),
                },
                new List<DateTime>
                {
                    new(2021, 1, 1, 0, 0, 0),
                }
            ],
            [
                new DateTime(2021, 1, 1, 0, 0, 0),
                new DateTime(2021, 1, 1, 2, 0, 0),
                new List<Reservation>
                {
                    new(new(Guid.NewGuid()), "", new(2021, 1, 1, 0, 30, 0), new(2021, 1, 2, 0, 30, 0)),
                },
                new List<DateTime>
                {
                    new(2021, 1, 1, 0, 0, 0),
                    new(2021, 1, 1, 1, 0, 0),
                    new(2021, 1, 1, 1, 30, 0),
                }
            ],
            [
                new DateTime(2021, 1, 1, 0, 0, 0),
                new DateTime(2021, 1, 1, 2, 0, 0),
                new List<Reservation>
                {
                    new(new(Guid.NewGuid()), "", new(2020, 12, 31, 0, 30, 0), new(2021, 1, 1, 0, 30, 0)),
                    new(new(Guid.NewGuid()), "", new(2021, 1, 1, 1, 0, 0), new(2021, 1, 2, 1, 0, 0)),
                },
                new List<DateTime>
                {
                    new(2021, 1, 1, 0, 0, 0),
                    new(2021, 1, 1, 1, 30, 0),
                }
            ],
        ];

    private readonly IReservationsRepository _reservationsRepository;
    private readonly IReservationTimes _reservationTimes;

    public ReservationTimesTests()
    {
        _reservationsRepository = new FakeReservationsRepository();
        _reservationTimes = new ReservationTimes(_reservationsRepository);
    }

    [Theory]
    [MemberData(nameof(GetArrivalTimesSimpleData))]
    public void GetArrivalTimes_ShouldReturnExpectedTimes(
        DateTime startDate,
        DateTime endDate,
        List<DateTime> expectedTimes)
    {
        var result = _reservationTimes.GetArrivalTimes(startDate, endDate);
        Assert.Equal(expectedTimes, result);
    }

    [Theory]
    [MemberData(nameof(GetArrivalTimesConflictingReservationsData))]
    public void GetArrivalTimes_ShouldNotReturnTimesWithConflictingReservations(
        DateTime startDate,
        DateTime endDate,
        List<Reservation> reservations,
        List<DateTime> expectedTimes)
    {
        foreach (var reservation in reservations)
        {
            _reservationsRepository.AddReservation(reservation);
        }

        var result = _reservationTimes.GetArrivalTimes(startDate, endDate);
        Assert.Equal(expectedTimes, result);
    }

}