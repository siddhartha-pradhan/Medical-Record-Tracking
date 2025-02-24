using EMR.Application.Interfaces.Repositories;
using EMR.Core.Entities;
using EMR.Infrastructure.Persistence;

namespace EMR.Infrastructure.Implementation.Repositories;

public class PharmacistRepository : Repository<Pharmacist>, IPharmacistRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PharmacistRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}