using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface ILabDiagnosisService
{
	List<LaboratoryDiagnosis> GetAllLabDiagnosis();

	void UpdateLabDiagnosis(LaboratoryDiagnosis diagnosis);
}
