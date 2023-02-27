using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class LaboratoryDiagnosis
{
    [Key]
    public Guid Id { get; set; }

    public Guid TestId { get; set; }

    public Guid ReferralId { get; set; }

    public float Value { get; set; }    

    public string Remarks { get; set; }

    public Guid TechnicianId { get; set; }

    [ForeignKey("TechnicianId")]
    public virtual LabTechnician? LabTechnician { get; set; }

    [ForeignKey("TestId")]
    public virtual DiagnosticTest DiagnosticTest { get; set; }

    [ForeignKey("ReferralId")]
    public virtual AppointmentDetail AppointmentDetail { get; set; }
}
