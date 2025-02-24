using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class TestCart
{
    [Key]
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }

    public Guid TestId { get; set; }

    public string PaymentStatus { get; set; } = Constants.Constants.Pending;

    public string ActionStatus { get; set; } = Constants.Constants.Pending;

    public float? Value { get; set; }

    public string? TechnicianRemarks { get; set; }

    public DateTime BookedDate { get; set; }

    public DateTime FinalizedDate { get; set; }


    [ForeignKey("PatientId")]
    public virtual Patient Patient { get; set; }

    [ForeignKey("TestId")]
    public virtual DiagnosticTest DiagnosticTest { get; set; }

}
