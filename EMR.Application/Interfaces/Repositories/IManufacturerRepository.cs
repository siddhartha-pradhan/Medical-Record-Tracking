using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Repositories;

public interface IManufacturerRepository : IRepository<Manufacturer>
{
    void Update(Manufacturer manufacturer);

    void Delete(Manufacturer manufacturer);
}
