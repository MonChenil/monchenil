namespace MonChenil.Domain.Reservations;

public interface IReservationsRepository
{
    IEnumerable<Reservation> GetReservations();
    IEnumerable<Reservation> GetReservationsByOwnerId(string ownerId);
    Reservation? GetReservationById(ReservationId reservationId);
    void AddReservation(Reservation reservation);
    void DeleteReservation(Reservation reservation);
}