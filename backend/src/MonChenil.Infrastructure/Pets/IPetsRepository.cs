using MonChenil.Domain.Pets;

namespace MonChenil.Infrastructure.Pets;

public interface IPetsRepository
{
    void AddPet(Pet pet);
}