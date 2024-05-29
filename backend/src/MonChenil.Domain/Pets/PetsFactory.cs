namespace MonChenil.Domain.Pets;

public class PetsFactory
{
    public static Pet CreatePet(PetDto petDto, string ownerId)
    {
        return petDto.Type switch
        {
            PetType.Cat => new Cat(petDto.Id, petDto.Name, ownerId),
            PetType.Dog => new Dog(petDto.Id, petDto.Name, ownerId),
            _ => throw new ArgumentException("Invalid pet type")
        };
    }
}