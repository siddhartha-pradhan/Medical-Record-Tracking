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
    public class TestTypeRepository : Repository<TestType>, ITestTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TestTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Add(TestType testType)
        {
            testType.CreatedAt = DateTime.Now;
            base.Add(testType);
        }

        public void Update(TestType testType)
        {
            var item = _dbContext.TestTypes.FirstOrDefault(x => x.Id == testType.Id);

            if (item != null)
            {
                item.Name = testType.Name;
                item.LastModifiedAt = DateTime.Now;
            }
        }
    }
}
