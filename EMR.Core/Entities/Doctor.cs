using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class Doctor
{
    [Key]
    public Guid Id { get; set; }

    public string UserId { get; set; }

    [Display(Name = "Certification Number")]
    public string CertificationNumber { get; set; }

    [Display(Name = "Highest Medical Degree")]
    public string HighestMedicalDegree { get; set; }

    public Guid DepartmentId { get; set; }

    public string ResumeURL { get; set; }

    public string CertificationURL { get; set; }

    public bool IsApproved { get; set; } = false;

    [ForeignKey("DepartmentId")]
    public virtual Specialty? Specialty { get; set; }

    [ForeignKey("UserId")]
    public virtual AppUser? AppUser { get; set; }

    public virtual ICollection<Appointment>? Appointments { get; set; }
}
