using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Repositories;

public interface IManufacturerRepository : IRepository<Manufacturer>
{
    void Update(Manufacturer manufacturer);

    void Delete(Manufacturer manufacturer);
}
