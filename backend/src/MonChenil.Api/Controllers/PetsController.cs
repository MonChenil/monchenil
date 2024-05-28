using Microsoft.AspNetCore.Mvc;
using MonChenil.Infrastructure.Entities;
using MonChenil.Infrastructure.Pets;
using MonChenil.Infrastructure.Repositories;

namespace MonChenil.Controllers;

[ApiController]
[Route("[controller]")]
public class PetsController : ControllerBase
{
    private readonly IRepository<PetEntity> repository;

    public PetsController(IRepository<PetEntity> repository)
    {
        this.repository = repository;
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
    public IActionResult Post(PetEntity pet)
    {
        repository.Add(pet);
        return CreatedAtAction(nameof(Get), new { id = pet.Id }, pet);
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