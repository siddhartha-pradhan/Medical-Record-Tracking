using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class Doctor
{
    [Key]
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public string CertificationNumber { get; set; } 

    public string HighestMedicalDegree { get; set; }

    public Guid DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    public virtual Specialty Specialty { get; set; }

    [ForeignKey("UserId")]
    public virtual AppUser AppUser { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
}
