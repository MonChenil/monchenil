using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonChenil.Api.Requests;
using MonChenil.Domain.Pets;
using MonChenil.Domain.Reservations;

namespace MonChenil.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ReservationsController : ControllerBase
{
    private readonly IReservationsRepository reservationsRepository;
    private readonly IPetsRepository petsRepository;

    public ReservationsController(IReservationsRepository reservationsRepository, IPetsRepository petsRepository)
    {
        this.reservationsRepository = reservationsRepository;
        this.petsRepository = petsRepository;
    }

    private string GetCurrentUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }

    [HttpGet]
    public IActionResult GetCurrentUserReservations()
    {
        string ownerId = GetCurrentUserId();
        return Ok(reservationsRepository.GetReservations().Where(reservation => reservation.OwnerId == ownerId));
    }

    [HttpPost]
    public IActionResult CreateReservation(CreateReservationRequest request)
    {
        string ownerId = GetCurrentUserId();
        var reservationId = new ReservationId(Guid.NewGuid());
        var reservation = new Reservation(reservationId, ownerId, request.StartDate, request.EndDate);
        var pets = petsRepository.GetPetsByIds(request.PetIds);
        reservation.AddPets(pets);

        reservationsRepository.AddReservation(reservation);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteReservation(Guid id)
    {
        string ownerId = GetCurrentUserId();
        ReservationId reservationId = new ReservationId(id);
        var reservation = reservationsRepository.GetReservations().FirstOrDefault(reservation => reservation.Id == reservationId && reservation.OwnerId == ownerId);
        if (reservation == null)
        {
            return NotFound();
        }

        reservationsRepository.DeleteReservation(reservation);
        return Ok();
    }
}