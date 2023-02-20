using Silverline.Core.Entities;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;

namespace Silverline.Infrastructure.Implementation.Services;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;

    public DoctorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddDoctor(Doctor Doctor)
    {
        _unitOfWork.Doctor.Add(Doctor);
        _unitOfWork.Save();
    }

    public List<Doctor> GetAllDoctors()
    {
        return _unitOfWork.Doctor.GetAll();
    }

    public Doctor GetDoctor(Guid Id)
    {
        return _unitOfWork.Doctor.Get(Id);
    }
}
