namespace MonChenil.Domain.Reservations;

public class ReservationTimes : IReservationTimes
{
    private readonly IReservationsRepository reservationsRepository;
    private const int INTERVAL_MINUTES = 30;
    private readonly TimeSpan OPENING_HOUR = new(9, 0, 0);
    private readonly TimeSpan CLOSING_HOUR = new(18, 0, 0);

    public ReservationTimes(IReservationsRepository reservationsRepository)
    {
        this.reservationsRepository = reservationsRepository;
    }

    public List<DateTime> GetArrivalTimes(DateTime startDate, DateTime endDate)
    {
        var reservations = reservationsRepository.GetOverlappingReservations(startDate, endDate);

        var times = new List<DateTime>();

        var currentTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, 0);
        if (currentTime.Minute % INTERVAL_MINUTES != 0)
        {
            currentTime = currentTime.AddMinutes(INTERVAL_MINUTES - (currentTime.Minute % INTERVAL_MINUTES));
        }

        while (currentTime < endDate)
        {
            if (reservations.Any(reservation => reservation.StartDate == currentTime || reservation.EndDate == currentTime))
            {
                currentTime = currentTime.AddMinutes(INTERVAL_MINUTES);
                continue;
            }

            if (currentTime.TimeOfDay < OPENING_HOUR || currentTime.TimeOfDay > CLOSING_HOUR.Subtract(new(0, INTERVAL_MINUTES, 0)))
            {
                currentTime = currentTime.AddMinutes(INTERVAL_MINUTES);
                continue;
            }

            times.Add(currentTime);
            currentTime = currentTime.AddMinutes(INTERVAL_MINUTES);
        }

        return times;
    }
}