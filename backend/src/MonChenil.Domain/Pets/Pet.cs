using MonChenil.Domain.Users;

namespace MonChenil.Domain.Pets;

public abstract class Pet
{
    protected string Name;
    protected PetType Type;
    protected User Owner;

    public Pet(string name, User owner)
    {
        Name = name;
        Owner = owner;
    }
}

