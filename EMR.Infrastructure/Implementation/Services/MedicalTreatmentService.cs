using EMR.Application.Interfaces.Repositories;
using EMR.Application.Interfaces.Services;
using EMR.Core.Constants;
using EMR.Core.Entities;

namespace EMR.Infrastructure.Implementation.Services;

public class MedicalTreatmentService : IMedicalTreatmentService
{
	private readonly IUnitOfWork _unitOfWork;

	public MedicalTreatmentService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public void CompleteCourse(Guid treatmentId)
	{
		var treatment = _unitOfWork.MedicalTreatment.Get(treatmentId);
		treatment.Status = Constants.Completed;
		treatment.IsCompleted = true;
		_unitOfWork.Save();
	}

	public List<MedicationTreatment> GetMedicationTreatments()
	{
		var result = _unitOfWork.MedicalTreatment.GetAll();
		
		return result;
	}

	public void UpdatePrescriptions(MedicationTreatment treatment)
	{
		var result = _unitOfWork.MedicalTreatment.GetAll().Where(x => x.Id == treatment.Id).FirstOrDefault();

		if (result != null)
		{
			result.PharmacistRemarks = treatment.PharmacistRemarks;
			result.PharmacistId = treatment.PharmacistId;
			result.FinalizedDate = DateTime.Now;
			result.ActionStatus = Constants.Completed;
		}

		_unitOfWork.Save();
	}
}
