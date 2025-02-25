using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class MedicationTreatment(): BaseEntity<Guid>(Guid.NewGuid())
{
	[ForeignKey(nameof(Pharmacist))]
	public Guid? PharmacistId { get; set; }

	[ForeignKey(nameof(Medicine))]
    public Guid MedicineId { get; set; }

	[ForeignKey(nameof(AppointmentDetail))]
    public Guid ReferralId { get; set; }

    /// <summary>
    /// The following property refers to the medication being undergoing.
    /// </summary>
    public string Status { get; set; } = Constants.Constants.Ongoing;

    public string Dose { get; set; } = string.Empty;

    public bool IsCompleted { get; set; }

    /// <summary>
    /// The following property defines the period referring for how long the medication is to be taken or intervals.
    /// </summary>
    public string TimePeriod { get; set; } = string.Empty;
    
    /// <summary>
    /// The following property defines the format referring for when the medication is to be taken.
    /// </summary>
    public string TimeFormat { get; set; } = string.Empty;

	public string DoctorRemarks { get; set; } = string.Empty;

	public string? PharmacistRemarks { get; set; }

	public string? ActionStatus { get; set; } = Constants.Constants.Pending;

	public DateTime? FinalizedDate { get; set; }

    public virtual MedicalOfficer? Pharmacist { get; set; }

    public virtual Medicine? Medicine { get; set; }

    public virtual AppointmentDetail? AppointmentDetail { get; set; }
}
