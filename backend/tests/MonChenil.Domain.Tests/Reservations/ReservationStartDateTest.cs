using MonChenil.Domain.Pets;
using MonChenil.Domain.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonChenil.Domain.Tests.Reservations
{
    public class ReservationStartDateTest
    {
        [Fact]
        public void StartDate_TooEarly_ThrowsStartDateReservationException()
        {
            Guid guid = Guid.NewGuid();
            ReservationId id = new ReservationId(guid);
            DateTime start = DateTime.Now.AddMinutes(30);
            DateTime end = DateTime.Now.AddDays(2);
            Assert.Throws<ReservationStartDateException>(() => new Reservation(id, "2", start, end));
        }

    }
}
