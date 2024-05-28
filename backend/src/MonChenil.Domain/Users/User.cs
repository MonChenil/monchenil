using Microsoft.AspNetCore.Identity;
using MonChenil.Domain.Pets;

namespace MonChenil.Domain.Users;

public class User : IdentityUser
{
    public List<Pet> Pets { get; set; } = [];
}