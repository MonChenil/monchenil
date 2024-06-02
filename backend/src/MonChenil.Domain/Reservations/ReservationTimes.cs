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

    public List<DateTime> GetArrivalTimes(DateTime startDate, DateTime endDate, List<Pet> pets)
    {
        List<DateTime> times = [];
        var reservations = reservationsRepository.GetOverlappingReservations(startDate, endDate);
        var currentTime = GetFirstTime(startDate);

        while (currentTime < endDate)
        {
            if (MaxCapacityReached(currentTime, pets))
            {
                currentTime = GetNextTime(currentTime);
                continue;
            }

            if (!IsOpenAt(currentTime) || AnyReservationAtTime(currentTime, reservations))
            {
                currentTime = GetNextTime(currentTime);
                continue;
            }

            times.Add(currentTime);
            currentTime = GetNextTime(currentTime);
        }

        return times;
    }

    public List<DateTime> GetDepartureTimes(DateTime startDate, DateTime endDate, List<Pet> pets)
    {
        List<DateTime> times = [];
        var reservations = reservationsRepository.GetOverlappingReservations(startDate, endDate);
        var currentTime = GetFirstTime(startDate);

        while (currentTime < endDate)
        {
            if (MaxCapacityReached(currentTime, pets))
            {
                // Cannot end a reservation after the max capacity is reached
                break;
            }

            if (!IsOpenAt(currentTime) || AnyReservationAtTime(currentTime, reservations))
            {
                currentTime = GetNextTime(currentTime);
                continue;
            }

            times.Add(currentTime);
            currentTime = GetNextTime(currentTime);
        }

        return times;
    }

    private bool MaxCapacityReached(DateTime currentTime, List<Pet> pets)
    {
        var overlappingReservations = reservationsRepository.GetOverlappingReservations(currentTime, currentTime.AddMinutes(INTERVAL_MINUTES));
        int petCount = overlappingReservations.SelectMany(reservation => reservation.Pets).Count();
        petCount += pets.Count;


        return petCount > MAX_CAPACITY;
    }
}