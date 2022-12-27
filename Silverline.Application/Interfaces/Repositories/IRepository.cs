using System.Linq.Expressions;

namespace Silverline.Application.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);

    T GetItem(int ID);

    List<T> GetAll(Expression<Func<T, bool>>? filter, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, string? includeProperties);

    void Add(T entity);

    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entities);
}
