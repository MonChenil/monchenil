namespace MonChenil.Entities.Pets;

public enum PetType
{
    Cat,
    Dog,
}

public abstract class Pet
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public abstract PetType Type { get; set; }
    public User? Owner { get; set; }
}
