namespace MonChenil.Infrastructure.Entities;

public class TimeSlot : IEntity
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Pet> Pets { get; set; } = [];
}

//private readonly IRepository<TimeSlot> _repository;
//private readonly IRepository<Pet> _petRepository;

//public TimeSlotService(IRepository<TimeSlot> repository,  IRepository<Pet> petRepository)
//{
//    _repository = repository;
//    _petRepository = petRepository;
//}

//public void Add(TimeSlot timeSlot)
//{
//    CheckTimeSlot(timeSlot);
//    _repository.Add(timeSlot);
//}

//public IEnumerable<TimeSlot> GetAll()
//{
//    return _repository.GetAll();
//}

//public IEnumerable<TimeSlot> GetAvailableTimeSlots(IEnumerable<Pet> pets)
//{
//    var currentTime = DateTime.Now;
//    var allTimeSlots = _repository.GetAll().Where(t => t.StartDate > currentTime);

//    return allTimeSlots.Where(t => ArePetsCompatible(t.Pets.Concat(pets)));
//}

//public IEnumerable<Pet> GetPetsByIds(IEnumerable<int> petIds)
//{
//    return _petRepository.GetAll().Where(p => petIds.Contains(p.Id));
//}

//public TimeSlot? GetById(int id)
//{
//    return _repository.GetById(id);
//}

//public void Update(TimeSlot timeSlot)
//{
//    CheckTimeSlot(timeSlot);
//    _repository.Update(timeSlot);
//}

//public void Delete(TimeSlot timeSlot)
//{
//    _repository.Delete(timeSlot);
//}

//public bool Exists(Expression<Func<TimeSlot, bool>> predicate)
//{
//    return _repository.Exists(predicate);
//}

//private void CheckTimeSlot(TimeSlot timeSlot)
//{
//    if (timeSlot.StartDate >= timeSlot.EndDate)
//    {
//        throw new ArgumentException("La date de début doit se situer avant la date de fin");
//    }

//    if (_repository.Exists(t => timeSlot.StartDate < t.EndDate && timeSlot.EndDate > t.StartDate))
//    {
//        throw new ArgumentException("Le créneau horaire en chevauche un autre");
//    }
//}