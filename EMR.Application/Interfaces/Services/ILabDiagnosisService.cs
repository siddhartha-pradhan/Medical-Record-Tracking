using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface ILabDiagnosisService
{
	List<LaboratoryDiagnosis> GetAllLabDiagnosis();

	void UpdateLabDiagnosis(LaboratoryDiagnosis diagnosis);

    void AddLabDiagnosis(LaboratoryDiagnosis diagnosis);
}
