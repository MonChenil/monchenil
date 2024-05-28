using Microsoft.AspNetCore.Identity;
using MonChenil.Data;
using MonChenil.Domain.Pets;
using MonChenil.Infrastructure.Users;

namespace MonChenil.Infrastructure.Pets;

public class PetsRepository : IPetsRepository
{
    private readonly ApplicationDbContext _dbContext;
    public PetsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddPet(Pet pet)
    {
        var petEntity = new PetEntity
        {
            Name = pet.Name,
            Type = pet.Type,
            OwnerId = pet.OwnerId
        };

        _dbContext.Pets.Add(petEntity);
        _dbContext.SaveChanges();
    }
}