using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;

namespace Silverline.Infrastructure.Implementation.Repositories;

public class TestCartRepository : Repository<TestCart>, ITestCartRepository
{
    private readonly ApplicationDbContext _dbContext;

	public TestCartRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}
