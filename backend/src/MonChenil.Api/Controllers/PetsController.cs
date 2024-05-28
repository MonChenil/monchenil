using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonChenil.Domain.Pets;
using MonChenil.Infrastructure.Entities;
using MonChenil.Infrastructure.Pets;
using MonChenil.Infrastructure.Repositories;

namespace MonChenil.Controllers;

[ApiController]
[Route("[controller]")]
public class PetsController : ControllerBase
{
    private readonly IRepository<PetEntity> repository;
    private readonly IPetsRepository petsRepository;

    public PetsController(IRepository<PetEntity> repository, IPetsRepository petsRepository)
    {
        this.repository = repository;
        this.petsRepository = petsRepository;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(repository.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var pet = repository.GetById(id);
        if (pet == null)
        {
            return NotFound();
        }

        return Ok(pet);
    }

    [HttpPost]
    [Authorize]
    public IActionResult Post(PetDto petDto)
    {
        string? ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(ownerId))
        {
            return Unauthorized();
        }

        var pet = PetsFactory.CreatePet(petDto.Name, petDto.Type, ownerId);
        if (pet == null)
        {
            return BadRequest();
        }

        petsRepository.AddPet(pet);
        return Ok();
    }


    [HttpPut()]
    public IActionResult Put(PetEntity pet)
    {
        if (!repository.Exists(p => p.Id == pet.Id))
        {
            return NotFound();
        }

        repository.Update(pet);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pet = repository.GetById(id);
        if (pet == null)
        {
            return NotFound();
        }

        repository.Delete(pet);
        return Ok();
    }
}