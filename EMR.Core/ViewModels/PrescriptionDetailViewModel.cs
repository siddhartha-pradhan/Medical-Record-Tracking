using EMR.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EMR.Core.ViewModels;

public class PrescriptionDetailViewModel
{
	[Display(Name = "Medicine Name")]
	public string MedicineName { get; set; }

	[Display(Name = "Medicine Type")]
	public string MedicineType { get; set; }

	[Display(Name = "Medicine URL")]
	public string MedicineURL { get; set; }

	public Guid DoctorId { get; set; }

	[Display(Name = "Referred Doctor")]
	public string DoctorName { get; set; }

	public Guid PatientId { get; set; }

	public string PatientImage { get; set; }

	public string PatientName { get; set; }

	public MedicationTreatment MedicationTreatments { get; set; }
}
