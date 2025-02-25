using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class LaboratoryDiagnosis() : BaseEntity<Guid>(Guid.NewGuid())
{
	[ForeignKey(nameof(DiagnosticTest))]
    public Guid TestId { get; set; }

    [ForeignKey(nameof(AppointmentDetail))]
    public Guid ReferralId { get; set; }

    [ForeignKey(nameof(LabTechnician))]
    public Guid? TechnicianId { get; set; }
    
    public float? Value { get; set; }

	public string DoctorRemarks { get; set; } = string.Empty;

	public string? TechnicianRemarks { get; set; }

    public string? Status { get; set; } = Constants.Constants.Ongoing;

	public string? ActionStatus { get; set; } = Constants.Constants.Pending;

    public DateTime? FinalizedDate { get; set; }

    public virtual MedicalOfficer? LabTechnician { get; set; }

    [ForeignKey("TestId")]
    public virtual DiagnosticTest? DiagnosticTest { get; set; }

    [ForeignKey("ReferralId")]
    public virtual AppointmentDetail? AppointmentDetail { get; set; }
}