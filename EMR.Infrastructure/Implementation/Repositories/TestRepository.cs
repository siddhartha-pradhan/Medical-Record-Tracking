using EMR.Core.Entities;
using EMR.Infrastructure.Persistence;
using EMR.Application.Interfaces.Repositories;

namespace EMR.Infrastructure.Implementation.Repositories;

public class TestRepository : Repository<DiagnosticTest>, ITestRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TestRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override List<DiagnosticTest> FilterDeleted()
    {
        return base.FilterDeleted().Where(x => !x.IsDeleted).ToList();
    }

    public override void Add(DiagnosticTest diagnosticTest)
    {
        diagnosticTest.CreatedAt = DateTime.Now;
        base.Add(diagnosticTest);
    }

    public void Delete(DiagnosticTest diagnosticTest)
    {
        diagnosticTest.IsDeleted = true;
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
