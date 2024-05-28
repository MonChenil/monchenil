using MonChenil.Domain.Pets;
using MonChenil.Infrastructure.Entities;
using MonChenil.Infrastructure.Users;

namespace MonChenil.Infrastructure.Pets;

public class PetEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PetType Type { get; set; }
    public string OwnerId { get; set; } = string.Empty;
    public List<TimeSlot> TimeSlots { get; set; } = [];
    public List<PetType> IncompatibleTypes { get; set; } = [];
}
