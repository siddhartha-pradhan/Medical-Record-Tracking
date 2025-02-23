using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Repositories;

public interface IMedicineRepository : IRepository<Medicine>
{
    void Update(Medicine medicine);

    void Delete(Medicine medicine);

}
