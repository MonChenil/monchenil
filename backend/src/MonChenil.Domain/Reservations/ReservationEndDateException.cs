using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonChenil.Domain.Reservations
{
    public class ReservationEndDateException : Exception
    {
        public ReservationEndDateException() : base($"End date must be at least next day from start date")
        {
        }
    }
}
