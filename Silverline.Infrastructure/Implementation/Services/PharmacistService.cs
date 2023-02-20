using Silverline.Core.Entities;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Infrastructure.Implementation.Services;

public class PharmacistService : IPharmacistService
{
    private readonly IUnitOfWork _unitOfWork;

    public PharmacistService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddPharmacist(Pharmacist Pharmacist)
    {
        _unitOfWork.Pharmacist.Add(Pharmacist);
        _unitOfWork.Save();
    }

    public List<Pharmacist> GetAllPharmacists()
    {
        return _unitOfWork.Pharmacist.GetAll();
    }

    public Pharmacist GetPharmacist(Guid Id)
    {
        return _unitOfWork.Pharmacist.Get(Id);
    }
}
