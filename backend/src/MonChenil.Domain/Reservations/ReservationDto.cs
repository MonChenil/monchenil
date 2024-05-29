using MonChenil.Domain.Pets;

namespace MonChenil.Domain.Reservations;

public record ReservationDto(DateTime StartDate, DateTime EndDate, List<PetId> PetIds);