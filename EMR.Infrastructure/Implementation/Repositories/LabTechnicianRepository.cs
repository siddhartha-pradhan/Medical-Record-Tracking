using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;

namespace Silverline.Infrastructure.Implementation.Repositories;

public class LabTechnicianRepository : Repository<LabTechnician>, ILabTechnicianRepository
{
    private readonly ApplicationDbContext _dbContext;

    public LabTechnicianRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
