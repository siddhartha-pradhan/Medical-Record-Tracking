using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Repositories;

public interface IPatientRepository : IRepository<Patient>
{
    void Update(Patient patient);
}
