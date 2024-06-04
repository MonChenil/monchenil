using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonChenil.Domain.Reservations
{
    public class ReservationDurationException : Exception
    {
        public ReservationDurationException() : base($"You can't make a reservation longer than {Reservation.MaxDurationInDays} days")
        {
        }
    }
}
