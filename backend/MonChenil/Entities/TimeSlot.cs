namespace MonChenil.Entities;

public class TimeSlot
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Pet> Pets { get; set; } = [];
}
