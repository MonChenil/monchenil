using Microsoft.AspNetCore.Identity;
using MonChenil.Entities.Pets;

namespace MonChenil.Entities;

public class User : IdentityUser
{
    public List<Pet> Pets { get; set; } = [];
}