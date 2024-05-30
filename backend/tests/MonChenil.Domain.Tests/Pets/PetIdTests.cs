using MonChenil.Domain.Pets;

namespace MonChenil.Domain.Tests.Pets;

public class PetIdTests
{
    [Theory]
    [InlineData("")]
    [InlineData("28738372423705")]
    [InlineData("1234567890123456")]
    public void PetId_NotA15DigitString_ThrowsPetIdException(string value)
    {
        Assert.Throws<PetIdException>(() => new PetId(value));
    }

    [Theory]
    [InlineData("287383724237054")]
    [InlineData("480531850353143")]
    [InlineData("358488615963723")]    
    public void PetId_A15DigitString_CreatesPetId(string value)
    {
        var petId = new PetId(value);
        Assert.Equal(value, petId.Value);
    }
}