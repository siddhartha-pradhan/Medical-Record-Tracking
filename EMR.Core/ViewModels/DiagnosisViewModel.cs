namespace EMR.Core.ViewModels;

public class DiagnosisViewModel
{
	public Guid PatientId { get; set; }

	public Guid Referral { get; set; }

	public string PatientName { get; set; }

	public string PatientEmail { get; set; }

	public string PatientImage { get; set; }

	public string PaymentStatus { get; set; }

	public List<LabDiagnosisViewModel> LaboratoryDiagnosis { get; set; }
}
