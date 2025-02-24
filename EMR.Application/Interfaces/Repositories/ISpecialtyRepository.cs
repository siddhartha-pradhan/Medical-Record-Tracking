using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Repositories;

public interface ISpecialtyRepository : IRepository<Specialty>
{
    void Update(Specialty specialty);

    void Delete(Specialty specialty);
}
