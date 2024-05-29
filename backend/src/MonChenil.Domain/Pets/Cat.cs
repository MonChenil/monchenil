namespace MonChenil.Domain.Pets;

public class Cat : Pet
{
    public Cat(PetId id, string name, string ownerId) : base(id, name, PetType.Cat, ownerId)
    {
    }
}