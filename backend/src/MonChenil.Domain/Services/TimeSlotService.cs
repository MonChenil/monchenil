using System.Linq.Expressions;
using MonChenil.Domain.Models;
using MonChenil.Infrastructure.Repositories;

namespace MonChenil.Domain.Services;

public class TimeSlotService
{
    private readonly IRepository<TimeSlot> _repository;
    private readonly IRepository<Pet> _petRepository;

    public TimeSlotService(IRepository<TimeSlot> repository,  IRepository<Pet> petRepository)
    {
        _repository = repository;
        _petRepository = petRepository;
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

        return allTimeSlots.Where(t => ArePetsCompatible(t.Pets.Concat(pets)));
    }

    public IEnumerable<Pet> GetPetsByIds(IEnumerable<int> petIds)
    {
        return _petRepository.GetAll().Where(p => petIds.Contains(p.Id));
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
        var mergedPets = timeSlot.Pets.Concat(pets).ToList();

        if (!ArePetsCompatible(mergedPets))
        {
            throw new ArgumentException("Incompatible pets cannot be added to the same time slot.", nameof(pets));
        }

        timeSlot.Pets.AddRange(pets);
        _repository.Update(timeSlot);
    }

    private bool ArePetsCompatible(IEnumerable<Pet> pets)
    {
        var petList = pets.ToList();
        for (int i = 0; i < petList.Count; i++)
        {
            for (int j = i + 1; j < petList.Count; j++)
            {
                if (!ArePetsCompatible(petList[i], petList[j]))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool ArePetsCompatible(Pet pet1, Pet pet2)
    {
        return !pet1.IncompatibleTypes.Contains(pet2.Type) && !pet2.IncompatibleTypes.Contains(pet1.Type);
    }

}
