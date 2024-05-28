using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MonChenil.Domain.Models;

namespace MonChenil.Infrastructure.Repositories
{
    internal class TimeSlotRepository : IRepository<TimeSlot>
    {
        public void Add(TimeSlot entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TimeSlot entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<TimeSlot, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeSlot> GetAll()
        {
            throw new NotImplementedException();
        }

        public TimeSlot? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(TimeSlot entity)
        {
            throw new NotImplementedException();
        }
    }
}
