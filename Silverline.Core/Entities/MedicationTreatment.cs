using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class MedicationTreatment
{
    [Key]
    public Guid Id { get; set; }

    public Guid MedicineId { get; set; }

    public Guid ReferralId { get; set; }

    public string Status { get; set; } = Constants.Constants.Ongoing; // refers to the medication being under going

    public string Dose { get; set; }

    public bool IsCompleted { get; set; } = false;

    [Display(Name = "Time Period")]
    public string TimePeriod { get; set; }  // Period refering for how long the medication is to be taken or intervals
    
    [Display(Name = "Time Format")]
    public string TimeFormat { get; set; }  // Format refering for when the medication is to be taken

    [Display(Name = "Doctor Remarks")]
	public string DoctorRemarks { get; set; }

    [Display(Name = "Pharmacist Remarks")]
	public string? PharmacistRemarks { get; set; }

	public string? ActionStatus { get; set; } = Constants.Constants.Pending;

	public DateTime? FinalizedDate { get; set; }

	public Guid? PharmacistId { get; set; }

    [ForeignKey("PharmacistId")]
    public virtual Pharmacist? Pharmacist { get; set; }

    [ForeignKey("MedicineId")]
    public virtual Medicine? Medicine { get; set; }

    [ForeignKey("ReferralId")]
    public virtual AppointmentDetail? AppointmentDetail { get; set; }
}
