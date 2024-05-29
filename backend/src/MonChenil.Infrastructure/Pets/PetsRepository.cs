using MonChenil.Data;
using MonChenil.Domain.Pets;

namespace MonChenil.Infrastructure.Pets;

public class PetsRepository : IPetsRepository
{
    private readonly ApplicationDbContext _dbContext;
    public PetsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Pet> GetPets()
    {
        return _dbContext.Pets;
    }

    public IEnumerable<Pet> GetPetsByIds(IEnumerable<PetId> petIds)
    {
        return _dbContext.Pets.Where(pet => petIds.Contains(pet.Id));
    }

    public IEnumerable<Pet> GetPetsByOwnerId(string ownerId)
    {
        return _dbContext.Pets.Where(pet => pet.OwnerId == ownerId);
    }

    public Pet? GetPetById(PetId petId)
    {
        return _dbContext.Pets.FirstOrDefault(pet => pet.Id == petId);
    }

    public void AddPet(Pet pet)
    {
        _dbContext.Pets.Add(pet);
        _dbContext.SaveChanges();
    }

    public void DeletePet(Pet pet)
    {
        _dbContext.Pets.Remove(pet);
        _dbContext.SaveChanges();
    }
}