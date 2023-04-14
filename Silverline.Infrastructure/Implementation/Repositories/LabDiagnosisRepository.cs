using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Infrastructure.Implementation.Repositories;

internal class LabDiagnosisRepository : Repository<LaboratoryDiagnosis>, ILabDiagnosisRepository
{
	private readonly ApplicationDbContext _dbContext;

	public LabDiagnosisRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}
