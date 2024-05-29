using MonChenil.Domain.Pets;

namespace MonChenil.Api.Requests;

public record CreateReservationRequest(DateTime StartDate, DateTime EndDate, List<PetId> PetIds);