using Silverline.Application.Interfaces.Repositories;
using Silverline.Application.Interfaces.Services;
using Silverline.Core.Entities;

namespace Silverline.Infrastructure.Implementation.Services;

public class LabDiagnosisService : ILabDiagnosisService
{
	private readonly IUnitOfWork _unitOfWork;

	public LabDiagnosisService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public List<LaboratoryDiagnosis> GetAllLabDiagnosis()
	{
		var result = _unitOfWork.LabDiagnosis.GetAll();

		return result;
	}
}
