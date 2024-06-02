using MonChenil.Domain.Pets;

namespace MonChenil.Domain.Reservations;

public class Reservation
{
    public ReservationId Id { get; }
    public string OwnerId { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public List<Pet> Pets { get; } = [];

    public Reservation(ReservationId id, string ownerId, DateTime startDate, DateTime endDate)
    {
        if (endDate.Date <= startDate.Date)
        {
            throw new ReservationEndDateException();
        }

        Id = id;
        OwnerId = ownerId;
        StartDate = startDate;
        EndDate = endDate;
    }

    public void AddPets(IEnumerable<Pet> pets)
    {
        Pets.AddRange(pets);
    }
}