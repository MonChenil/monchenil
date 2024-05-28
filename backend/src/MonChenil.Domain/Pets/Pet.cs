using MonChenil.Domain.Users;

namespace MonChenil.Domain.Pets;

public abstract class Pet
{
    protected string Name;
    protected PetType Type;
    protected IApplicationUser Owner;

    public Pet(string name, IApplicationUser owner)
    {
        Name = name;
        Owner = owner;
    }
}

