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
        Id = id;
        OwnerId = ownerId;
        if (DateTime.Now.Subtract(startDate).TotalHours < 1)
        {
            throw new ReservationStartDateException();
        } else StartDate = startDate;
        if(startDate.AddDays(1).CompareTo(endDate)>0) {
            throw new ArgumentException("Au moins un jour");
        } else EndDate = endDate;
    }

    public void AddPets(IEnumerable<Pet> pets)
    {
        Pets.AddRange(pets);
    }
}