namespace MonChenil.Domain.Pets;

public interface IPetsRepository
{
    IEnumerable<Pet> GetPets();
    IEnumerable<Pet> GetPetsByIds(IEnumerable<PetId> petIds);
    void AddPet(Pet pet);
    void DeletePet(Pet pet);
}