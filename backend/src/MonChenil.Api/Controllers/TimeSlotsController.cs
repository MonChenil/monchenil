using Microsoft.AspNetCore.Mvc;
using MonChenil.Infrastructure.Entities;
using MonChenil.Domain.Services;

namespace MonChenil.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeSlotsController : ControllerBase
{
    private readonly TimeSlotService timeSlotService;

    public TimeSlotsController(TimeSlotService timeSlotService)
    {
        this.timeSlotService = timeSlotService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(timeSlotService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var timeSlot = timeSlotService.GetById(id);
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
            timeSlotService.Add(timeSlot);
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
        if (!timeSlotService.Exists(t => t.Id == timeSlot.Id))
        {
            return NotFound();
        }

        timeSlotService.Update(timeSlot);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var timeSlot = timeSlotService.GetById(id);
        if (timeSlot == null)
        {
            return NotFound();
        }

        timeSlotService.Delete(timeSlot);
        return Ok();
    }

    [HttpGet("available")]
    public IActionResult GetAvailableTimeSlots([FromQuery] List<int> petIds)
    {
        var pets = timeSlotService.GetPetsByIds(petIds);
        var availableTimeSlots = timeSlotService.GetAvailableTimeSlots(pets);
        return Ok(availableTimeSlots);
    }
}