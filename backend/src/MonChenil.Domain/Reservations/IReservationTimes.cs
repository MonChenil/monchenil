using MonChenil.Domain.Pets;

namespace MonChenil.Domain.Reservations;

public interface IReservationTimes
{
    List<DateTime> GetArrivalTimes(DateTime startDate, DateTime endDate);
    List<DateTime> GetDepartureTimes(DateTime startDate, DateTime endDate, DateTime arrivalTime, List<Pet> pets);
}