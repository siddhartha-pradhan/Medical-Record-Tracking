using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class TestCart() : BaseEntity<Guid>(Guid.NewGuid())
{
    [ForeignKey(nameof(Patient))]
    public Guid PatientId { get; set; }

    [ForeignKey(nameof(DiagnosticTest))]
    public Guid TestId { get; set; }

    public string PaymentStatus { get; set; } = Constants.Constants.Pending;

    public string ActionStatus { get; set; } = Constants.Constants.Pending;

    public float? Value { get; set; }

    public string? TechnicianRemarks { get; set; }

    public DateTime BookedDate { get; set; }

    public DateTime FinalizedDate { get; set; }

    public virtual Patient? Patient { get; set; }

    public virtual DiagnosticTest? DiagnosticTest { get; set; }
}
