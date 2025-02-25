using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class LaboratoryDiagnosis
{
    [Key]
    public Guid Id { get; set; }

    public Guid TestId { get; set; }

    public Guid ReferralId { get; set; }

    public float? Value { get; set; }

    [Display(Name = "Doctor Remarks")]
	public string DoctorRemarks { get; set; } = string.Empty;

    [Display(Name = "Technician Remarks")]
	public string? TechnicianRemarks { get; set; }

    public string? Status { get; set; } = Constants.Constants.Ongoing;

	public string? ActionStatus { get; set; } = Constants.Constants.Pending;

    public DateTime? FinalizedDate { get; set; }

	public Guid? TechnicianId { get; set; }

    [ForeignKey("TechnicianId")]
    public virtual LabTechnician? LabTechnician { get; set; }

    [ForeignKey("TestId")]
    public virtual DiagnosticTest? DiagnosticTest { get; set; }

    [ForeignKey("ReferralId")]
    public virtual AppointmentDetail? AppointmentDetail { get; set; }
}
