using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Infrastructure.Repositories
{
    public class TestTypeRepository : Repository<TestType>, ITestTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TestTypeRepository(ApplicationDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public void Update(TestType testType)
        {
            throw new NotImplementedException();
        }
    }
}
