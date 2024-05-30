namespace MonChenil.Domain.Reservations;

public class ReservationTimes : IReservationTimes
{
    private readonly IReservationsRepository reservationsRepository;
    private const int INTERVAL = 30;

    public ReservationTimes(IReservationsRepository reservationsRepository)
    {
        this.reservationsRepository = reservationsRepository;
    }

    public List<DateTime> GetArrivalTimes(DateTime startDate, DateTime endDate)
    {
        var reservations = reservationsRepository.GetOverlappingReservations(startDate, endDate);

        var times = new List<DateTime>();

        var currentTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startDate.Hour, startDate.Minute, 0);
        if (currentTime.Minute % INTERVAL != 0)
        {
            currentTime = currentTime.AddMinutes(INTERVAL - (currentTime.Minute % INTERVAL));
        }

        while (currentTime < endDate)
        {
            if (reservations.All(reservation => reservation.StartDate != currentTime && reservation.EndDate != currentTime))
            {
                times.Add(currentTime);
            }

            currentTime = currentTime.AddMinutes(INTERVAL);
        }

        return times;
    }
}