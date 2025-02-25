using System.ComponentModel.DataAnnotations;

namespace EMR.Core.Entities;

public class MedicalOfficer
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string Type { get; set; } = string.Empty;

    public string CertificationNumber { get; set; } = string.Empty;

    public string HighestMedicalDegree { get; set; } = string.Empty;

    public Guid? DepartmentId { get; set; }

    public string Resume { get; set; } = string.Empty;

    public string Certification { get; set; } = string.Empty;

    public bool? IsApproved { get; set; }

    public virtual Specialty? Specialty { get; set; }

    public virtual User? AppUser { get; set; }

    public virtual ICollection<Appointment>? Appointments { get; set; }
}
