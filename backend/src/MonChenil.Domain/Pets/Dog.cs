namespace MonChenil.Domain.Pets;

public class Dog : Pet
{
    public Dog(string name, string ownerId) : base(name, PetType.Dog, ownerId)
    {
    }
}