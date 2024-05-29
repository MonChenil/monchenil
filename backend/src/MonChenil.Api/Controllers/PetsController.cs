using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonChenil.Api.Requests;
using MonChenil.Domain.Pets;

namespace MonChenil.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PetsController : ControllerBase
{
    private readonly IPetsRepository petsRepository;

    public PetsController(IPetsRepository petsRepository)
    {
        this.petsRepository = petsRepository;
    }

    private string GetCurrentUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }

    [HttpGet]
    public IActionResult GetCurrentUserPets()
    {
        string currentUserId = GetCurrentUserId();
        return Ok(petsRepository.GetPetsByOwnerId(currentUserId));
    }

    [HttpPost]
    public IActionResult CreatePet(CreatePetRequest request)
    {
        string currentUserId = GetCurrentUserId();
        var pet = PetsFactory.CreatePet(request.Id, request.Name, request.Type, currentUserId);
        if (pet == null)
        {
            return BadRequest();
        }

        petsRepository.AddPet(pet);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePet(string id)
    {
        string currentUserId = GetCurrentUserId();
        PetId petId = new PetId(id);
        var pet = petsRepository.GetPetById(petId);
        if (pet == null)
        {
            return NotFound();
        }

        if (pet.OwnerId != currentUserId)
        {
            return Forbid();
        }

        petsRepository.DeletePet(pet);
        return Ok();
    }
}