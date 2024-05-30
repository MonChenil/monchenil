using Microsoft.EntityFrameworkCore;
using MonChenil.Data;
using MonChenil.Domain.Reservations;

namespace MonChenil.Infrastructure.Reservations;

public class ReservationsRepository : IReservationsRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ReservationsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Reservation> GetReservations()
    {
        return _dbContext.Reservations.Include(reservation => reservation.Pets);
    }

    public IEnumerable<Reservation> GetOverlappingReservations(DateTime startDate, DateTime endDate)
    {
        return _dbContext.Reservations
            .Include(reservation => reservation.Pets)
            .AsEnumerable()
            .Where(reservation => reservation.Overlaps(startDate, endDate));
    }

    public IEnumerable<Reservation> GetReservationsByOwnerId(string ownerId)
    {
        return _dbContext.Reservations
            .Where(reservation => reservation.OwnerId == ownerId)
            .Include(reservation => reservation.Pets);
    }

    public Reservation? GetReservationById(ReservationId reservationId)
    {
        return _dbContext.Reservations
            .Include(reservation => reservation.Pets)
            .FirstOrDefault(reservation => reservation.Id == reservationId);
    }

    public void AddReservation(Reservation reservation)
    {
        _dbContext.Reservations.Add(reservation);
        _dbContext.SaveChanges();
    }

    public void DeleteReservation(Reservation reservation)
    {
        _dbContext.Reservations.Remove(reservation);
        _dbContext.SaveChanges();
    }
}