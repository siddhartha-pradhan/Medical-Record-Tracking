using EMR.Core.Entities;

namespace EMR.Application.Interfaces.Services;

public interface IMedicalTreatmentService
{
	List<MedicationTreatment> GetMedicationTreatments();

	void UpdatePrescriptions(MedicationTreatment treatment);

	void CompleteCourse(Guid treatmentId);
}
