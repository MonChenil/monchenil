using MonChenil.Domain.Pets;

namespace MonChenil.Infrastructure.Pets;

public interface IPetsRepository
{
    IEnumerable<Pet> GetPets();
    void AddPet(Pet pet);
    void DeletePet(Pet pet);
}