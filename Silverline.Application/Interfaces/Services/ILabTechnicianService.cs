using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface ILabTechnicianService
{
    LabTechnician GetLabTechnician(Guid Id);

    List<LabTechnician> GetAllLabTechnicians();

    void AddLabTechnician(LabTechnician LabTechnician);
}
