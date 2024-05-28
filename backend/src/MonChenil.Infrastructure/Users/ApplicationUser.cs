using Microsoft.AspNetCore.Identity;
using MonChenil.Infrastructure.Pets;

namespace MonChenil.Infrastructure.Users;

public class ApplicationUser : IdentityUser
{
    public List<PetEntity> Pets { get; set; } = [];
}