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
        string currentUserId = GetCurrentUserId();
        return Ok(reservationsRepository.GetReservationsByOwnerId(currentUserId));
    }

    [HttpPost]
    public IActionResult CreateReservation(CreateReservationRequest request)
    {
        string currentUserId = GetCurrentUserId();
        var reservationId = new ReservationId(Guid.NewGuid());
        Reservation reservation;
        try
        {
            reservation = new Reservation(reservationId, currentUserId, request.StartDate, request.EndDate);
        }
        catch (ReservationEndDateException ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }

        var pets = petsRepository.GetPetsByIds(request.PetIds);
        reservation.AddPets(pets);

        reservationsRepository.AddReservation(reservation);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteReservation(Guid id)
    {
        string currentUserId = GetCurrentUserId();
        ReservationId reservationId = new(id);
        var reservation = reservationsRepository.GetReservationById(reservationId);
        if (reservation == null)
        {
            return NotFound();
        }

        if (reservation.OwnerId != currentUserId)
        {
            return Forbid();
        }

        reservationsRepository.DeleteReservation(reservation);
        return Ok();
    }
}