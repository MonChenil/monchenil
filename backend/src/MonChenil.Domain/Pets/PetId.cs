using System.Text.RegularExpressions;

namespace MonChenil.Domain.Pets;

public record PetId
{
    private static Regex PetIdRegex() => new(@"^\d{15}$");
    public string Value { get; }

    public PetId(string value)
    {
        if (!PetIdRegex().IsMatch(value))
        {
            throw new PetIdException();
        }

        Value = value;
    }

    public static PetId FromString(string value) => new(value);
}