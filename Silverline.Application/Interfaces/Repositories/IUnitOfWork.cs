namespace Silverline.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; set; }

    IManufacturerRepository Manufacturer { get; set; }

    IMedicineRepository Medicine { get; set; }

    ISpecialtyRepository Specialty { get; set; }

    ITestRepository Test { get; set; }

    ITestTypeRepository TestType { get; set; }

    void Save();
}
