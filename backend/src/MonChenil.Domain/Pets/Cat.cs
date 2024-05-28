using MonChenil.Domain.Users;

namespace MonChenil.Domain.Pets;

public class Cat : Pet
{
    public Cat(string name, User owner) : base(name, owner)
    {
        Type = PetType.Cat;
    }
}