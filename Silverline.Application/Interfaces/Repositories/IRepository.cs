namespace Silverline.Application.Interfaces.Repositories;

public interface IRepository<T> where T : class
{
    T Get(Guid id);

    List<T> FilterDeleted();

    List<T> GetAll(bool filterDeleted = false);

    void Add(T entity);
}
