using System.Linq.Expressions;

namespace MonChenil.Infrastructure.Repositories;

public interface IRepository<T>
{
    T? GetById(int id);
    IEnumerable<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    bool Exists(Expression<Func<T, bool>> predicate);
}
