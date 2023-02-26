using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IPatientService
{
    Patient GetPatient(Guid Id);

    List<Patient> GetAllPatients();

    void AddPatient(Patient patient);

    void AddCredits(Guid id);

}