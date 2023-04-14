using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Entities;

namespace Silverline.Infrastructure.Implementation.Services;

public class MedicalTreatmentService : IMedicalTreatmentService
{
	private readonly IUnitOfWork _unitOfWork;

	public MedicalTreatmentService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public List<MedicationTreatment> GetMedicationTreatments()
	{
		var result = _unitOfWork.MedicalTreatment.GetAll();
		
		return result;
	}
}
