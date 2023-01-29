using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Infrastructure.Implementation.Repositories;

public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ManufacturerRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override List<Manufacturer> FilterDeleted()
    {
        return base.FilterDeleted().Where(x => !x.IsDeleted).ToList();
    }

    public override void Add(Manufacturer manufacturer)
    {
        manufacturer.CreatedAt = DateTime.Now;
        base.Add(manufacturer);
    }

    public void Delete(Manufacturer manufacturer)
    {
        manufacturer.IsDeleted = true;
    }

    public void Update(Manufacturer manufacturer)
    {
        var item = _dbContext.Manufacturers.FirstOrDefault(x => x.Id == manufacturer.Id);

        if (item != null)
        {
            item.Name = manufacturer.Name;
            item.Location = manufacturer.Location;
            item.LastModifiedAt = DateTime.Now;
        }
    }
}
