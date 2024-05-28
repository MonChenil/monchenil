using System.Linq.Expressions;
using MonChenil.Domain.Pets;

namespace MonChenil.Infrastructure.Repositories
{
    internal class PetRepository : IRepository<Pet>
    {
        public void Add(Pet entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Pet entity)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Expression<Func<Pet, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pet> GetAll()
        {
            throw new NotImplementedException();
        }

        public Pet? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Pet entity)
        {
            throw new NotImplementedException();
        }
    }
}
