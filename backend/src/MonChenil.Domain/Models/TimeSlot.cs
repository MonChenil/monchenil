using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MonChenil.Domain.Models
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Pet> Pets { get; set; } = [];

        public bool IsFull()
        {
            return false;
        }

        public void AddPets(IEnumerable<Pet> pets)
        {
            //var mergedPets = Pets.Concat(pets).ToList();

            //if (!ArePetsCompatible(mergedPets))
            //{
            //    throw new ArgumentException("Incompatible pets cannot be added to the same time slot.", nameof(pets));
            //}

            //Pets.AddRange(pets);
        }

        public void AddPet(Pet pet)
        {

        }


    }
}
