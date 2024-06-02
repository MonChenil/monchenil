using MonChenil.Domain.Reservations;

namespace MonChenil.Domain.Tests.Reservations;

public class FakeReservationsRepository : IReservationsRepository
{
    private readonly List<Reservation> _reservations = [];

    public FakeReservationsRepository()
    {
    }

    public FakeReservationsRepository(List<Reservation> reservations)
    {
        _reservations = reservations;
    }

    public void AddReservation(Reservation reservation)
    {
        _reservations.Add(reservation);
    }

    public void DeleteReservation(Reservation reservation)
    {
        _reservations.Remove(reservation);
    }

    public Reservation? GetReservationById(ReservationId reservationId)
    {
        return _reservations.FirstOrDefault(r => r.Id == reservationId);
    }

    public IEnumerable<Reservation> GetReservations()
    {
        return _reservations;
    }

    public IEnumerable<Reservation> GetOverlappingReservations(DateTime startDate, DateTime endDate)
    {
        return _reservations.Where(r => r.Overlaps(startDate, endDate));
    }

    public IEnumerable<Reservation> GetReservationsByOwnerId(string ownerId)
    {
        return _reservations.Where(r => r.OwnerId == ownerId);
    }
}