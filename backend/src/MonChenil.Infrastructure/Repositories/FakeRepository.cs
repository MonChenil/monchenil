using System.Linq.Expressions;
using MonChenil.Infrastructure.Entities;

namespace MonChenil.Infrastructure.Repositories;

public class FakeRepository<T> : IRepository<T> where T : IEntity
{
    private readonly List<T> _entities;

    public FakeRepository() : this([]) { }

    public FakeRepository(List<T> entities)
    {
        _entities = entities;
    }

    public T? GetById(int id)
    {
        return _entities.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<T> GetAll()
    {
        return _entities;
    }

    public void Add(T entity)
    {
        _entities.Add(entity);
    }

    public void Update(T entity)
    {
        var index = _entities.FindIndex(e => e.Id == entity.Id);
        _entities[index] = entity;
    }

    public void Delete(T entity)
    {
        _entities.Remove(entity);
    }

    public bool Exists(Expression<Func<T, bool>> predicate)
    {
        return _entities.Any(predicate.Compile());
    }
}