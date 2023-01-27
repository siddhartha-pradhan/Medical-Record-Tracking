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
    public class TestRepository : Repository<DiagnosticTest>, ITestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Add(DiagnosticTest diagnosticTest)
        {
            diagnosticTest.CreatedAt = DateTime.Now;
            base.Add(diagnosticTest);
        }

        public void Update(DiagnosticTest diagnosticTest)
        {
            var item = _dbContext.DiagnosticTests.FirstOrDefault(x => x.Id == diagnosticTest.Id);

            if (item != null)
            {
                item.Title = diagnosticTest.Title;
                item.Description = diagnosticTest.Description;
                item.UnitPrice = diagnosticTest.UnitPrice;
                item.InitialRange = diagnosticTest.InitialRange;
                item.FinalRange = diagnosticTest.FinalRange;
                item.Unit = diagnosticTest.Unit;
                item.ClassId = diagnosticTest.ClassId;
                item.LastModifiedAt = DateTime.Now;
            }
        }
    }
}
