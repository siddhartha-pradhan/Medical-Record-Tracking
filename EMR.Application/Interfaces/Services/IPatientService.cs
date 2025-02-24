using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface IPatientService
{
    Patient GetPatient(Guid Id);

    List<Patient> GetAllPatients();

    void AddPatient(Patient patient);

    void AddCredits(Guid id, int point);

    void UpdatePatient(Patient patient);

    int GetCredits(string email);

}