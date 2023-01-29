using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Infrastructure.Implementation.Repositories;

public class TestTypeRepository : Repository<TestType>, ITestTypeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TestTypeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public override List<TestType> FilterDeleted()
    {
        return base.FilterDeleted().Where(x => !x.IsDeleted).ToList();
    }

    public override void Add(TestType testType)
    {
        testType.CreatedAt = DateTime.Now;
        base.Add(testType);
    }
    public void Delete(TestType testType)
    {
        testType.IsDeleted = true;
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
