using MonChenil.Domain.Pets;

namespace MonChenil.Domain.Reservations;

public class ReservationTimes : IReservationTimes
{
    private readonly IReservationsRepository reservationsRepository;
    private const int MAX_CAPACITY = 3;
    private const int INTERVAL_MINUTES = 30;
    private static readonly TimeSpan OPENING_HOUR = new(9, 0, 0);
    private static readonly TimeSpan CLOSING_HOUR = new(18, 0, 0);

    public ReservationTimes(IReservationsRepository reservationsRepository)
    {
        this.reservationsRepository = reservationsRepository;
    }

    public List<DateTime> GetArrivalTimes(DateTime startDate, DateTime endDate, IEnumerable<Pet> pets)
    {
        return GetTimes(startDate, endDate, pets, false);
    }

    public List<DateTime> GetDepartureTimes(DateTime startDate, DateTime endDate, IEnumerable<Pet> pets)
    {
        return GetTimes(startDate, endDate, pets, true);
    }

    private bool ArePetsAvailable(DateTime startDate, DateTime endDate, IEnumerable<Pet> pets)
    {
        var reservations = reservationsRepository.GetOverlappingReservations(startDate, endDate);
        return !reservations.Any(reservation => reservation.Pets.Any(pets.Contains));
    }

    private List<DateTime> GetTimes(DateTime startDate, DateTime endDate, IEnumerable<Pet> pets, bool breakOnMaxCapacity)
    {
        List<DateTime> times = [];
        var reservations = reservationsRepository.GetOverlappingReservations(startDate, endDate);

        for (var currentTime = GetFirstTime(startDate); currentTime < endDate; currentTime = GetNextTime(currentTime))
        {
            if (MaxCapacityReached(currentTime, pets) && breakOnMaxCapacity)
            {
                break;
            }

            if (!IsTimeAvailable(pets, reservations, currentTime))
            {
                continue;
            }

            times.Add(currentTime);
        }

        return times;
    }

    private bool IsTimeAvailable(IEnumerable<Pet> pets, IEnumerable<Reservation> reservations, DateTime currentTime)
    {
        return !MaxCapacityReached(currentTime, pets) 
            && IsOpenAt(currentTime) 
            && !AnyReservationAtTime(currentTime, reservations) 
            && ArePetsAvailable(currentTime, currentTime.AddMinutes(INTERVAL_MINUTES), pets);
    }

    private static DateTime GetTimeWithoutSeconds(DateTime time)
    {
        return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
    }

    private static DateTime GetNextTime(DateTime currentTime)
    {
        return currentTime.AddMinutes(INTERVAL_MINUTES - (currentTime.Minute % INTERVAL_MINUTES));
    }

    private static DateTime GetFirstTime(DateTime time)
    {
        time = GetTimeWithoutSeconds(time);
        if (time.Minute % INTERVAL_MINUTES == 0)
        {
            return time;
        }

        return GetNextTime(time);
    }

    private static bool IsTimeBetween(DateTime time, TimeSpan start, TimeSpan end)
    {
        return time.TimeOfDay >= start && time.TimeOfDay <= end;
    }

    private static bool IsOpenAt(DateTime time)
    {
        TimeSpan lastTime = CLOSING_HOUR.Subtract(new(0, INTERVAL_MINUTES, 0));
        return IsTimeBetween(time, OPENING_HOUR, lastTime);
    }

    private static bool AnyReservationAtTime(DateTime time, IEnumerable<Reservation> reservations)
    {
        return reservations.Any(reservation => reservation.StartDate == time || reservation.EndDate == time);
    }

    private bool MaxCapacityReached(DateTime currentTime, IEnumerable<Pet> pets)
    {
        var overlappingReservations = reservationsRepository.GetOverlappingReservations(currentTime, currentTime.AddMinutes(INTERVAL_MINUTES));
        int petCount = overlappingReservations.SelectMany(reservation => reservation.Pets).Count();
        petCount += pets.Count();

        return petCount > MAX_CAPACITY;
    }
}