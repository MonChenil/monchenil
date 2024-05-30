namespace MonChenil.Domain.Reservations;

public interface IReservationTimes
{
    List<DateTime> GetArrivalTimes(DateTime startDate, DateTime endDate);
}