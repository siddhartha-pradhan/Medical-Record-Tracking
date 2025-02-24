using EMR.Core.Entities;

namespace EMR.Core.ViewModels;

public class MedicalRecordViewModel
{	
	public string? UserId { get; set; }

	public string? PatientName { get; set; }

	public string Specialty { get; set; }	

	public List<MedicalRecord> MedicalRecords { get; set; }
}
