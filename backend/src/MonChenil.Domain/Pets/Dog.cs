namespace MonChenil.Domain.Pets;

public class Dog : Pet
{
    public Dog(PetId id, string name, string ownerId) : base(id, name, PetType.Dog, ownerId)
    {
    }
}