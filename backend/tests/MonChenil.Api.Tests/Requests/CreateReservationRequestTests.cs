using MonChenil.Api.Requests;
using MonChenil.Domain.Pets;
using MonChenil.Domain.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonChenil.Api.Tests.Requests
{
    public class CreateReservationRequestTests
    {
        [Fact]
        public void StartDate_LessThanOneHourFromNow_ThrowsReservationStartDateException()
        {
            var startDate = DateTime.Now.AddMinutes(30);
            var endDate = DateTime.Now.AddDays(1);
            List<PetId> petIds = [];
            Assert.Throws<ReservationStartDateException>(() => new CreateReservationRequest(startDate, endDate, petIds));
        }

        [Fact]
        public void StartDate_MoreThanOneHourFromNow_CreatesReservation()
        {
            var startDate = DateTime.Now.AddHours(1).AddMinutes(1);
            var endDate = DateTime.Now.AddDays(1);
            List<PetId> petIds = [];
            var reservation = new CreateReservationRequest(startDate, endDate, petIds);
            Assert.Equal(startDate, reservation.StartDate);
        }
    }
}
