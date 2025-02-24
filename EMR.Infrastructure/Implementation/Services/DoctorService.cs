using EMR.Core.Entities;
using EMR.Application.Interfaces.Services;
using EMR.Application.Interfaces.Repositories;

namespace EMR.Infrastructure.Implementation.Services;

public class DoctorService : IDoctorService
{
    private readonly IUnitOfWork _unitOfWork;

    public DoctorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddDoctor(Doctor doctor)
    {
        _unitOfWork.Doctor.Add(doctor);
        _unitOfWork.Save();
    }

    public void ApproveDoctor(Doctor doctor)
    {
        var appUser = _unitOfWork.AppUser.GetFirstOrDefault(x => x.Id == doctor.UserId);

        doctor.IsApproved = true;
        appUser.EmailConfirmed = true;

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

    public Doctor GetUserDoctor(string Id)
    {
        return _unitOfWork.Doctor.Retrieve(Id);
    }
}
