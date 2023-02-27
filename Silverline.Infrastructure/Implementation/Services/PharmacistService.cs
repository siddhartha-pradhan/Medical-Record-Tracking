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

    public void AddPharmacist(Pharmacist pharmacist)
    {
        _unitOfWork.Pharmacist.Add(pharmacist);
        _unitOfWork.Save();
    }

    public void ApprovePharmacist(Pharmacist pharmacist)
    {
        var appUser = _unitOfWork.AppUser.GetFirstOrDefault(x => x.Id == pharmacist.UserId);

        appUser.EmailConfirmed = true;
        pharmacist.IsApproved = true;
        
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

    public Pharmacist GetUserPharmacist(string Id)
    {
        return _unitOfWork.Pharmacist.Retrieve(Id);
    }
}