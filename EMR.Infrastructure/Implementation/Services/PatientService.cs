using EMR.Core.Entities;
using EMR.Application.Interfaces.Services;
using EMR.Application.Interfaces.Repositories;
using EMR.Infrastructure.Persistence;

namespace EMR.Infrastructure.Implementation.Services;

public class PatientService : IPatientService
{
    private readonly IUnitOfWork _unitOfWork;

    public PatientService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddCredits(Guid id, int point)
    {
        var patient = _unitOfWork.Patient.GetFirstOrDefault(x => x.Id == id);
        
        if (patient != null)
        {
            patient.CreditPoints += point;
        }

        _unitOfWork.Save();

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

	public int GetCredits(string email)
	{
		var user = _unitOfWork.AppUser.GetAll().Where(x => x.Email == email).FirstOrDefault();
        var patient = _unitOfWork.Patient.GetAll().Where(x => x.UserId == user.Id).FirstOrDefault();
        return patient.CreditPoints;
	}

	public Patient GetPatient(Guid Id)
    {
        return _unitOfWork.Patient.Get(Id);
    }

    public void UpdatePatient(Patient patient)
    {
        _unitOfWork.Patient.Update(patient);
        _unitOfWork.Save();
    }
}