using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface IDoctorService
{
    MedicalOfficer GetDoctor(Guid Id);

    MedicalOfficer GetUserDoctor(string Id);

    List<MedicalOfficer> GetAllDoctors();

    void AddDoctor(MedicalOfficer medicalOfficer);

    void ApproveDoctor(MedicalOfficer medicalOfficer);
}
