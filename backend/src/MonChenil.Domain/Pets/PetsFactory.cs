namespace MonChenil.Domain.Pets;

public class PetsFactory
{
    public static Pet CreatePet(string name, PetType type, string ownerId)
    {
        return type switch
        {
            PetType.Dog => new Dog(name, ownerId),
            PetType.Cat => new Cat(name, ownerId),
            _ => throw new ArgumentException("Invalid pet type")
        };
    }
}