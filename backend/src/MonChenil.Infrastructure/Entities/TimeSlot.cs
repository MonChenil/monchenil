using MonChenil.Domain.Pets;

namespace MonChenil.Infrastructure.Entities;

public class TimeSlot : IEntity
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Pet> Pets { get; set; } = [];
}
