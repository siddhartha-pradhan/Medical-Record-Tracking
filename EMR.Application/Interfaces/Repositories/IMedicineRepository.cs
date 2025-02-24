using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Repositories;

public interface IMedicineRepository : IRepository<Medicine>
{
    void Update(Medicine medicine);

    void Delete(Medicine medicine);

}
