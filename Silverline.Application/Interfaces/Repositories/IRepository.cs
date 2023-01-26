using System.Linq.Expressions;

namespace Silverline.Application.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    T Get(int id);

    List<T> GetAll();

    void Add(T entity);

    void Remove(T entity);

    void Save();
}
