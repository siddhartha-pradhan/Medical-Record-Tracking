using Silverline.Core.Entities;

namespace Silverline.Application.Interfaces.Services;

public interface IMedicalTreatmentService
{
	List<MedicationTreatment> GetMedicationTreatments();
}
