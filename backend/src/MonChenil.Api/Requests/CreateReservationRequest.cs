using MonChenil.Domain.Pets;
using MonChenil.Domain.Reservations;

namespace MonChenil.Api.Requests;

public record CreateReservationRequest(DateTime StartDate, DateTime EndDate, List<PetId> PetIds)
{
    public DateTime StartDate { get; init; } = StartDate < DateTime.Now.AddHours(1) ? throw new ReservationStartDateException() : StartDate;
}