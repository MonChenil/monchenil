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
        return _dbContext.Pets.Select(pet => PetsFactory.CreatePet(pet.Id, pet.Name, pet.Type, pet.OwnerId));
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