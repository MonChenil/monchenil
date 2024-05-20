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

    public IEnumerable<TimeSlot> GetAvailableTimeSlots(IEnumerable<Pet> pets)
    {
        var currentTime = DateTime.Now;
        var allTimeSlots = _repository.GetAll().Where(t => t.StartDate > currentTime);

        return allTimeSlots.Where(t => t.Pets.All(existingPet =>
            pets.All(newPet => ArePetsCompatible(existingPet, newPet))
        ));
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
            throw new ArgumentException("La date de début doit se situer avant la date de fin");
        }

        if (_repository.Exists(t => timeSlot.StartDate < t.EndDate && timeSlot.EndDate > t.StartDate))
        {
            throw new ArgumentException("Le créneau horaire en chevauche un autre");
        }
    }
  
    public void AddPetsToTimeSlot(TimeSlot timeSlot, IEnumerable<Pet> pets)
    {
        foreach (var pet in pets)
        {
            foreach (var existingPet in timeSlot.Pets)
            {
                if (!ArePetsCompatible(existingPet, pet))
                {
                    throw new ArgumentException($"The pet '{pet.Name}' (Type: {pet.Type}) is not compatible with another pet already booked for this time slot.");
                }
            }
        }

        timeSlot.Pets.AddRange(pets);

        _repository.Update(timeSlot);
    }

    private bool ArePetsCompatible(Pet pet1, Pet pet2)
    {
        return !pet1.IncompatibleTypes.Contains(pet2.Type) && !pet2.IncompatibleTypes.Contains(pet1.Type);
    }

    private bool Overlaps(TimeSlot timeSlotA, TimeSlot timeSlotB)
    {
        return timeSlotA.StartDate < timeSlotB.EndDate && timeSlotA.EndDate > timeSlotB.StartDate;
    }
}
