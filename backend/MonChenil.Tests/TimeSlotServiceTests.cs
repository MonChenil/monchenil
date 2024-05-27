using System.Data.Common;
using MonChenil.Infrastructure.Entities;
using MonChenil.Infrastructure.Repositories;
using MonChenil.Domain.Services;

namespace MonChenil.Tests
{
    public class TimeSlotServiceTests
    {
        [Fact]
        public void ClientsCanViewAvailableTimeSlots()
        {
            var existingTimeSlots = new List<TimeSlot>
            {
                new TimeSlot { StartDate = DateTime.Now.AddHours(1), EndDate = DateTime.Now.AddHours(2) },
                new TimeSlot { StartDate = DateTime.Now.AddHours(-1), EndDate = DateTime.Now.AddHours(1) },
            };

            var petRepository = new FakeRepository<Pet>();

            var timeSlotService = new TimeSlotService(new FakeRepository<TimeSlot>(existingTimeSlots), petRepository);

            var availableTimeSlots = timeSlotService.GetAvailableTimeSlots(new List<Pet>());

            Assert.NotNull(availableTimeSlots);
            Assert.Single(availableTimeSlots);
            Assert.Equal(existingTimeSlots[0], availableTimeSlots.First());
        }
    }
}
