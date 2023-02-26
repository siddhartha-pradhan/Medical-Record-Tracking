using Silverline.Core.Entities;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Infrastructure.Implementation.Services;

public class LabTechnicianService : ILabTechnicianService
{
    private readonly IUnitOfWork _unitOfWork;

    public LabTechnicianService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddLabTechnician(LabTechnician LabTechnician)
    {
        _unitOfWork.LabTechnician.Add(LabTechnician);
        _unitOfWork.Save();
    }

    public void ApproveLabTechnician(AppUser appUser)
    {
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
}
