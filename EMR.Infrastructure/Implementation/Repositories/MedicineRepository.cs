using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;
using Silverline.Application.Interfaces.Repositories;


namespace Silverline.Infrastructure.Implementation.Repositories;

public class MedicineRepository : Repository<Medicine>, IMedicineRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MedicineRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override List<Medicine> FilterDeleted()
    {
        return base.FilterDeleted().Where(x => !x.IsDeleted).ToList();
    }

    public override void Add(Medicine medicine)
    {
        medicine.CreatedAt = DateTime.Now;
        base.Add(medicine);
    }

    public void Delete(Medicine medicine)
    {
        medicine.IsDeleted = true;
    }

    public void Update(Medicine medicine)
    {
        var item = _dbContext.Medicines.FirstOrDefault(x => x.Id == medicine.Id);

        if (item != null)
        {
            item.Name = medicine.Name;
            item.Description = medicine.Description;
            item.UnitPrice = medicine.UnitPrice;
            item.Type = medicine.Type;
            item.CategoryId = medicine.CategoryId;
            item.ManufacturerId = medicine.ManufacturerId;
            item.LastModifiedAt = DateTime.Now;
        }
    }
}
