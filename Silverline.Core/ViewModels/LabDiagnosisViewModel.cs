using System.ComponentModel.DataAnnotations;

namespace Silverline.Core.ViewModels;

public class LabDiagnosisViewModel
{
	public Guid Id { get; set; }

	public Guid TestId { get; set; }

	[Display(Name = "Test Title")]
	public string TestName { get; set; }	

	[Display(Name = "Test Range")]
	public string TestRange { get; set; }

    public string Unit { get; set; }	

	public float? Value { get; set; }

	public string? TechnicianRemarks { get; set; }

	public Guid ReferralId { get; set; }
	
	public Guid DoctorId { get; set; }

	[Display(Name = "Referred Doctor")]
	public string DoctorName { get; set; }

    public Guid PatientId { get; set; }

	public byte[] PatientImage { get; set; }

	public string PatientName { get; set;}

	[Display(Name = "Doctor Remarks")]
	public string DoctorRemarks { get; set; }

	public DateTime? FinalizedDate { get; set; }
}
