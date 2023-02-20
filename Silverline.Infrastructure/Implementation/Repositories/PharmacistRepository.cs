using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;

namespace Silverline.Infrastructure.Implementation.Repositories;

public class PharmacistRepository : Repository<Pharmacist>, IPharmacistRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PharmacistRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}