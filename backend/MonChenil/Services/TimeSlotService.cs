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

    public void BookTimeSlotForPets(TimeSlot timeSlot, IEnumerable<Pet> pets)
    {
        if (timeSlot == null)
        {
            throw new ArgumentNullException(nameof(timeSlot));
        }

        if (pets == null || !pets.Any())
        {
            throw new ArgumentException("At least one pet must be selected for booking.", nameof(pets));
        }

        timeSlot.Pets.AddRange(pets);

        _repository.Update(timeSlot);
    }

    public void AddPetToTimeSlot(TimeSlot timeSlot, IEnumerable<Pet> pet)
    {
        if (timeSlot == null)
        {
            throw new ArgumentNullException(nameof(timeSlot));
        }

        if (pet == null || !pet.Any())
        {
            throw new ArgumentException("At least one pet must be selected for adding to the time slot.", nameof(pet));
        }

        timeSlot.Pets.AddRange(pet);

        _repository.Update(timeSlot);
    }

    private bool Overlaps(TimeSlot timeSlotA, TimeSlot timeSlotB)
    {
        return timeSlotA.StartDate < timeSlotB.EndDate && timeSlotA.EndDate > timeSlotB.StartDate;
    }
}
