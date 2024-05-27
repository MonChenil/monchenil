using Microsoft.AspNetCore.Identity;

namespace MonChenil.Infrastructure.Entities;

public class User : IdentityUser
{
    public List<Pet> Pets { get; set; } = [];
}