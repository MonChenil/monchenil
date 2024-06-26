﻿namespace MonChenil.Domain.Pets;

public class PetsFactory
{
    public static Pet CreatePet(PetId id, string name, PetType type, string ownerId)
    {
        return type switch
        {
            PetType.Cat => new Cat(id, name, ownerId),
            PetType.Dog => new Dog(id, name, ownerId),
            _ => throw new ArgumentException("Invalid pet type")
        };
    }
}