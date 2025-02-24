using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface ICategoryService
{
    Category GetCategory(Guid Id);

    List<Category> GetAllCategories();

    void AddCategory(Category category);

    void UpdateCategory(Category category);

    void DeleteCategory(Category category);
}
