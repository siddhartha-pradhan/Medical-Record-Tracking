using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface ILabTechnicianService
{
    LabTechnician GetLabTechnician(Guid Id);

    LabTechnician GetUserLabTechnician(string Id);

    List<LabTechnician> GetAllLabTechnicians();

    void AddLabTechnician(LabTechnician labTechnician);

    void ApproveLabTechnician(LabTechnician labTechnician);
}
