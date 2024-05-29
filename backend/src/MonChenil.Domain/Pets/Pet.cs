namespace MonChenil.Domain.Pets;

public abstract class Pet
{
    public PetId Id { get; }
    public string Name { get; }
    public PetType Type { get; }
    public string OwnerId { get; }

    public Pet(PetId id, string name, PetType type, string ownerId)
    {
        Id = id;
        Name = name;
        Type = type;
        OwnerId = ownerId;
    }
}

