using Silverline.Core.Entities;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;
using System.Numerics;

namespace Silverline.Infrastructure.Implementation.Services;

public class LabTechnicianService : ILabTechnicianService
{
    private readonly IUnitOfWork _unitOfWork;

    public LabTechnicianService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddLabTechnician(LabTechnician labTechnician)
    {
        _unitOfWork.LabTechnician.Add(labTechnician);
        _unitOfWork.Save();
    }

    public void ApproveLabTechnician(LabTechnician labTechnician)
    {
        var appUser = _unitOfWork.AppUser.GetFirstOrDefault(x => x.Id == labTechnician.UserId);

        labTechnician.IsApproved = true;
        appUser.EmailConfirmed = true;
        
        _unitOfWork.Save();
    }

    public List<LabTechnician> GetAllLabTechnicians()
    {
        return _unitOfWork.LabTechnician.GetAll();
    }

    public LabTechnician GetLabTechnician(Guid Id)
    {
        return _unitOfWork.LabTechnician.Get(Id);
    }

    public LabTechnician GetUserLabTechnician(string Id)
    {
        return _unitOfWork.LabTechnician.Retrieve(Id);
    }
}
