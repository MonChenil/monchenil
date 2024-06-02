using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonChenil.Domain.Reservations
{
    public class ReservationStartDateException : Exception
    {
        public ReservationStartDateException() : base($"Start date must be at least an hour from now")
        {
        }
    }
}
