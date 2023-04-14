using Silverline.Core.Entities;
using Silverline.Infrastructure.Persistence;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Infrastructure.Implementation.Repositories;

public class MedicalTreatmentRepository : Repository<MedicationTreatment>, IMedicalTreatmentRepository
{
	private readonly ApplicationDbContext _dbContext;

	public MedicalTreatmentRepository(ApplicationDbContext dbContext) : base(dbContext)
	{
		_dbContext = dbContext;
	}
}
