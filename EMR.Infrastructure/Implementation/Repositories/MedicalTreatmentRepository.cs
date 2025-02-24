using EMR.Core.Entities;
using EMR.Infrastructure.Persistence;
using EMR.Application.Interfaces.Repositories;

namespace EMR.Infrastructure.Implementation.Repositories;

public class MedicalTreatmentRepository : Repository<MedicationTreatment>, IMedicalTreatmentRepository
{
	private readonly ApplicationDbContext _dbContext;

	public MedicalTreatmentRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}
