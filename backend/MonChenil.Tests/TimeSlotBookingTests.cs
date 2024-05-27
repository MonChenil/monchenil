//using MonChenil.Infrastructure.Entities;
//using MonChenil.Infrastructure.Repositories;
//using MonChenil.Domain.Services;

//namespace MonChenil.Tests
//{
//    public class TimeSlotBookingTests
//    {
//        [Fact]
//        public void ClientsCanSelectPetsForBooking()
//        {
//            var fakeRepository = new FakeRepository<TimeSlot>(new List<TimeSlot>());

//            var petRepository = new FakeRepository<Pet>();

//            var timeSlotService = new TimeSlotService(fakeRepository, petRepository);

//            var timeSlot = new TimeSlot { Id = 1, StartDate = DateTime.Now.AddHours(1), EndDate = DateTime.Now.AddHours(2) };
//            fakeRepository.Add(timeSlot);

//            var pets = new List<Pet>
//            {
//                new Pet { Id = 1, Name = "Buddy", Type = PetType.Dog },
//                new Pet { Id = 2, Name = "Whiskers", Type = PetType.Cat }
//            };

//            timeSlotService.AddPetsToTimeSlot(timeSlot, pets);

//            var bookedTimeSlot = timeSlotService.GetById(1);

//            Assert.NotNull(bookedTimeSlot);
//            Assert.Equal(2, bookedTimeSlot.Pets.Count);
//            Assert.Contains(bookedTimeSlot.Pets, pet => pet.Name == "Buddy");
//            Assert.Contains(bookedTimeSlot.Pets, pet => pet.Name == "Whiskers");
//        }

//      [Fact]
//      public void ClientsCanAddPetsToTimeSlot()
//      {
//        var fakeRepository = new FakeRepository<TimeSlot>(new List<TimeSlot>());
//        var petRepository = new FakeRepository<Pet>();

//        var timeSlotService = new TimeSlotService(fakeRepository, petRepository);

//        var timeSlot = new TimeSlot { Id = 1, StartDate = DateTime.Now.AddHours(1), EndDate = DateTime.Now.AddHours(2) };
//        fakeRepository.Add(timeSlot);

//        var pets = new List<Pet>
//        {
//            new Pet { Id = 1, Name = "Buddy", Type = PetType.Dog }
//        };

//        timeSlotService.AddPetsToTimeSlot(timeSlot, pets);

//        var bookedTimeSlot = timeSlotService.GetById(1);

//        Assert.NotNull(bookedTimeSlot);
//        Assert.Single(bookedTimeSlot.Pets);
//        Assert.Contains(bookedTimeSlot.Pets, p => p.Name == "Buddy");
//      }

//        [Fact]
//        public void IncompatiblePetsCannotBeAddedToSameTimeSlot()
//        {
//            var fakeRepository = new FakeRepository<TimeSlot>(new List<TimeSlot>());

//            var petRepository = new FakeRepository<Pet>();

//            var timeSlotService = new TimeSlotService(fakeRepository, petRepository);

//            var timeSlot = new TimeSlot { Id = 1, StartDate = DateTime.Now.AddHours(1), EndDate = DateTime.Now.AddHours(2) };
//            fakeRepository.Add(timeSlot);

//            var dog = new Pet { Id = 1, Name = "Buddy", Type = PetType.Dog, IncompatibleTypes = new List<PetType> { PetType.Cat } };
//            var cat = new Pet { Id = 2, Name = "Whiskers", Type = PetType.Cat, IncompatibleTypes = new List<PetType> { PetType.Dog } };

//            timeSlotService.AddPetsToTimeSlot(timeSlot, new List<Pet> { dog });

//            Assert.Throws<ArgumentException>(() => timeSlotService.AddPetsToTimeSlot(timeSlot, new List<Pet> { cat }));
//        }
//    }
//}
