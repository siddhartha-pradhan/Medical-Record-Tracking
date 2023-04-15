namespace Silverline.Core.ViewModels;

public class DiagnosisViewModel
{
	public Guid Referral { get; set; }

	public string PatientName { get; set; }

	public byte[] PatientImage { get; set; }

	public List<LabDiagnosisViewModel> LaboratoryDiagnosis { get; set; }
}
