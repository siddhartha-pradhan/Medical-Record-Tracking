using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Silverline.Core.ViewModels;

public class PrescriptionDiagnosisViewModel
{
	public Guid Id { get; set; }

	public Guid MedicineId { get; set; }

	public string MedicineURL { get; set; }

	[Display(Name = "Medicine Name")]
	public string MedicineName { get; set; }
	
	[Display(Name = "Type")]
	public string MedicineType { get; set; }

	public string? PharmacistRemarks { get; set; }

	public Guid ReferralId { get; set; }

	public Guid DoctorId { get; set; }

	[Display(Name = "Referred Doctor")]
	public string DoctorName { get; set; }

	public Guid PatientId { get; set; }

	public string PatientImage { get; set; }

	public string PatientName { get; set; }

	[Display(Name = "Doctor Remarks")]
	public string DoctorRemarks { get; set; }

	public string Dose { get; set; }

	public string TimePeriod { get; set; }

	public string TimeFormat { get; set; }

	public DateTime? FinalizedDate { get; set; }
}
