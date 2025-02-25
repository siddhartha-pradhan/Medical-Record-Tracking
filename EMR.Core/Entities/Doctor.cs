using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EMR.Core.Entities;

public class Doctor
{
    [Key]
    public Guid Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    [Display(Name = "Certification Number")]
    public string CertificationNumber { get; set; } = string.Empty;

    [Display(Name = "Highest Medical Degree")]
    public string HighestMedicalDegree { get; set; } = string.Empty;

    public Guid DepartmentId { get; set; }

    public string ResumeURL { get; set; } = string.Empty;

    public string CertificationURL { get; set; } = string.Empty;

    public bool IsApproved { get; set; } = false;

    [ForeignKey("DepartmentId")]
    public virtual Specialty? Specialty { get; set; }

    [ForeignKey("UserId")]
    public virtual User? AppUser { get; set; }

    public virtual ICollection<Appointment>? Appointments { get; set; }
}
