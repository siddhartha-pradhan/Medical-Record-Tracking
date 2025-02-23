using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IManufacturerService
{
    Manufacturer GetManufacturer(Guid Id);

    List<Manufacturer> GetAllManufacturerers();

    void AddManufacturer(Manufacturer manufacturer);

    void UpdateManufacturer(Manufacturer manufacturer);

    void UpdateStatusManufacturer(Manufacturer manufacturer);

    void DeleteManufacturer(Manufacturer manufacturer);
}
