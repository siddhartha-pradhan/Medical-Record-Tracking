using EMR.Application.Interfaces.Repositories;
using EMR.Core.Entities;
using EMR.Infrastructure.Persistence;

namespace EMR.Infrastructure.Implementation.Repositories;

public class TestCartRepository : Repository<TestCart>, ITestCartRepository
{
    private readonly ApplicationDbContext _dbContext;

	public TestCartRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}
