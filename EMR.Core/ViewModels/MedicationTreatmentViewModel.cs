namespace EMR.Core.ViewModels;

public class MedicationTreatmentViewModel
{
	public Guid Id { get; set; }

	public Guid MedicineId { get; set; }

	public Guid ReferralId { get; set; }

	public string MedicineName { get; set; }

	public string Dose { get; set; }

	public string TimePeriod { get; set; }

	public string TimeFormat { get; set; }

	public string DoctorRemarks { get; set; }
}
