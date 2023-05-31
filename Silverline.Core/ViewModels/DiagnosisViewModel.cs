namespace Silverline.Core.ViewModels;

public class DiagnosisViewModel
{
	public Guid Referral { get; set; }

	public string PatientName { get; set; }

	public string PatientImage { get; set; }

	public string PaymentStatus { get; set; }

	public List<LabDiagnosisViewModel> LaboratoryDiagnosis { get; set; }
}
