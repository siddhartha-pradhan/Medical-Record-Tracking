namespace Silverline.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    void Save();

    ICategoryRepository Category { get; set; }

    IManufacturerRepository Manufacturer { get; set; }

    IMedicineRepository Medicine { get; set; }

    ISpecialtyRepository Specialty { get; set; }

    ITestRepository Test { get; set; }

    ITestTypeRepository TestType { get; set; }
}
