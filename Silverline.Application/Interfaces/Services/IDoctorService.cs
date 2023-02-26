using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IDoctorService
{
    Doctor GetDoctor(Guid Id);

    List<Doctor> GetAllDoctors();

    void AddDoctor(Doctor Doctor);

    void ApproveDoctor(AppUser appUser);
}
