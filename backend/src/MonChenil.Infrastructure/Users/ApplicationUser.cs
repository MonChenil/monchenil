using Microsoft.AspNetCore.Identity;
using MonChenil.Domain.Pets;

namespace MonChenil.Infrastructure.Users;

public class ApplicationUser : IdentityUser
{
    public List<Pet> Pets { get; set; } = [];
}