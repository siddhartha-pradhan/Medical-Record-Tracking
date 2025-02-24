using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);

    void Delete(Category category);
}
