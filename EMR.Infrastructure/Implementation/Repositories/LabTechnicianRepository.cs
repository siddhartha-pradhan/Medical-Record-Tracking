using EMR.Application.Interfaces.Repositories;
using EMR.Core.Entities;
using EMR.Infrastructure.Persistence;

namespace EMR.Infrastructure.Implementation.Repositories;

public class LabTechnicianRepository : Repository<LabTechnician>, ILabTechnicianRepository
{
    private readonly ApplicationDbContext _dbContext;

    public LabTechnicianRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
