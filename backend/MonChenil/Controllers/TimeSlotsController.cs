using Microsoft.AspNetCore.Mvc;
using MonChenil.Entities;
using MonChenil.Repositories;

namespace MonChenil.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeSlotsController : ControllerBase
{
    private readonly IRepository<TimeSlot> repository;

    public TimeSlotsController(IRepository<TimeSlot> repository)
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
        var timeSlot = repository.GetById(id);
        if (timeSlot == null)
        {
            return NotFound();
        }

        return Ok(timeSlot);
    }

    [HttpPost]
    public IActionResult Post(TimeSlot timeSlot)
    {
        repository.Add(timeSlot);
        return CreatedAtAction(nameof(Get), new { id = timeSlot.Id }, timeSlot);
    }

    [HttpPut()]
    public IActionResult Put(TimeSlot timeSlot)
    {
        if (!repository.Exists(t => t.Id == timeSlot.Id))
        {
            return NotFound();
        }

        repository.Update(timeSlot);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var timeSlot = repository.GetById(id);
        if (timeSlot == null)
        {
            return NotFound();
        }

        repository.Delete(timeSlot);
        return Ok();
    }
}