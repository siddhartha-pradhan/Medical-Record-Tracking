using EMR.Core.Entities;
using EMR.Infrastructure.Persistence;
using EMR.Application.Interfaces.Repositories;

namespace EMR.Infrastructure.Implementation.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override List<Category> FilterDeleted()
    {
        return base.FilterDeleted().Where(x => !x.IsDeleted).ToList();
    }

    public override void Add(Category category)
    {
        category.CreatedAt = DateTime.Now;
        base.Add(category);
    }

    public void Delete(Category category)
    {
        category.IsDeleted = true;
    }

    public void Update(Category category)
    {
        var item = _dbContext.Categories.FirstOrDefault(x => x.Id == category.Id);
        
        if (item != null) 
        {
            item.Name = category.Name;
            item.LastModifiedAt = DateTime.Now;
        }
    }
}
