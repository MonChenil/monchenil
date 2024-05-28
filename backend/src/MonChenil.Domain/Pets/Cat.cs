namespace MonChenil.Domain.Pets;

public class Cat : Pet
{
    public Cat(string name, string ownerId) : base(name, PetType.Cat, ownerId)
    {
    }
}