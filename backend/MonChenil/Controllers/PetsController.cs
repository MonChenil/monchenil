using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonChenil.Data;
using MonChenil.Entities;

namespace MonChenil.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PetsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.Pets.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var pet = _context.Pets.Find(id);
        if (pet == null)
        {
            return NotFound();
        }

        return Ok(pet);
    }

    [HttpPost]
    public IActionResult Post(Pet pet)
    {
        _context.Pets.Add(pet);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = pet.Id }, pet);
    }

    [HttpPut()]
    public IActionResult Put(Pet pet)
    {
        if (!_context.Pets.Any(p => p.Id == pet.Id))
        {
            return NotFound();
        }

        _context.Pets.Update(pet);
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var pet = _context.Pets.Find(id);
        if (pet == null)
        {
            return NotFound();
        }

        _context.Pets.Remove(pet);
        _context.SaveChanges();
        return Ok();
    }
}