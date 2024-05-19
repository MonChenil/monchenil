using System.Linq.Expressions;
using MonChenil.Entities;
using MonChenil.Repositories;

namespace MonChenil.Services;

public class TimeSlotService
{
    private readonly IRepository<TimeSlot> _repository;

    public TimeSlotService(IRepository<TimeSlot> repository)
    {
        _repository = repository;
    }

    public void Add(TimeSlot timeSlot)
    {
        CheckTimeSlot(timeSlot);
        _repository.Add(timeSlot);
    }

    public IEnumerable<TimeSlot> GetAll()
    {
        return _repository.GetAll();
    }

    public TimeSlot? GetById(int id)
    {
        return _repository.GetById(id);
    }

    public void Update(TimeSlot timeSlot)
    {
        CheckTimeSlot(timeSlot);
        _repository.Update(timeSlot);
    }

    public void Delete(TimeSlot timeSlot)
    {
        _repository.Delete(timeSlot);
    }

    public bool Exists(Expression<Func<TimeSlot, bool>> predicate)
    {
        return _repository.Exists(predicate);
    }

    private void CheckTimeSlot(TimeSlot timeSlot)
    {
        if (timeSlot.StartDate >= timeSlot.EndDate)
        {
            throw new ArgumentException("Start date must be before end date");
        }

        if (_repository.Exists(t => Overlaps(timeSlot, t)))
        {
            throw new ArgumentException("Time slot overlaps with another");
        }
    }

    private bool Overlaps(TimeSlot timeSlotA, TimeSlot timeSlotB)
    {
        return timeSlotA.StartDate < timeSlotB.EndDate && timeSlotA.EndDate > timeSlotB.StartDate;
    }
}
