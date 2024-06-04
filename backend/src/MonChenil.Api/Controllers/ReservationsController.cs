using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonChenil.Api.Requests;
using MonChenil.Domain.Pets;
using MonChenil.Domain.Reservations;

namespace MonChenil.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationsRepository reservationsRepository;
    private readonly IPetsRepository petsRepository;
    private readonly IReservationTimes reservationTimes;

    public ReservationsController(
        IReservationsRepository reservationsRepository,
        IPetsRepository petsRepository,
        IReservationTimes reservationTimes
    )
    {
        this.reservationsRepository = reservationsRepository;
        this.petsRepository = petsRepository;
        this.reservationTimes = reservationTimes;
    }

    private string GetCurrentUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetCurrentUserReservations()
    {
        string currentUserId = GetCurrentUserId();
        return Ok(reservationsRepository.GetReservationsByOwnerId(currentUserId));
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateReservation(CreateReservationRequest request)
    {
        var pets = petsRepository.GetPetsByIds(request.PetIds);
        if (!reservationTimes.AreTimesAvailableForPets(request.StartDate, request.EndDate, pets))
        {
            return new BadRequestObjectResult($"The selected times are not available for the selected pets.");
        }
        try
        {
            string currentUserId = GetCurrentUserId();
            var reservation = new Reservation(new(Guid.NewGuid()), currentUserId, request.StartDate, request.EndDate);
            reservation.AddPets(pets);
            reservationsRepository.AddReservation(reservation);
            return Ok();
        }
        catch (ReservationEndDateException ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
        catch (ReservationDurationException ex)
        {
            return new BadRequestObjectResult(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
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

    [HttpGet("arrival-times")]
    public IActionResult GetArrivalTimes([FromQuery] GetArrivalTimesRequest request)
    {
        var petIds = request.PetIds.Select(PetId.FromString).ToList();
        var pets = petsRepository.GetPetsByIds(petIds);
        List<DateTime> arrivalTimes = reservationTimes.GetArrivalTimes(request.StartDate, request.EndDate, pets);
        return Ok(arrivalTimes);
    }

    [HttpGet("departure-times")]
    public IActionResult GetDepartureTimes([FromQuery] GetDepartureTimesRequest request)
    {
        var petIds = request.PetIds.Select(PetId.FromString).ToList();
        var pets = petsRepository.GetPetsByIds(petIds);
        List<DateTime> departureTimes = reservationTimes.GetDepartureTimes(request.StartDate, request.EndDate, pets);
        return Ok(departureTimes);
    }
}