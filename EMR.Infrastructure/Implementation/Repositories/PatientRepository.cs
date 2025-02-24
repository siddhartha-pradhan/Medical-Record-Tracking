using EMR.Application.Interfaces.Repositories;
using EMR.Core.Entities;
using EMR.Infrastructure.Persistence;

namespace EMR.Infrastructure.Implementation.Repositories;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    private readonly ApplicationDbContext _dbContext;

	public PatientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public void Update(Patient patient)
    {
        var item = _dbContext.Patients.FirstOrDefault(x => x.Id == patient.Id);

        if (item != null)
        {
            item.Address = patient.Address;
            item.CreditPoints = patient.CreditPoints;
        }
    }
}
