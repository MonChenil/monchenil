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