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

    public void AddDoctor(MedicalOfficer medicalOfficer)
    {
        _unitOfWork.Doctor.Add(medicalOfficer);
        _unitOfWork.Save();
    }

    public void ApproveDoctor(MedicalOfficer medicalOfficer)
    {
        var appUser = _unitOfWork.AppUser.GetFirstOrDefault(x => x.Id == medicalOfficer.UserId);

        medicalOfficer.IsApproved = true;
        appUser.EmailConfirmed = true;

        _unitOfWork.Save();
    }

    public List<MedicalOfficer> GetAllDoctors()
    {
        return _unitOfWork.Doctor.GetAll();
    }

    public MedicalOfficer GetDoctor(Guid Id)
    {
        return _unitOfWork.Doctor.Get(Id);
    }

    public MedicalOfficer GetUserDoctor(string Id)
    {
        return _unitOfWork.Doctor.Retrieve(Id);
    }
}
