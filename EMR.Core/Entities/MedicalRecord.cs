using EMR.Core.Entities.Shared;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class MedicalRecord() : BaseEntity<Guid>(Guid.NewGuid())
{
    [ForeignKey(nameof(Patient))]
    public Guid PatientId { get; set; } 

    public string Specialty { get; set; } = string.Empty;

    public string Doctor { get; set; }   = string.Empty;

    public string DateOfAppointment { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;   

    public string Description { get; set; } = string.Empty; 

    public string Medicines { get; set; } = string.Empty;

    public string LaboratoryTests { get; set; } = string.Empty;

    public virtual Patient? Patient { get; set; }
}