using Microsoft.AspNetCore.Mvc;
using MonChenil.Infrastructure.Entities;
using MonChenil.Infrastructure.Repositories;

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
        try
        {
            repository.Add(timeSlot);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }

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

    [HttpGet("available")]
    public IActionResult GetAvailableTimeSlots([FromQuery] List<int> petIds)
    {
        throw new NotImplementedException();
        //var availableTimeSlots = repository.GetAll().Where(timeslot=>!timeslot.IsFull());
        //return Ok(availableTimeSlots);
    }
}