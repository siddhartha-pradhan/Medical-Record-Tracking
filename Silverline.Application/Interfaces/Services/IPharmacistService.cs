using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IPharmacistService
{
    Pharmacist GetPharmacist(Guid Id);
    
    Pharmacist GetUserPharmacist(string Id);

    List<Pharmacist> GetAllPharmacists();

    void AddPharmacist(Pharmacist pharmacist);

    void ApprovePharmacist(Pharmacist pharmacist);
}