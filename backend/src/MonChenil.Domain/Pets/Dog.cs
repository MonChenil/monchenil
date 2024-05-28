using MonChenil.Domain.Users;

namespace MonChenil.Domain.Pets;

public class Dog : Pet
{
    public Dog(string name, User owner) : base(name, owner)
    {
        Type = PetType.Dog;
    }
}