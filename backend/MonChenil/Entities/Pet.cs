namespace MonChenil.Entities;

public enum PetType
{
    Cat,
    Dog,
}

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public PetType Type { get; set; }
    public User? Owner { get; set; }
    public List<TimeSlot> TimeSlots { get; set; } = [];
}
