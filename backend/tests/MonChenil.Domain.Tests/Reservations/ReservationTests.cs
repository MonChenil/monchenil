using MonChenil.Domain.Reservations;

namespace MonChenil.Domain.Tests.Reservations;

public class ReservationTests
{
    [Fact]
    public void EndDate_DayBeforeStartDate_ThrowsReservationEndDateException()
    {
        ReservationId id = new(Guid.NewGuid());
        string ownerId = "ownerId";
        DateTime startDate = new(2021, 1, 2, 0, 0, 0);
        DateTime endDate = new(2021, 1, 1, 0, 0, 0);

        Assert.Throws<ReservationEndDateException>(() => new Reservation(id, ownerId, startDate, endDate));
    }

    [Fact]
    public void EndDate_SameDayAsStartDate_ThrowsReservationEndDateException()
    {
        ReservationId id = new(Guid.NewGuid());
        string ownerId = "ownerId";
        DateTime startDate = new(2021, 1, 1, 0, 0, 0);
        DateTime endDate = new(2021, 1, 1, 1, 0, 0);

        Assert.Throws<ReservationEndDateException>(() => new Reservation(id, ownerId, startDate, endDate));
    }

    [Fact]
    public void EndDate_DayAfterStartDate_CreatesReservation()
    {
        ReservationId id = new(Guid.NewGuid());
        string ownerId = "ownerId";
        DateTime startDate = new(2021, 1, 1, 0, 0, 0);
        DateTime endDate = new(2021, 1, 2, 0, 0, 0);

        Reservation reservation = new(id, ownerId, startDate, endDate);

        Assert.Equal(endDate, reservation.EndDate);
    }

    [Fact]
    public void DurationGreaterThanMaxDuration_ThrowsReservationDurationException()
    {
        ReservationId id = new(Guid.NewGuid());
        string ownerId = "ownerId";
        DateTime startDate = new(2021, 1, 1, 0, 0, 0);
        DateTime endDate = new(2021, 2, 1, 0, 0, 0);

        Assert.Throws<ReservationDurationException>(() => new Reservation(id, ownerId, startDate, endDate));
    }

    [Fact]
    public void DurationLessThanMaxDuration_CreatesReservation()
    {
        ReservationId id = new(Guid.NewGuid());
        string ownerId = "ownerId";
        DateTime startDate = new(2021, 1, 1, 0, 0, 0);
        DateTime endDate = new(2021, 1, 31, 0, 0, 0);

        Reservation reservation = new(id, ownerId, startDate, endDate);

        Assert.Equal(endDate, reservation.EndDate);
    }
}