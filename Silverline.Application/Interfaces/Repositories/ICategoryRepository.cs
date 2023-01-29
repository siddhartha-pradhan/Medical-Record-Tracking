using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);

    void Delete(Category category);
}
