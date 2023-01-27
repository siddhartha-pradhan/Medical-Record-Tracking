using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Infrastructure.Implementation.Repositories
{
    public class MedicineRepository : Repository<Medicine>, IMedicineRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MedicineRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Add(Medicine medicine)
        {
            medicine.CreatedAt = DateTime.Now;
            base.Add(medicine);
        }

        public void Update(Medicine medicine)
        {
            var item = _dbContext.Medicines.FirstOrDefault(x => x.Id == medicine.Id);

            if (item != null)
            {
                item.Name = medicine.Name;
                item.Description = medicine.Description;
                item.UnitPrice = medicine.UnitPrice;
                item.CategoryId = medicine.CategoryId;
                item.ManufacturerId = medicine.ManufacturerId;
                item.LastModifiedAt = DateTime.Now;
            }
        }
    }
}
