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
    public class SpecialtyRepository : Repository<Specialty>, ISpecialtyRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SpecialtyRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Add(Specialty specialty)
        {
            specialty.CreatedAt = DateTime.Now;
            base.Add(specialty);
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
}
