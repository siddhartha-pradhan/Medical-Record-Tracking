using EMR.Core.Entities;
using EMR.Infrastructure.Persistence;
using EMR.Application.Interfaces.Repositories;

namespace EMR.Infrastructure.Implementation.Repositories;

internal class LabDiagnosisRepository : Repository<LaboratoryDiagnosis>, ILabDiagnosisRepository
{
	private readonly ApplicationDbContext _dbContext;

	public LabDiagnosisRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}
