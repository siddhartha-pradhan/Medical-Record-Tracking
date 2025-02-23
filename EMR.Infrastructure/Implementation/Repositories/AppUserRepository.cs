using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Infrastructure.Implementation.Repositories;

public class AppUserRepository : Repository<AppUser>, IAppUserRepository
{
    private readonly ApplicationDbContext _dbContext;

	public AppUserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
