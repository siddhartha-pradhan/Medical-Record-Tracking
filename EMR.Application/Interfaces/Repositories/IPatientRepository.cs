using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Repositories;

public interface IPatientRepository : IRepository<Patient>
{
    void Update(Patient patient);
}
