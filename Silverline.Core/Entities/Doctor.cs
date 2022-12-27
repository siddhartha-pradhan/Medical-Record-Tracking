using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class Doctor
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string CertificationNumber { get; set; } 

    public string HighestMedicalDegree { get; set; }

    public Guid DepartmentId { get; set; }

    [ForeignKey("DepartmentId")]
    public Specialty Specialty { get; set; }
}
