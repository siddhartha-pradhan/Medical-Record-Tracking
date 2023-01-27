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
    public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ManufacturerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Add(Manufacturer manufacturer)
        {
            manufacturer.CreatedAt = DateTime.Now;
            base.Add(manufacturer);
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
}
