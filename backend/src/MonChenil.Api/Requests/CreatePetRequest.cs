using MonChenil.Domain.Pets;

namespace MonChenil.Api.Requests;

public record CreatePetRequest(PetId Id, string Name, PetType Type);