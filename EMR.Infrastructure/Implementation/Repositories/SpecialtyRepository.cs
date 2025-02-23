using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Infrastructure.Implementation.Repositories;

public class SpecialtyRepository : Repository<Specialty>, ISpecialtyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SpecialtyRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override List<Specialty> FilterDeleted()
    {
        return base.FilterDeleted().Where(x => !x.IsDeleted).ToList();
    }

    public override void Add(Specialty specialty)
    {
        specialty.CreatedAt = DateTime.Now;
        base.Add(specialty);
    }

    public void Delete(Specialty specialty)
    {
        specialty.IsDeleted = true; 
    }

    public void Update(Specialty specialty)
    {
        var item = _dbContext.Specialties.FirstOrDefault(x => x.Id == specialty.Id);

        if (item != null)
        {
            item.Name = specialty.Name;
            item.LastModifiedAt = specialty.LastModifiedAt;
        }
    }
}
