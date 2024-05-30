namespace MonChenil.Domain.Pets;

public class PetIdException : Exception
{
    public PetIdException() : base($"PetId must be a 15-digit string")
    {
    }
}