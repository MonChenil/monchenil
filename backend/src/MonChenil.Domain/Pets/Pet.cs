namespace MonChenil.Domain.Pets;

public abstract class Pet
{
    public string Name { get; }
    public PetType Type { get; }
    public string OwnerId { get; }

    public Pet(string name, PetType type, string ownerId)
    {
        Name = name;
        Type = type;
        OwnerId = ownerId;
    }
}

