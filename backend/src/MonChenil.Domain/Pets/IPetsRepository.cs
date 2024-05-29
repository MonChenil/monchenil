namespace MonChenil.Domain.Pets;

public interface IPetsRepository
{
    IEnumerable<Pet> GetPets();
    IEnumerable<Pet> GetPetsByIds(IEnumerable<PetId> petIds);
    IEnumerable<Pet> GetPetsByOwnerId(string ownerId);
    Pet? GetPetById(PetId petId);
    void AddPet(Pet pet);
    void DeletePet(Pet pet);
}