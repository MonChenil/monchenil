using MonChenil.Domain.Pets;

namespace MonChenil.Domain.Reservations;

public class Reservation
{
    public const int MaxDurationInDays = 30;
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

        if ((endDate - startDate).Days > MaxDurationInDays)
        {
            throw new ReservationDurationException();
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

    public bool Overlaps(DateTime startDate, DateTime endDate)
    {
        return StartDate < endDate && EndDate >= startDate;
    }
}