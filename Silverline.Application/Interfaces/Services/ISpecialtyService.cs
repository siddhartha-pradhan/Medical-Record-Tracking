using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface ISpecialtyService
{
    Specialty GetSpecialty(Guid Id);

    List<Specialty> GetAllSpecialties();

    void AddSpecialty(Specialty specialty);

    void UpdateSpecialty(Specialty specialty);

    void DeleteSpecialty(Specialty specialty);
}
