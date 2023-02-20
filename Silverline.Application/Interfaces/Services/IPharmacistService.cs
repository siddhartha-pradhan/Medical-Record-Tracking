using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IPharmacistService
{
    Pharmacist GetPharmacist(Guid Id);

    List<Pharmacist> GetAllPharmacists();

    void AddPharmacist(Pharmacist Pharmacist);
}