using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonChenil.Domain.Models
{
    public abstract class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        //public PetType Type { get; set; }
        public User? Owner { get; set; }
        //public List<PetType> IncompatibleTypes { get; set; } = new List<PetType>();
        public bool IsCompatible(Pet other)
        {
            //return !pet1.IncompatibleTypes.Contains(pet2.Type) && !pet2.IncompatibleTypes.Contains(pet1.Type);
            return false;
        }
    }
}
