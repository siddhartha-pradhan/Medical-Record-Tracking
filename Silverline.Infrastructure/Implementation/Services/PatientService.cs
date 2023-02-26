using Silverline.Core.Entities;
using Silverline.Application.Interfaces.Services;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Infrastructure.Persistence;

namespace Silverline.Infrastructure.Implementation.Services;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;

    public PatientService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddCredits(Guid id)
    {
        var patient = _unitOfWork.Patient.GetFirstOrDefault(x => x.Id == id);
        
        if (patient == null)
        {
            patient.CreditPoints += 10;
        }
    }

    public void AddPatient(Patient patient)
    {
        _unitOfWork.Patient.Add(patient);
        _unitOfWork.Save();
    }

    public List<Patient> GetAllPatients()
    {
        return _unitOfWork.Patient.GetAll();
    }

    public Patient GetPatient(Guid Id)
    {
        return _unitOfWork.Patient.Get(Id);
    }
}