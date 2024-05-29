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
        string ownerId = GetCurrentUserId();
        return Ok(petsRepository.GetPets().Where(pet => pet.OwnerId == ownerId));
    }

    [HttpPost]
    public IActionResult CreatePet(CreatePetRequest request)
    {
        string ownerId = GetCurrentUserId();
        var pet = PetsFactory.CreatePet(request.Id, request.Name, request.Type, ownerId);
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
        string ownerId = GetCurrentUserId();
        PetId petId = new PetId(id);
        var pet = petsRepository.GetPets().FirstOrDefault(pet => pet.Id == petId && pet.OwnerId == ownerId);
        if (pet == null)
        {
            return NotFound();
        }

        petsRepository.DeletePet(pet);
        return Ok();
    }
}