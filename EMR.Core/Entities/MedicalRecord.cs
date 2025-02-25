using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class MedicalRecord
{
    [Key]
    public Guid Id { get; set; }

    public string Specialty { get; set; } = string.Empty;

    public Guid PatientId { get; set; } 

    public string DoctorName { get; set; }   = string.Empty;

    public string DateOfAppointment { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;   

    public string Description { get; set; } = string.Empty; 

    public string Medicines { get; set; } = string.Empty;

    public string LaboratoryTests { get; set; } = string.Empty;

    [ForeignKey("PatientId")]
    public virtual Patient? Patient { get; set; }
}
