using Silverline.Application.Interfaces.Repositories;
using Silverline.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Silverline.Infrastructure.Implementation.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Category = new CategoryRepository(_dbContext);
            Manufacturer = new ManufacturerRepository(_dbContext);
            Medicine = new MedicineRepository(_dbContext);
            Specialty = new SpecialtyRepository(_dbContext);
            Test = new TestRepository(_dbContext);
            TestType = new TestTypeRepository(_dbContext);
        }

        public ICategoryRepository Category { get; set; }
        
        public IManufacturerRepository Manufacturer { get; set; }
        
        public IMedicineRepository Medicine { get; set; }
        
        public ISpecialtyRepository Specialty { get; set; }
        
        public ITestRepository Test { get; set; }
        
        public ITestTypeRepository TestType { get; set; }
        
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
