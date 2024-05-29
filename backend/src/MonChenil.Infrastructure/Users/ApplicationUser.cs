using Microsoft.AspNetCore.Identity;
using MonChenil.Domain.Pets;
using MonChenil.Domain.Reservations;

namespace MonChenil.Infrastructure.Users;

public class ApplicationUser : IdentityUser
{
    public List<Pet> Pets { get; set; } = [];
    public List<Reservation> Reservations { get; set; } = [];
}