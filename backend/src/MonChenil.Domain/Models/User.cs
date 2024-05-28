using Microsoft.AspNetCore.Identity;

namespace MonChenil.Domain.Models;

public class User : IdentityUser
{
    public List<Pet> Pets { get; set; } = [];
}