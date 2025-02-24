using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface IDoctorService
{
    Doctor GetDoctor(Guid Id);

    Doctor GetUserDoctor(string Id);

    List<Doctor> GetAllDoctors();

    void AddDoctor(Doctor doctor);

    void ApproveDoctor(Doctor doctor);
}
