using MonChenil.Domain.Pets;

namespace MonChenil.Domain.Reservations;

public interface IReservationTimes
{
    List<DateTime> GetArrivalTimes(DateTime startDate, DateTime endDate, IEnumerable<Pet> pets);
    List<DateTime> GetDepartureTimes(DateTime startDate, DateTime endDate, IEnumerable<Pet> pets);
    bool AreTimesAvailableForPets(DateTime startDate, DateTime endDate, IEnumerable<Pet> pets);
}