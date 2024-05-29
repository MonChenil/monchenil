namespace MonChenil.Domain.Reservations;

public interface IReservationsRepository
{
    IEnumerable<Reservation> GetReservations();
    void AddReservation(Reservation reservation);
    void DeleteReservation(Reservation reservation);
}