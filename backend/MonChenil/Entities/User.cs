using Microsoft.AspNetCore.Identity;

namespace MonChenil.Entities;

public class User : IdentityUser
{
    public List<Pet> Pets { get; set; } = [];
}