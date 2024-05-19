using Xunit;
using MonChenil.Entities;
using MonChenil.Repositories;
using MonChenil.Services;

namespace MonChenil.Tests
{
    public class TimeSlotBookingTests
    {
        [Fact]
        public void ClientsCanSelectPetsForBooking()
        {
            var fakeRepository = new FakeRepository<TimeSlot>(new List<TimeSlot>());
            var timeSlotService = new TimeSlotService(fakeRepository);

            var timeSlot = new TimeSlot { Id = 1, StartDate = DateTime.Now.AddHours(1), EndDate = DateTime.Now.AddHours(2) };
            fakeRepository.Add(timeSlot);

            var pets = new List<Pet>
            {
                new Pet { Id = 1, Name = "Buddy", Type = PetType.Dog },
                new Pet { Id = 2, Name = "Whiskers", Type = PetType.Cat }
            };

            timeSlotService.BookTimeSlotForPets(timeSlot, pets);

            var bookedTimeSlot = timeSlotService.GetById(1);

            Assert.NotNull(bookedTimeSlot);
            Assert.Equal(2, bookedTimeSlot.Pets.Count);
            Assert.Contains(bookedTimeSlot.Pets, pet => pet.Name == "Buddy");
            Assert.Contains(bookedTimeSlot.Pets, pet => pet.Name == "Whiskers");
        }
    }
}
