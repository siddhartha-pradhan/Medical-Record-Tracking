using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class LabTechnician
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public string CertificateNumber { get; set; }

    public string HighestMedicalDegree { get; set; }

    public byte[] Resume { get; set; }

    public string ResumeURL { get; set; }

    public byte[] Certification { get; set; }

    public string CertificationURL { get; set; }

    [ForeignKey("UserId")]
    public virtual AppUser AppUser { get; set; }
}
