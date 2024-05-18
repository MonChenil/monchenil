using Microsoft.AspNetCore.Mvc;
using MonChenil.Data;
using MonChenil.Entities;

namespace MonChenil.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeSlotsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TimeSlotsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_context.TimeSlots.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var timeSlot = _context.TimeSlots.Find(id);
        if (timeSlot == null)
        {
            return NotFound();
        }

        return Ok(timeSlot);
    }

    [HttpPost]
    public IActionResult Post(TimeSlot timeSlot)
    {
        _context.TimeSlots.Add(timeSlot);
        _context.SaveChanges();
        return CreatedAtAction(nameof(Get), new { id = timeSlot.Id }, timeSlot);
    }

    [HttpPut()]
    public IActionResult Put(TimeSlot timeSlot)
    {
        if (!_context.TimeSlots.Any(p => p.Id == timeSlot.Id))
        {
            return NotFound();
        }

        _context.TimeSlots.Update(timeSlot);
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var timeSlot = _context.TimeSlots.Find(id);
        if (timeSlot == null)
        {
            return NotFound();
        }

        _context.TimeSlots.Remove(timeSlot);
        _context.SaveChanges();
        return Ok();
    }
}